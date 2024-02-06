using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightProjClient.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Kgbooking> Kgbookings { get; set; }

    public virtual DbSet<Kgflight> Kgflights { get; set; }

    public virtual DbSet<Kguser> Kgusers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kgbooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__KGbookin__73951AEDC338CE04");

            entity.ToTable("KGbookings");

            entity.Property(e => e.BookingDate).HasColumnType("datetime");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Kgbookings)
                .HasForeignKey(d => d.Fid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KGbookings__Fid__2AC04CAA");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.Kgbookings)
                .HasForeignKey(d => d.Uid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KGbookings__Uid__2BB470E3");
        });

        modelBuilder.Entity<Kgflight>(entity =>
        {
            entity.HasKey(e => e.Fid).HasName("PK__KGflight__C1D1314A028974A9");

            entity.ToTable("KGflights");

            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.Fdest)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("FDest");
            entity.Property(e => e.Fname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("FName");
            entity.Property(e => e.Frate).HasColumnName("FRate");
            entity.Property(e => e.Fsource)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("FSource");
        });

        modelBuilder.Entity<Kguser>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__KGusers__C5B69A4AC7C84553");

            entity.ToTable("KGusers");

            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Ulocation)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ULocation");
            entity.Property(e => e.Uname)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
