using BasalatAssessment.Vehicle.Data.Tracking.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasalatAssessment.Vehicle.Data.Tracking
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        internal DbSet<VehicleTracking> VehicleTracking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleTracking>(entity =>
            {
                entity.HasKey(e => e.VehicleId);
            });
        }
    }
}
