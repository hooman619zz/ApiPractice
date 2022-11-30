using FirstApiProject.Data.DbContexts;
using FirstApiProject.UW.Repositories.CityServices;
using FirstApiProject.UW.Repositories.PointOfInterestsServices;
using Microsoft.EntityFrameworkCore;

namespace FirstApiProject.UW
{
    public class UnitOfWork :IUnitOfWork
    {
        public ICityServices cityServices { get; }

        public IPointOfInterestsServices pointOfInterestsServices { get; }
        public UnitOfWork(ICityServices cityServices,IPointOfInterestsServices pointOfInterestsServices)
        {
            this.cityServices = cityServices;
            this.pointOfInterestsServices = pointOfInterestsServices;
        }

    }
}
