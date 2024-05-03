using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mobile_Velingrad.Data.Models;

namespace Mobile_Velingrad.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<string>,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Model> Models { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Color> Colors { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Engine> Engines { get; set; }

        public virtual DbSet<EngineType> EngineTypes { get; set; }

        public virtual DbSet<ExtrasPackage> Extras { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<TagCars> TagCars { get; set; }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TagCars>(entity =>
            {
                entity.HasKey(x => new { x.VehicleId, x.TagId });
            });

            builder.Entity<ExtrasPackage>(entity =>
            {
                entity.HasOne(e => e.Vehicle).WithOne(p => p.ExtrasPackage).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<User>(option =>
            {
                option
                .HasIndex(x => x.NationalId)
                .IsUnique();
            });
            base.OnModelCreating(builder);
        }
    }
}
