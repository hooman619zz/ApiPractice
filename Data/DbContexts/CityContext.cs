using FirstApiProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstApiProject.Data.DbContexts
{
    public class CityContext : DbContext
    {
        #region Ctor & Di
        public CityContext(
                            DbContextOptions<CityContext> options
                          ) : base(options)
        {

        }
        public DbSet<City> cities { get; set; }
        public DbSet<PointOfInterest> pointOfInterests { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Fluent Api For City
            //modelBuilder.Entity<City>(x => x.ToTable("City").Property(p => p.Id).HasColumnName("Id"));
            //modelBuilder.Entity<City>(x => x.ToTable("City").Property(p => p.Name).HasColumnName("Name"));
            //modelBuilder.Entity<City>(x => x.ToTable("City").Property(p => p.Description).HasColumnName("Description"));
            modelBuilder.Entity<City>().Property(a => a.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<City>().Property(a => a.Description).HasMaxLength(200);
            modelBuilder.Entity<City>().HasMany(c => c.PointOfInterests).WithOne(p => p.City);
            #endregion

            #region Fluent Api For PointOfInteres
            //modelBuilder.Entity<PointOfInterest>(x => x.ToTable("PointOfInterests").Property(p=>p.Id).HasColumnName("Id"));
            //modelBuilder.Entity<PointOfInterest>(x => x.ToTable("PointOfInterests").Property(p=>p.Name).HasColumnName("Name"));
            //modelBuilder.Entity<PointOfInterest>(x => x.ToTable("PointOfInterests").Property(p=>p.Description).HasColumnName("Description"));
            modelBuilder.Entity<PointOfInterest>().Property(a => a.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<PointOfInterest>().Property(a => a.Description).HasMaxLength(200);
            modelBuilder.Entity<PointOfInterest>().HasOne(p => p.City).WithMany(c => c.PointOfInterests).HasForeignKey(c => c.CityId);
            #endregion

            #region Use reflection for table set
            base.OnModelCreating(modelBuilder);
            var entitiesAssembly = typeof(BaseEntity).Assembly;
            modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
