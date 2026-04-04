using Grpc.Net.Client;
using HealthCareSystem.AppointmentsAPI.Infrastructure.ExternalServices.gRPCClients.Document;
using HealthCareSystem.AppointmentsAPI.Presentation.DTOs.Request.Document;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareSystem.AppointmentsAPI.Presentation.Controllers
{
    [ApiController]
    public class DocumentController(IConfiguration _configuration) : ControllerBase
    {
        [HttpGet("api/Document/GetAll")]
        public async Task<DocumentList> GetAll()
        {
            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);

            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);

            DocumentList lstDocument = await Client.GetAllAsync(new Empty());

            return lstDocument;
        }

        [HttpGet("api/Document/GetAllByPatientId/{id}")]
        public async Task<DocumentList> GetAllByPatientId(Guid id)
        {
            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);

            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);

            DocumentList lstDocument = await Client.GetAllByPatientIdAsync(new PatientId { Id = id.ToString() });

            return lstDocument;
        }

        [HttpGet("api/Document/GetOneByDocumentId/{id}")]
        public async Task<GrpcDocument> GetOneByDocumentId(Guid id)
        {
            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);

            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);

            GrpcDocument GrpcDocument = await Client.GetOneByDocumentIdAsync(new DocumentId { Id = id.ToString() });

            return GrpcDocument;
        }

        [HttpPost("api/Document/Post")]
        public async Task<IActionResult> Post(PostDocumentRequestDTO request)
        {
            GrpcPostDocument GrpcPostDocument = new()
            {
                PatientId = request.PatientId.ToString(),
                Url = request.URL
            };


            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);

            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);

            await Client.PostAsync(GrpcPostDocument);

            return Created();
        }

        [HttpPut("api/Document/Put")]
        public async Task<IActionResult> Put(PutDocumentRequestDTO request)
        {
            GrpcPutDocument GrpcPutDocument = new()
            {
                DocumentId = request.DocumentId.ToString(),
                PatientId = request.PatientId.ToString(),
                Url = request.URL
            };


            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);

            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);

            await Client.PutAsync(GrpcPutDocument);

            return NoContent();
        }

        [HttpDelete("api/Document/DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);

            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);

            await Client.DeleteAllAsync(new Empty());

            return NoContent();
        }

        [HttpDelete("api/Document/DeleteAllByPatientId/{id}")]
        public async Task<IActionResult> DeleteAllByPatientId(Guid id)
        {
            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);

            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);

            await Client.DeleteAllByPatientIdAsync(new PatientId { Id = id.ToString()});

            return NoContent();
        }

        [HttpDelete("api/Document/DeleteOneByDocumentId/{id}")]
        public async Task<IActionResult> DeleteOneByDocumentId(Guid id)
        {
            //gRPC calls
            using GrpcChannel GrpcChannel = GrpcChannel.ForAddress(_configuration["GrpcEndpoints:DocumentService"]);

            var Client = new DocumentService.DocumentServiceClient(GrpcChannel);

            await Client.DeleteOneByDocumentIdAsync(new DocumentId { Id = id.ToString() });

            return NoContent();
        }
    }
}
