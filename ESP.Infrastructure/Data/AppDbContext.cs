    using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ESP.Infrastructure;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Racetype> Racetypes { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PRIMARY");

            entity.ToTable("Comments");

            entity.HasIndex(e => e.IdRace, "IdRace");

            entity.Property(e => e.IdComment).HasColumnType("int(11)");
            entity.Property(e => e.Comment1)
                .HasColumnType("text")
                .HasColumnName("Comment");
            entity.Property(e => e.IdRace).HasColumnType("int(11)");
            entity.Property(e => e.Rate).HasColumnType("int(11)");

            entity.HasOne(d => d.IdRaceNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdRace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comments_ibfk_1");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.IdRace).HasName("PRIMARY");

            entity.ToTable("Races");

            entity.HasIndex(e => e.IdRaceType, "IdRaceType");

            entity.Property(e => e.IdRace).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IdRaceType).HasColumnType("int(11)");
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Kilometer).HasColumnType("int(11)");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.NumberPlace).HasColumnType("int(11)");
            entity.Property(e => e.RaceName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10,2)");

            entity.HasOne(d => d.IdRaceTypeNavigation).WithMany(p => p.Races)
                .HasForeignKey(d => d.IdRaceType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("races_ibfk_1");
        });

        modelBuilder.Entity<Racetype>(entity =>
        {
            entity.HasKey(e => e.IdRaceType).HasName("PRIMARY");

            entity.ToTable("Racetypes");

            entity.Property(e => e.IdRaceType).HasColumnType("int(11)");
            entity.Property(e => e.RaceType1)
                .HasMaxLength(50)
                .HasColumnName("RaceType");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.IdRegistration).HasName("PRIMARY");

            entity.ToTable("Registrations");

            entity.HasIndex(e => e.IdRace, "IdRace");


            entity.Property(e => e.IdRegistration).HasColumnType("int(11)");
            entity.Property(e => e.BibNumber).HasColumnType("int(11)");
            entity.Property(e => e.IdRace).HasColumnType("int(11)");

            entity.HasOne(d => d.IdRaceNavigation).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.IdRace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registrations_ibfk_2");

        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.IdResult).HasName("PRIMARY");

            entity.ToTable("Results");

            entity.HasIndex(e => e.IdRace, "IdRace");

            entity.HasIndex(e => new { e.IdUser, e.IdRace }, "IdUser").IsUnique();

            entity.Property(e => e.IdResult).HasColumnType("int(11)");
            entity.Property(e => e.IdRace).HasColumnType("int(11)");
            entity.Property(e => e.IdUser).HasColumnType("int(11)");
            entity.Property(e => e.Place).HasColumnType("int(11)");

            entity.HasOne(d => d.IdRaceNavigation).WithMany(p => p.Results)
                .HasForeignKey(d => d.IdRace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("results_ibfk_2");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("Users");

            entity.HasIndex(e => e.Mail, "Mail").IsUnique();

            entity.Property(e => e.IdUser).HasColumnType("int(11)");
            entity.Property(e => e.ClubTeam).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.IsAdmin).HasDefaultValueSql("'0'");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Mail).HasMaxLength(30);
            entity.Property(e => e.Nationality).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
