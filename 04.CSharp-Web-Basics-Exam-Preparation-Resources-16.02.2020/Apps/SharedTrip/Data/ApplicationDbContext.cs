namespace SharedTrip.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel;
    using System.Security.Cryptography.X509Certificates;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UserTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SharedTrip;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary key -> UserTrip
            modelBuilder.Entity<UserTrip>().HasKey(x => new { x.TripId, x.UserId });

            base.OnModelCreating(modelBuilder);
        } 
    }
}
