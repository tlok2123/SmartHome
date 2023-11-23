using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartHome.Models.Entity;

public partial class SmartHomeContext : DbContext
{
    public SmartHomeContext()
    {
    }

    public SmartHomeContext(DbContextOptions<SmartHomeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<SecurityLog> SecurityLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=SmartHome;User Id=sa;Password=123456;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Email>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Email__3213E83F5DB2F0F8");

            entity.ToTable("Email");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Gmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gmail");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SecurityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Security__3213E83F20EF5540");

            entity.ToTable("SecurityLog");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
