using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataAccess
{
    public class BuildSoftwareDbContext : DbContext
    {
        public virtual DbSet<Werf> Werven { get; set; }
        public virtual DbSet<Werknemer> Werknemers { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Rol> Rollen { get; set; }

        public BuildSoftwareDbContext(DbContextOptions<BuildSoftwareDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=BuildSoftwareDB");
            }

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Werf>()
                .HasMany(e => e.Werknemers)
                .WithOne(e => e.Werf)
                .HasForeignKey(e => e.WerfId)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("RefreshToken");

                entity.Property(e => e.VervalDatum)
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Werknemer)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.WerknemerId);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.RolBeschrijving)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Nieuwe Positie')");
            });

            modelBuilder.Entity<Werknemer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("Werknemer");

                entity.Property(e => e.Voornaam)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Achternaam)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Emailadres)
                    .IsRequired(false)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Wachtwoord)
                    .IsRequired(false)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WerfId)
                   .HasDefaultValueSql("((1))");

                entity.Property(e => e.RolId)
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Werf)
                    .WithMany(p => p.Werknemers)
                    .HasForeignKey(d => d.WerfId);

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Werknemers)
                    .HasForeignKey(d => d.RolId);
            });
        }
    }
}
