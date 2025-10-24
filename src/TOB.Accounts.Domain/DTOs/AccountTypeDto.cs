namespace TOB.Accounts.Domain.DTOs;

public class AccountTypeDto
{
    public Guid AccountTypeId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
}
