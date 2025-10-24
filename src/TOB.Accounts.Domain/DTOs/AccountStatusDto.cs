namespace TOB.Accounts.Domain.DTOs;

public class AccountStatusDto
{
    public Guid AccountStatusId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
}
