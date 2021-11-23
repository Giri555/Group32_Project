using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Group32_API.Models
{
    public partial class DestinationInfoDBContext : DbContext
    {
        public DestinationInfoDBContext()
        {
        }

        public DestinationInfoDBContext(DbContextOptions<DestinationInfoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DestinationInfo> DestinationInfos { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //              optionsBuilder.UseSqlServer("Server=mssqlhalab003.cdhvikju3skg.us-east-2.rds.amazonaws.com,1433;Database=DestinationInfoDB;User ID=ha; Password=Password123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DestinationInfo>(entity =>
            {
                entity.HasKey(e => e.DestinationId)
                    .HasName("PK__Destinat__DB5FE4CCA94CFF24");

                entity.ToTable("DestinationInfo");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(500);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Comment).HasMaxLength(1000);

                entity.Property(e => e.DateTime)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.DestinationId)
                    .HasConstraintName("FK_Review_Destination_DestinationId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
