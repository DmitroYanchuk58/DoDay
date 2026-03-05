using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
