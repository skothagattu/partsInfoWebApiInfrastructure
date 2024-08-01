using Microsoft.EntityFrameworkCore;
using PartsInfoWebApi.core.Models;
using PartsInfoWebApi.core.Models.DesignServices;
using PartsInfoWebApi.Core.Models;
using System.Drawing;

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

        public DbSet<CabAireDWGNumber> CabAireDWGNumbers { get; set; }
        public DbSet<EcoLog> EcoLog { get; set; }
        public DbSet<EcrLog> EcrLog { get; set; }

        public DbSet<CMI_DESC> CMI_DESC { get; set; }
        public DbSet<CMI_VENDOR> CMI_VENDOR { get; set; }
        public DbSet<StdPartIndex> StdPartIndex { get; set; }

        public DbSet<PromInfo> PromInfo { get; set; }
        public DbSet<PromCurrentEco> PromCurrentEco { get; set; }
        public DbSet<PromModel> PromModel { get; set; }
        public DbSet<MB> MB { get; set; }
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
            modelBuilder.Entity<CabAireDWGNumber>(entity =>
            {
                entity.HasKey(e => e.NO);
                entity.Property(e => e.PREFIX).HasMaxLength(50);
                entity.Property(e => e.DESC).HasMaxLength(50);
                entity.Property(e => e.MODEL).HasMaxLength(50);
                entity.Property(e => e.ORIG).HasMaxLength(50);
                entity.Property(e => e.DATE).HasMaxLength(50);
            });
            modelBuilder.Entity<EcoLog>(entity =>
            {
                entity.HasKey(e => e.NO);
                entity.Property(e => e.NO).HasColumnType("int");
                entity.Property(e => e.DESC).HasMaxLength(50);
                entity.Property(e => e.MODEL).HasMaxLength(50);
                entity.Property(e => e.ECR).HasMaxLength(50);
                entity.Property(e => e.DATE_LOG).HasMaxLength(50);
                entity.Property(e => e.NAME).HasMaxLength(50);
                entity.Property(e => e.DATE_REL).HasMaxLength(50);
            });
            modelBuilder.Entity<EcrLog>(entity =>
            {
                entity.HasKey(e => e.NO);
                entity.Property(e => e.NO).HasColumnType("int");
                entity.Property(e => e.DESC).HasMaxLength(500);
                entity.Property(e => e.MODEL).HasMaxLength(50);
                entity.Property(e => e.DATE_LOG).HasMaxLength(50);
                entity.Property(e => e.NAME).HasMaxLength(50);
                entity.Property(e => e.ECO).HasMaxLength(50);
                entity.Property(e => e.DATE_REL).HasMaxLength(50);
            });

            modelBuilder.Entity<CMI_DESC>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasMaxLength(50);
                entity.Property(e => e.CMINO).HasMaxLength(50);
                entity.Property(e => e.DESCRIPTION).HasMaxLength(50);
            });

            modelBuilder.Entity<CMI_VENDOR>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasMaxLength(50);
                entity.Property(e => e.CMINO).HasMaxLength(50);
                entity.Property(e => e.CODE).HasMaxLength(50);
                entity.Property(e => e.MFGNO).HasMaxLength(50);
                entity.Property(e => e.SUB).HasMaxLength(50);
                entity.Property(e => e.DATE).HasMaxLength(50);
                entity.Property(e => e.NOTES).HasMaxLength(50);
            });
            modelBuilder.Entity<StdPartIndex>(entity =>
            {
                entity.ToTable("STDPARTINDEX");
                entity.HasKey(e => e.Number);
                entity.Property(e => e.Number).HasColumnName("NUMBER").HasMaxLength(50);
                entity.Property(e => e.Title).HasColumnName("TITLE").HasMaxLength(50);
            });
            modelBuilder.Entity<PromInfo>(entity =>
            {
                entity.ToTable("PROMINFO");
                entity.HasKey(e => e.PromNo);
                entity.Property(e => e.PromNo).HasColumnName("PROMNO");
                entity.Property(e => e.LocA).HasColumnName("LOCA");
                entity.Property(e => e.LocB).HasColumnName("LOCB");
                entity.Property(e => e.PromB).HasColumnName("PROMB");
                entity.Property(e => e.LocC).HasColumnName("LOCC");
                entity.Property(e => e.PromC).HasColumnName("PROMC");
                entity.Property(e => e.PromA).HasColumnName("PROMA");
                entity.Property(e => e.LocD).HasColumnName("LOCD");
                entity.Property(e => e.PromD).HasColumnName("PROMD");
                entity.Property(e => e.RelDate).HasColumnName("RELDATE");
                entity.Property(e => e.TypeA).HasColumnName("TYPEA");
                entity.Property(e => e.TypeB).HasColumnName("TYPEB");
                entity.Property(e => e.QtyB).HasColumnName("QTYB");
            });

            modelBuilder.Entity<PromCurrentEco>(entity =>
            {
                entity.ToTable("PROMECO");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.PromNo).HasColumnName("PROMNO");
                entity.Property(e => e.CurrentRev).HasColumnName("CURRENTREV");
            });
            modelBuilder.Entity<PromModel>(entity =>
            {
                entity.ToTable("PROMMODEL");
                entity.Property(e => e.PromNo).HasColumnName("PROMNO");
                entity.Property(e => e.Model).HasColumnName("MODEL");
            });
            modelBuilder.Entity<MB>(entity =>
            {
                entity.ToTable("MB");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasMaxLength(50);
                entity.Property(e => e.DwgNo).HasColumnName("DWGNO").HasMaxLength(50);
                entity.Property(e => e.RefItem).HasColumnName("REFITEM").HasMaxLength(50);
                entity.Property(e => e.SIZE).HasMaxLength(50);
                entity.Property(e => e.TYPE).HasMaxLength(50);
                entity.Property(e => e.DESCRIPTION).HasMaxLength(100);
                entity.Property(e => e.LOCATION).HasMaxLength(50);
                entity.Property(e => e.MATLUSED).HasMaxLength(50);
                entity.Property(e => e.OrigNo).HasColumnName("ORIGNO").HasMaxLength(50);
                entity.Property(e => e.CutType).HasColumnName("CUTTYPE").HasMaxLength(50);
                entity.Property(e => e.QtyPer).HasColumnName("QTYPER").HasMaxLength(50);
                entity.Property(e => e.TESTED).HasMaxLength(50);
                entity.Property(e => e.NOTES).HasMaxLength(500);
            });

        }
    }
}

