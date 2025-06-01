using BE.Domain.Entities;
using BE.Domain.Entities.Hypothek;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Persistence
{
    internal class ImmobilienDbContext : DbContext
    {
        internal DbSet<ImmobilienOverview> ImmobilienOverviews { get; set; }
        internal DbSet<ImmobilienType> ImmobilienTypes { get; set; }
        internal DbSet<ImmobilienHausgeld> ImmobilienHausgeld { get; set; }
        internal DbSet<ImmobilienHypothek> ImmobilienHypotheken { get; set; }
        internal DbSet<Bruttomietrendite> Bruttomietrenditen { get; set; }
        internal DbSet<Ruecklage> Ruecklagen { get; set; }

        public ImmobilienDbContext(DbContextOptions<ImmobilienDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship: ImmobilienOverview ↔ ImmobilienType (many-to-one)
            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(o => o.ImmobilienType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: ImmobilienOverview ↔ ImmobilienHausgeld (one-to-one)
            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(o => o.ImmobilienHausgeld)
                .WithOne(h => h.ImmobilienOverview)
                .HasForeignKey<ImmobilienHausgeld>(h => h.ImmobilienOverviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: ImmobilienOverview ↔ ImmobilienHypothek (one-to-one)
            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(o => o.ImmobilienHypothek)
                .WithOne(h => h.ImmobilienOverview)
                .HasForeignKey<ImmobilienHypothek>(h => h.ImmobilienOverviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: ImmobilienOverview ↔ Bruttomietrendite (one-to-one)
            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(o => o.Bruttomietrendite)
                .WithOne(h => h.ImmobilienOverview)
                .HasForeignKey<Bruttomietrendite>(h => h.ImmobilienOverviewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(o => o.Ruecklage)
                .WithOne(h => h.ImmobilienOverview)
                .HasForeignKey<Ruecklage>(h => h.ImmobilienOverviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // Kaufnebenkosten is still an owned type (with nested value objects)
            modelBuilder.Entity<ImmobilienHypothek>().OwnsOne(h => h.Kaufnebenkosten);

            // Kreditbelastung is also an owned type
            modelBuilder.Entity<ImmobilienHypothek>().OwnsOne(h => h.Kreditbelastung);

            // ImmobilienHausgeld has value objects (also owned)
            modelBuilder.Entity<ImmobilienHausgeld>().OwnsOne(h => h.Hausgeld);
            modelBuilder.Entity<ImmobilienHausgeld>().OwnsOne(h => h.UmlagefaehigesHausgeld);
            modelBuilder.Entity<ImmobilienHausgeld>().OwnsOne(h => h.NichtUmlagefaehigesHausgeld);

            modelBuilder.Entity<Bruttomietrendite>().OwnsOne(h => h.UmlagefaehigesHausgeld);
            modelBuilder.Entity<Bruttomietrendite>().OwnsOne(h => h.Kaltmiete);
            modelBuilder.Entity<Bruttomietrendite>().OwnsOne(h => h.Warmmiete);

            modelBuilder.Entity<Ruecklage>().OwnsOne(h => h.Instandhaltung);
            modelBuilder.Entity<Ruecklage>().OwnsOne(h => h.Mietausfall);
            modelBuilder.Entity<Ruecklage>().OwnsOne(h => h.RuecklagenBetrag);


        }
    }
}

