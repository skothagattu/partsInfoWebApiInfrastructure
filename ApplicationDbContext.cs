using Microsoft.EntityFrameworkCore;
using PartsInfoWebApi.core.Models;
using PartsInfoWebApi.Core.Models;

namespace PartsInfoWebApi.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ThreeLetterCode> ThreeLetterCode { get; set; }
        public DbSet<SubLog> SubLog { get; set; }
        public DbSet<D03numbers> D03numbers { get; set; }
        public DbSet<DWGnumbers> DWGnumbers { get; set; }

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

            modelBuilder.Entity<SubLog>(entity =>
            {
                entity.HasKey(e => e.NO);
                entity.Property(e => e.NO).HasMaxLength(50);
                entity.Property(e => e.PART_NO).HasMaxLength(50);
                entity.Property(e => e.DESC).HasMaxLength(50);
                entity.Property(e => e.REQ_BY).HasMaxLength(50);
                entity.Property(e => e.REQ_DATE).HasMaxLength(50);
                entity.Property(e => e.ASSIGN).HasMaxLength(50);
                entity.Property(e => e.ACCEPT).HasMaxLength(50);
                entity.Property(e => e.REJECT).HasMaxLength(50);
                entity.Property(e => e.DATE).HasMaxLength(50);
            });

            modelBuilder.Entity<D03numbers>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasMaxLength(50);
                entity.Property(e => e.DESCRIPTION).HasMaxLength(50);
                entity.Property(e => e.BL_NUMBER).HasMaxLength(50);
                entity.Property(e => e.PANEL_DWG).HasMaxLength(50);
                entity.Property(e => e.WHO).HasMaxLength(50);
                entity.Property(e => e.START_DATE).HasMaxLength(50);
                entity.Property(e => e.MODEL).HasMaxLength(50);
            });
            modelBuilder.Entity<DWGnumbers>(entity =>
            {
                entity.HasKey(e => e.NO);
                entity.Property(e => e.PREFIX).HasMaxLength(50);
                entity.Property(e => e.NO).HasMaxLength(50);
                entity.Property(e => e.DESC).HasMaxLength(50);
                entity.Property(e => e.MODEL).HasMaxLength(50);
                entity.Property(e => e.ORIG).HasMaxLength(50);
                entity.Property(e => e.DATE).HasMaxLength(50);
            });
        }
    }
}

