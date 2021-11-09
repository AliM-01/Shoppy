using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Domain;
public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public DateTime LastUpdateDate { get; set; } = DateTime.Now;
}
