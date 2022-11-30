using FirstApiProject.Data.DbContexts;
using FirstApiProject.Entities;
using FirstApiProject.Repository;
using FirstApiProject.UW.Repositories.CityServices;
using Microsoft.EntityFrameworkCore;

namespace FirstApiProject.UW.CityServices
{
    public class CityServices :BaseRepository<City>,ICityServices
    {
        CityContext _context;
        public CityServices(CityContext dbcontext) : base(dbcontext)
        {

        }
        public async Task<City> GetCityAsync(int cityId, bool includerPointOfInterests)
        {
            if (includerPointOfInterests == true)
                return await _context.Set<City>().Include(p => p.PointOfInterests).Where(c => c.Id == cityId).FirstOrDefaultAsync()??throw new ArgumentNullException();
           return await _context.Set<City>().Include(p => p.PointOfInterests).Where(c => c.Id == cityId).FirstOrDefaultAsync() ?? throw new ArgumentNullException();

        }
    }
}
