using Microsoft.EntityFrameworkCore;
using Immobilienverwaltung_Backend.Features.Immobilien_Overview.Models;

namespace Immobilienverwaltung_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<ImmobilienOverview> ImmobilienOverviews { get; set; }
        public DbSet<Immobilien_Type> ImmobilienTypes { get; set; }
        public DbSet<Immobilien_Hausgeld> ImmobilienHausgelder { get; set; }

        // Fluent API configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set table names (optional but good practice)
            modelBuilder.Entity<ImmobilienOverview>().ToTable("ImmobilienOverviews");
            modelBuilder.Entity<Immobilien_Type>().ToTable("ImmobilienTypes");
            modelBuilder.Entity<Immobilien_Hausgeld>().ToTable("ImmobilienHausgeld");

            // Configure Many-to-One relationship between ImmobilienOverview and ImmobilienType
            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(io => io.ImmobilienType)  // Each ImmobilienOverview has one ImmobilienType
                .WithMany(it => it.ImmobilienOverviews)  // Each ImmobilienType can have many ImmobilienOverviews
                .HasForeignKey(io => io.ImmobilienTypeId);  // Foreign Key on ImmobilienOverview

            // Configure One-to-One relationship between ImmobilienOverview and ImmobilienHausgeld
            modelBuilder.Entity<ImmobilienOverview>()
                .HasOne(io => io.ImmobilienHausgeld)  // Each ImmobilienOverview has one ImmobilienHausgeld
                .WithOne(ih => ih.ImmobilienOverview)  // Each ImmobilienHausgeld has one ImmobilienOverview
                .HasForeignKey<ImmobilienOverview>(io => io.ImmobilienHausgeldId) // Foreign Key on ImmobilienOverview
                .OnDelete(DeleteBehavior.Cascade); 


            // Configure properties of ImmobilienHausgeld to be owned by ImmobilienHausgeld entity
            modelBuilder.Entity<Immobilien_Hausgeld>()
                .OwnsOne(h => h.Hausgeld);  // Assuming Hausgeld is a complex type

            modelBuilder.Entity<Immobilien_Hausgeld>()
                .OwnsOne(h => h.Umlagefaehiges_Hausgeld);  // Assuming Umlagefaehiges_Hausgeld is a complex type

            modelBuilder.Entity<Immobilien_Hausgeld>()
                .OwnsOne(h => h.Nicht_Umlagefaehiges_Hausgeld);  // Assuming Nicht_Umlagefaehiges_Hausgeld is a complex type
        }
    }
}
