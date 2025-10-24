using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TOB.Accounts.API.Controllers;
using TOB.Accounts.Domain.DTOs;
using TOB.Accounts.Domain.Queries;
using Xunit;

namespace TOB.Accounts.API.Tests.Controllers;

public class AccountTypesControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly AccountTypesController _controller;

    public AccountTypesControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new AccountTypesController(_mockMediator.Object);
    }

    [Fact]
    public async Task GetAllAccountTypesAsync_ReturnsOkResult_WithAccountTypes()
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

        _mockMediator
            .Setup(m => m.Send(It.IsAny<GetAllAccountTypesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedAccountTypes);

        // Act
        var result = await _controller.GetAllAccountTypesAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAccountTypes = Assert.IsAssignableFrom<IEnumerable<AccountTypeDto>>(okResult.Value);
        Assert.Equal(2, returnedAccountTypes.Count());
        _mockMediator.Verify(m => m.Send(It.IsAny<GetAllAccountTypesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAccountTypesAsync_ReturnsOkResult_WithEmptyList_WhenNoAccountTypes()
    {
        // Arrange
        _mockMediator
            .Setup(m => m.Send(It.IsAny<GetAllAccountTypesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<AccountTypeDto>());

        // Act
        var result = await _controller.GetAllAccountTypesAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAccountTypes = Assert.IsAssignableFrom<IEnumerable<AccountTypeDto>>(okResult.Value);
        Assert.Empty(returnedAccountTypes);
    }
}
