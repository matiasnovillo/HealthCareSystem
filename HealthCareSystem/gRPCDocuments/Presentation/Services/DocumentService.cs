using Grpc.Core;
using HealthCareSystem.gRPCDocuments.Domain.Models;
using HealthCareSystem.gRPCDocuments.Infrastructure.Persistence;
using HealthCareSystem.gRPCDocuments.Protos;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem.gRPCDocuments.Presentation.Services
{
    public class DocumentServiceImplementation(DocumentDbContext _context) : DocumentService.DocumentServiceBase
    {
        public override async Task<DocumentList> GetAll(Empty request, ServerCallContext context)
        {
            try
            {
                List<Document> lstDocument = await _context.Document
                    .ToListAsync();

                DocumentList DocumentList = new();

                DocumentList.List.AddRange(lstDocument.Select(x => new GrpcDocument
                {
                    DocumentId = x.DocumentId.ToString(),
                    PatientId = x.PatientId.ToString(),
                    Url = x.URL
                }));

                return DocumentList;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<DocumentList> GetAllByPatientId(PatientId request, ServerCallContext context)
        {
            try
            {
                Guid PatientIdAsGuid = Guid.Parse(request.Id);

                List<Document> lstDocument = await _context.Document
                    .Where(x => x.PatientId == PatientIdAsGuid)
                    .ToListAsync();

                DocumentList DocumentList = new();

                DocumentList.List.AddRange(lstDocument.Select(x => new GrpcDocument
                {
                    DocumentId = x.DocumentId.ToString(),
                    PatientId = x.PatientId.ToString(),
                    Url = x.URL
                }));

                return DocumentList;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<GrpcDocument> GetOneByDocumentId(DocumentId request, ServerCallContext context)
        {
            try
            {
                Guid DocumentIdAsGuid = Guid.Parse(request.Id);

                Document? Document = await _context.Document.FirstOrDefaultAsync(x => x.DocumentId == DocumentIdAsGuid);

                if (Document == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Document not found"));
                }

                GrpcDocument GrpcDocument = new()
                {
                    DocumentId = Document.DocumentId.ToString(),
                    PatientId = Document.PatientId.ToString(),
                    Url = Document.URL
                };

                return GrpcDocument;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<Empty> Post(GrpcPostDocument request, ServerCallContext context)
        {
            try
            {
                Document Document = new(
                Guid.NewGuid(),
                Guid.Parse(request.PatientId),
                request.Url
                );

                _context.Document.Add(Document);

                await _context.SaveChangesAsync();

                return new Empty();
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<Empty> Put(GrpcPutDocument request, ServerCallContext context)
        {
            try
            {
                Guid DocumentIdAsGuid = Guid.Parse(request.DocumentId);

                Document? Document = await _context.Document
                .FirstOrDefaultAsync(x => x.DocumentId == DocumentIdAsGuid);

                if (Document == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Document not found"));
                }

                Document.UpdateAll(
                    Guid.Parse(request.PatientId),
                    request.Url
                    );

                await _context.SaveChangesAsync();

                return new Empty();
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<Empty> DeleteAll(Empty request, ServerCallContext context)
        {
            try
            {
                List<Document> lstDocument = await _context.Document.ToListAsync();

                _context.Document.RemoveRange(lstDocument);

                await _context.SaveChangesAsync();

                return new Empty();
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<Empty> DeleteAllByPatientId(PatientId request, ServerCallContext context)
        {
            try
            {
                Guid PatientIdAsGuid = Guid.Parse(request.Id);

                List<Document> lstDocument = await _context.Document
                    .Where(x => x.PatientId == PatientIdAsGuid)
                    .ToListAsync();

                _context.Document.RemoveRange(lstDocument);

                await _context.SaveChangesAsync();

                return new Empty();
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<Empty> DeleteOneByDocumentId(DocumentId request, ServerCallContext context)
        {
            try
            {
                Guid DocumentIdAsGuid = Guid.Parse(request.Id);

                Document? Document = await _context.Document
                    .FirstOrDefaultAsync(x => x.DocumentId == DocumentIdAsGuid);

                if (Document == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Document not found"));
                }

                _context.Document.Remove(Document);

                await _context.SaveChangesAsync();

                return new Empty();
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
