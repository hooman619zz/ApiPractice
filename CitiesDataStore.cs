using FirstApiProject.Models;

namespace FirstApiProject
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto() { Id = 1, Name ="tehran",Description="Capital of iran",PointOfInterests=new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto() { Id = 1, Name ="hooman tower",Description="hooman twer"},
                    new PointOfInterestDto() { Id = 2, Name ="hooman tower",Description="hooman twer2"}

                }
                },
                new CityDto() { Id = 2, Name ="Nyc",PointOfInterests=new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto() { Id = 2, Name ="nyc tower",Description="nyc twer"}
                }
                },
                new CityDto() { Id = 3, Name ="qom",Description="sham dozd"}

            };
        }
    }
}
