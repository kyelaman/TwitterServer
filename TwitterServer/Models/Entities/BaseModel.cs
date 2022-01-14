namespace TwitterServer.Models.Entities
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
