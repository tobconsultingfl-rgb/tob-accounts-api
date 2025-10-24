using Moq;
using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Domain.Queries;
using TOB.Accounts.Infrastructure.Repositories;
using TOB.Accounts.Services.Handlers.QueryHandlers;
using Xunit;

namespace TOB.Accounts.Services.Tests.Handlers;

public class GetAllAccountTypesQueryHandlerTests
{
    private readonly Mock<IAccountTypeRepository> _mockRepository;
    private readonly GetAllAccountTypesQueryHandler _handler;

    public GetAllAccountTypesQueryHandlerTests()
    {
        _mockRepository = new Mock<IAccountTypeRepository>();
        _handler = new GetAllAccountTypesQueryHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ReturnsAllAccountTypes()
    {
        // Arrange
        var expectedAccountTypes = new List<AccountTypeDto>
        {
            new AccountTypeDto
            {
                AccountTypeId = Guid.NewGuid(),
                Name = "Customer",
                Description = "Customer type",
                IsActive = true,
                DisplayOrder = 1
            },
            new AccountTypeDto
            {
                AccountTypeId = Guid.NewGuid(),
                Name = "Prospect",
                Description = "Prospect type",
                IsActive = true,
                DisplayOrder = 2
            }
        };

        _mockRepository
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedAccountTypes);

        var query = new GetAllAccountTypesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(expectedAccountTypes, result);
        _mockRepository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoAccountTypes()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<AccountTypeDto>());

        var query = new GetAllAccountTypesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRepository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
