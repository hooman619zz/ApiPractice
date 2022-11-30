namespace FirstApiProject.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; }

    }
}
