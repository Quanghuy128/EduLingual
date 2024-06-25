using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduLingual.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now.AddHours(7);
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now.AddHours(7);
        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
