using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Models;

public partial class CureHospitalDbContext : DbContext
{
    public CureHospitalDbContext()
    {

    }

    public CureHospitalDbContext(DbContextOptions<CureHospitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

    public virtual DbSet<Specilization> Specilizations { get; set; }

    public virtual DbSet<Surgery> Surgeries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=SAINATH;Database=CureHospitalDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK_DoctorID");

            entity.ToTable("DOCTOR");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DoctorSpecialization>(entity =>
        {
            entity.HasKey(e => new { e.DoctorId, e.SpecializationCode }).HasName("PK_DoctorIDSpecializationCode");

            entity.ToTable("DoctorSpecialization");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.SpecializationCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSpecializations)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoctorID");

            entity.HasOne(d => d.SpecializationCodeNavigation).WithMany(p => p.DoctorSpecializations)
                .HasForeignKey(d => d.SpecializationCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpecializationCode");
        });

        modelBuilder.Entity<Specilization>(entity =>
        {
            entity.HasKey(e => e.SpecializationCode).HasName("PK_specializationCode");

            entity.ToTable("Specilization");

            entity.Property(e => e.SpecializationCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("specializationCode");
            entity.Property(e => e.SpecializationName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("specializationName");
        });

        modelBuilder.Entity<Surgery>(entity =>
        {
            entity.HasKey(e => e.SurgeryId).HasName("PK_SurgeryID");

            entity.ToTable("Surgery");

            entity.Property(e => e.SurgeryId).HasColumnName("SurgeryID");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.EndTime).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.StartTime).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.SurgeryCategory)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Doctor).WithMany(p => p.Surgeries)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_SurgeryDoctorID");

            entity.HasOne(d => d.SurgeryCategoryNavigation).WithMany(p => p.Surgeries)
                .HasForeignKey(d => d.SurgeryCategory)
                .HasConstraintName("FK_SurgeryCategory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
