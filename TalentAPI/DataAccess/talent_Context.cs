using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TalnetAPI.DataAccess
{
    public partial class talent_Context : DbContext
    {
        public talent_Context()
        {
        }

        public talent_Context(DbContextOptions<talent_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdminLogin> TblAdminLogins { get; set; } = null!;
        public virtual DbSet<TblUploadBanner> TblUploadBanners { get; set; } = null!;
        public virtual DbSet<Tbl_AddExperTise> Tbl_AddExperTises { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=103.83.81.251;database=talent_;User ID=talent;Password=talnetdb@1234;Trusted_Connection=False;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("talent");

            modelBuilder.Entity<TblAdminLogin>(entity =>
            {
                entity.ToTable("Tbl_AdminLogin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Addedon).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Isdelete).HasColumnName("isdelete");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .HasColumnName("role");

                entity.Property(e => e.Useremail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("useremail");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<TblUploadBanner>(entity =>
            {
                entity.ToTable("Tbl_UploadBanner");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath).IsUnicode(false);

                entity.Property(e => e.FileType)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
