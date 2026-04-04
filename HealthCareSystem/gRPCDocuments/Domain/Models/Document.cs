namespace HealthCareSystem.gRPCDocuments.Domain.Models
{
    public class Document
    {
        public Guid DocumentId { get; private set; }
        public Guid PatientId { get; private set; }
        public string URL { get; private set; } = string.Empty;

        // EF Core constructor
        private Document() { }

        public Document(Guid documentId, Guid patientId, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be empty.", nameof(url));
            }

            DocumentId = documentId;
            PatientId = patientId;
            URL = url;
        }

        public void UpdateAll(Guid patientId, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be empty.", nameof(url));
            }

            PatientId = patientId;
            URL = url;
        }

        public void UpdateURL(string newURL)
        {
            if (string.IsNullOrWhiteSpace(newURL))
            {
                throw new ArgumentException("Specialty cannot be empty.", nameof(newURL));
            }

            URL = newURL;
        }
    }
}
