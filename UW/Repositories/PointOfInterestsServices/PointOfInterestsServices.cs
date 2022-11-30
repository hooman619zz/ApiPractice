using FirstApiProject.Data.DbContexts;
using FirstApiProject.Entities;
using FirstApiProject.Repository;

namespace FirstApiProject.UW.Repositories.PointOfInterestsServices
{
    public class PointOfInterestsServices : BaseRepository<PointOfInterest>, IPointOfInterestsServices
    {
        public PointOfInterestsServices(CityContext dbcontext) : base(dbcontext)
        {
        }
    }
}
