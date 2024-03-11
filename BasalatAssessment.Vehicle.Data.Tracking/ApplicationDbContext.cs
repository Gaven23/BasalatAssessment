using BasalatAssessment.Vehicle.Data.Tracking.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasalatAssessment.Vehicle.Data.Tracking
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        internal DbSet<VehicleDetails> VehicleDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleDetails>(entity =>
            {
                entity.HasKey(e => e.VehicleId);
            });
        }
    }
}
