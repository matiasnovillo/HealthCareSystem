using HealthCareSystem.AppointmentsAPI.Application.Interfaces.Doctor;
using HealthCareSystem.AppointmentsAPI.Application.Interfaces.Patient;
using HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.HttpClients.Doctor;
using HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.HttpClients.Patient;
using HealthCareSystem.AppointmentsAPI.Infrastructure.Messaging.Consumers;
using HealthCareSystem.AppointmentsAPI.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppointmentDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register PatientsApiClient
builder.Services.AddHttpClient<IPatientService, PatientApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiEndpoints:PatientsApi"]);
});

// Register DoctorsApiClient
builder.Services.AddHttpClient<IDoctorService, DoctorApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiEndpoints:DoctorsApi"]);
});

//Register MassTransit/RabbitMQ and the consumer
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AppointmentCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        // Configure the receive endpoint
        cfg.ReceiveEndpoint("appointment_created_queue", e =>
        {
            e.PrefetchCount = 1; // Fetch one message at a time
            e.UseConcurrencyLimit(1); // Process one message at a time
            e.ConfigureConsumer<AppointmentCreatedConsumer>(context);
        });
    });
});


builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
