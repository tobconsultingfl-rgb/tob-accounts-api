using TOB.Accounts.Domain.DTOs;
using Xunit;

namespace TOB.Accounts.Domain.Tests.DTOs;

public class AccountTypeDtoTests
{
    [Fact]
    public void AccountTypeDto_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var accountTypeId = Guid.NewGuid();
        var name = "Customer";
        var description = "Existing customer with active business";
        var displayOrder = 1;

        // Act
        var dto = new AccountTypeDto
        {
            AccountTypeId = accountTypeId,
            Name = name,
            Description = description,
            IsActive = true,
            DisplayOrder = displayOrder
        };

        // Assert
        Assert.Equal(accountTypeId, dto.AccountTypeId);
        Assert.Equal(name, dto.Name);
        Assert.Equal(description, dto.Description);
        Assert.True(dto.IsActive);
        Assert.Equal(displayOrder, dto.DisplayOrder);
    }

    [Fact]
    public void AccountTypeDto_Properties_CanBeSetToNull()
    {
        // Arrange & Act
        var dto = new AccountTypeDto
        {
            AccountTypeId = Guid.NewGuid(),
            Name = "Test",
            Description = null,
            IsActive = false,
            DisplayOrder = 0
        };

        // Assert
        Assert.NotNull(dto);
        Assert.Equal("Test", dto.Name);
        Assert.Null(dto.Description);
        Assert.False(dto.IsActive);
    }
}
