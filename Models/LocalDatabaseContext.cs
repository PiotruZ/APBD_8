using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APBD_8.Models;

public partial class LocalDatabaseContext : DbContext
{
    public LocalDatabaseContext()
    {
    }

    public LocalDatabaseContext(DbContextOptions<LocalDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripCountry> TripCountries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localDb)\\localDB;Database=LocalDatabase;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A0450BF3D87");

            entity.HasIndex(e => e.Pesel, "UQ__Clients__48A5F71711022366").IsUnique();

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Pesel).HasMaxLength(11);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => e.ClientTripId).HasName("PK__ClientTr__29CAF0FD2D3406CC");

            entity.Property(e => e.ClientTripId).HasColumnName("ClientTripID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TripId).HasColumnName("TripID");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientTri__Clien__3B75D760");

            entity.HasOne(d => d.Trip).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientTri__TripI__3C69FB99");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Countrie__10D160BF008FC658");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("PK__Trips__51DC711E78E2402E");

            entity.Property(e => e.TripId).HasColumnName("TripID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TripCountry>(entity =>
        {
            entity.HasKey(e => e.TripCountryId).HasName("PK__TripCoun__36401766837ACE3C");

            entity.Property(e => e.TripCountryId).HasColumnName("TripCountryID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.TripId).HasColumnName("TripID");

            entity.HasOne(d => d.Country).WithMany(p => p.TripCountries)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TripCount__Count__403A8C7D");

            entity.HasOne(d => d.Trip).WithMany(p => p.TripCountries)
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TripCount__TripI__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
