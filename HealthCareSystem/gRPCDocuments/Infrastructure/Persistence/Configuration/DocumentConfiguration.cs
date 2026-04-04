using gRPCDocuments.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gRPCDocuments.Infrastructure.Persistence.Configuration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> entity)
        {
            try
            {
                //DocumentId
                entity.HasKey(e => e.DocumentId);
            }
            catch (Exception) { throw; }
        }
    }
}
