using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Persistence
{
    internal class ImmobilienDbContext : DbContext
    {
        internal DbSet<ImmobilienOverview> ImmobilienOverviews { get; set; }
        internal DbSet<ImmobilienType> ImmobilienTypes { get; set; }
        internal DbSet<ImmobilienHausgeld> ImmobilienHausgeld { get; set; }



        public ImmobilienDbContext(DbContextOptions<ImmobilienDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //ImmobilienOverview has one Immobilientype, but many immobilienOverviews can have the same immobilienType

            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(o => o.ImmobilienType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //ImmobilienOverview has one ImmobilienHausgeld, and one immobilienhausgeld belongs to one immobilienOverview


            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(o => o.ImmobilienHausgeld)
                .WithOne(h => h.ImmobilienOverview)
                .HasForeignKey<ImmobilienHausgeld>(h => h.ImmobilienOverviewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImmobilienHausgeld>().OwnsOne(h => h.Hausgeld);
            modelBuilder.Entity<ImmobilienHausgeld>().OwnsOne(h => h.UmlagefaehigesHausgeld);
            modelBuilder.Entity<ImmobilienHausgeld>().OwnsOne(h => h.NichtUmlagefaehigesHausgeld);

        }
    }
}
