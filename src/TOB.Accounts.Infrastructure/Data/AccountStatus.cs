using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Infrastructure.Data;

public class AccountStatus
{
    [Key]
    public Guid AccountStatusId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public int DisplayOrder { get; set; } = 0;

    // Navigation property
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}
