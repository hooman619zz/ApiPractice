namespace FirstApiProject.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<PointOfInterest> PointOfInterests { get; set; }

    }
}
