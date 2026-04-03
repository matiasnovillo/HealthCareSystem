using HealthCareSystem.Application.Interfaces.Doctor;

namespace HealthCareSystem.Infrastructure.ExternalServices.HttpClients.Doctor
{
    public class DoctorApiClient(HttpClient httpClient) : IDoctorService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<DoctorResponse> GetOneByIdAsync(Guid id)
        {
            HttpResponseMessage HttpResponseMessage = await _httpClient.GetAsync($"api/Doctor/GetOneByGUID/{id}");

            HttpResponseMessage.EnsureSuccessStatusCode();

            return await HttpResponseMessage.Content.ReadFromJsonAsync<DoctorResponse>() ?? throw new InvalidOperationException("Failed to deserialize the response.");
        }
    }
}
