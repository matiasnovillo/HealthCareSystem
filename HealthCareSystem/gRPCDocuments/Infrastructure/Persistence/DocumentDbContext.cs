using gRPCDocuments.Domain.Models;
using gRPCDocuments.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace gRPCDocuments.Infrastructure.Persistence
{
    public class DocumentDbContext : DbContext
    {
        public DbSet<Document> Document { get; set; }

        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.ApplyConfiguration(new DocumentConfiguration());
                modelBuilder.Entity<Document>().ToTable("Document");
            }
            catch (Exception) { throw; }
        }
    }
}
