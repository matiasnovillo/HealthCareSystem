using HealthCareSystem.AppointmentsAPI.Application.Interfaces.Patient;

namespace HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.HttpClients.Patient
{
    public class PatientApiClient(HttpClient httpClient) : IPatientService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<PatientResponse> GetOneByIdAsync(Guid id)
        {
            HttpResponseMessage HttpResponseMessage = await _httpClient.GetAsync($"api/Patient/GetOneByGUID/{id}");

            HttpResponseMessage.EnsureSuccessStatusCode();

            return await HttpResponseMessage.Content.ReadFromJsonAsync<PatientResponse>() ?? throw new InvalidOperationException("Failed to deserialize the response.");
        }
    }
}
