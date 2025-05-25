using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirstDemo.Models;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Roll).HasName("pk_roll");

            entity.ToTable("Student");

            entity.Property(e => e.Roll)
                .ValueGeneratedNever()
                .HasColumnName("roll");
            entity.Property(e => e.StuDob).HasColumnName("stuDob");
            entity.Property(e => e.StuGender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("stuGender");
            entity.Property(e => e.StuName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("stuName");
            entity.Property(e => e.StuPhone)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("stuPhone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
