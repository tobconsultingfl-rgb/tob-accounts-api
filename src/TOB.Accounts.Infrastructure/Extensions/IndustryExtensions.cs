using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Infrastructure.Data;

namespace TOB.Accounts.Infrastructure.Extensions;

public static class IndustryExtensions
{
    public static IndustryDto ToDto(this Industry industry)
    {
        return new IndustryDto
        {
            IndustryId = industry.IndustryId,
            Name = industry.Name,
            Description = industry.Description,
            IsActive = industry.IsActive,
            DisplayOrder = industry.DisplayOrder
        };
    }
}
