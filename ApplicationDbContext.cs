using Microsoft.EntityFrameworkCore;
using PartsInfoWebApi.Models;

namespace PartsInfoWebApi.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ThreeLetterCode> ThreeLetterCode { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ThreeLetterCode>(entity =>
            {
                entity.HasKey(e => e.CODE);
                entity.Property(e => e.CODE).HasMaxLength(50);
                entity.Property(e => e.TYPE).HasMaxLength(50);
                entity.Property(e => e.COMPANY).HasMaxLength(100);
                entity.Property(e => e.ADDRESS1).HasMaxLength(100);
                entity.Property(e => e.ADDRESS2).HasMaxLength(100);
                entity.Property(e => e.CITY_STATE_ZIP).HasMaxLength(100);
                entity.Property(e => e.CONTACT1).HasMaxLength(50);
                entity.Property(e => e.PHONE1).HasMaxLength(20);
                entity.Property(e => e.EXT1).HasMaxLength(10);
                entity.Property(e => e.CONTACT2).HasMaxLength(50);
                entity.Property(e => e.PHONE2).HasMaxLength(20);
                entity.Property(e => e.EXT2).HasMaxLength(10);
                entity.Property(e => e.FAX).HasMaxLength(20);
                entity.Property(e => e.TERMS).HasMaxLength(50);
                entity.Property(e => e.FOB).HasMaxLength(50);
                entity.Property(e => e.NOTES).HasMaxLength(500);
            });
        }
    }
}
