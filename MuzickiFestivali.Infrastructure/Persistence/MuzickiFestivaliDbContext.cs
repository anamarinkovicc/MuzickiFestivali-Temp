using Microsoft.EntityFrameworkCore;
using MuzickiFestivali.Domain.Entities;
using MuzickiFestivali.Domain.Enums;

namespace MuzickiFestivali.Infrastructure;

public class MuzickiFestivaliDbContext : DbContext
{
    public MuzickiFestivaliDbContext(DbContextOptions<MuzickiFestivaliDbContext> options) : base(options) { }

    public DbSet<Osoba> Osobe { get; set; }
    public DbSet<Zaposleni> Zaposleni { get; set; }
    public DbSet<Izvodjac> Izvodjaci { get; set; }
    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Festival> Festivali { get; set; }
    public DbSet<Nastup> Nastupi { get; set; }
    public DbSet<Termin> Termini { get; set; }
    public DbSet<Bina> Bine { get; set; }
    public DbSet<Lajkuje> Lajkovi { get; set; }
    public DbSet<Nastupa> Nastupanja { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Osoba>().ToTable("Osobe").HasKey(o => o.idOsoba);
        modelBuilder.Entity<Zaposleni>().ToTable("Zaposleni");
        modelBuilder.Entity<Izvodjac>().ToTable("Izvodjaci");
        modelBuilder.Entity<Korisnik>().ToTable("Korisnici");

        modelBuilder.Entity<Festival>().ToTable("Festivali").HasKey(f => f.idFestival);
        modelBuilder.Entity<Festival>()
            .HasOne(f => f.zaposleni)
            .WithMany(z => z.festivali)
            .HasForeignKey(f => f.idOsoba)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Nastup>(entity =>
        {
            entity.ToTable("Nastupi");
            entity.HasKey(n => new { n.idFestival, n.idNastup });
            entity.Property(n => n.idNastup).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Festival>()
            .HasMany(f => f.nastupi)
            .WithOne(n => n.festival)
            .HasForeignKey(n => n.idFestival)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Termin>(entity =>
        {
            entity.ToTable("Termini");
            entity.HasKey(t => new { t.idFestival, t.idNastup, t.idTermin });
            entity.Property(t => t.idTermin).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Nastup>()
            .HasMany(n => n.termini)
            .WithOne(t => t.nastup)
            .HasForeignKey(t => new { t.idFestival, t.idNastup })
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Termin>()
            .HasOne(t => t.bina)
            .WithMany(b => b.termini)
            .HasForeignKey(t => t.idBina)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Bina>().ToTable("Bine").HasKey(b => b.idBina);

        modelBuilder.Entity<Izvodjac>()
            .Property(i => i.zanr).HasConversion<string>();
        modelBuilder.Entity<Korisnik>()
            .Property(k => k.omiljeniZanr).HasConversion<string>();
        modelBuilder.Entity<Nastup>()
            .Property(n => n.zanr).HasConversion<string>();
        modelBuilder.Entity<Termin>()
            .Property(t => t.tip).HasConversion<string>();

        modelBuilder.Entity<Lajkuje>().ToTable("Lajkovi")
            .HasKey(l => new { l.idOsoba, l.idFestival, l.idNastup });

        modelBuilder.Entity<Lajkuje>()
            .HasOne(l => l.korisnik)
            .WithMany(k => k.lajkuje)
            .HasForeignKey(l => l.idOsoba)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Lajkuje>()
            .HasOne(l => l.nastup)
            .WithMany(n => n.lajkovi)
            .HasForeignKey(l => new { l.idFestival, l.idNastup })
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Nastupa>().ToTable("Nastupanja")
            .HasKey(n => new { n.idOsoba, n.idFestival, n.idNastup, n.idTermin });

        modelBuilder.Entity<Nastupa>()
            .HasOne(n => n.izvodjac)
            .WithMany(i => i.nastupa)
            .HasForeignKey(n => n.idOsoba)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Nastupa>()
            .HasOne(n => n.termin)
            .WithMany(t => t.izvodjaci)
            .HasForeignKey(n => new { n.idFestival, n.idNastup, n.idTermin })
            .OnDelete(DeleteBehavior.Cascade);
    }
}