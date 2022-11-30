namespace FirstApiProject.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }


        public ICollection<PointOfInterestDto> PointOfInterests { get; set; } = new List<PointOfInterestDto>();

    }
}
