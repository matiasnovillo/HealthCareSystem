using HealthCareSystem.Application.Interfaces.Doctor;
using HealthCareSystem.Application.Interfaces.Patient;
using HealthCareSystem.Infrastructure.ExternalServices.Doctor;
using HealthCareSystem.Infrastructure.ExternalServices.Patient;
using HealthCareSystem.Infrastructure.Persistence;
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
