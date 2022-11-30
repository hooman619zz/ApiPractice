using FirstApiProject.UW.Repositories.CityServices;
using FirstApiProject.UW.Repositories.PointOfInterestsServices;

namespace FirstApiProject.UW
{
    public interface IUnitOfWork
    {
        public ICityServices cityServices { get; }
        public IPointOfInterestsServices pointOfInterestsServices { get; }
    }
}
