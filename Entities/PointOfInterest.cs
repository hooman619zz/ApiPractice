namespace FirstApiProject.Entities
{
    public class PointOfInterest :BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
