namespace DemoApplication.Database.Models.Common
{
    public abstract class BaseEntity<Tkey>
    {
        public Tkey Id { get; set; }
    }
}
