using FirstApiProject.UW;
using FirstApiProject.UW.CityServices;
using FirstApiProject.UW.Repositories.CityServices;
using FirstApiProject.UW.Repositories.PointOfInterestsServices;

namespace FirstApiProject.Services
{
    public static class DependencyInjections
    {
        public static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICityServices, CityServices>();
            services.AddScoped<IPointOfInterestsServices, PointOfInterestsServices>();


        }
    }
}
