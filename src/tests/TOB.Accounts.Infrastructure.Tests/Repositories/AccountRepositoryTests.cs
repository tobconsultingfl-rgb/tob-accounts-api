using Microsoft.EntityFrameworkCore;
using TOB.Accounts.Domain.Requests;
using TOB.Accounts.Infrastructure.Data;
using TOB.Accounts.Infrastructure.Repositories.Implementations;
using Xunit;

namespace TOB.Accounts.Infrastructure.Tests.Repositories;

public class AccountRepositoryTests : IDisposable
{
    private readonly AccountsDbContext _context;
    private readonly AccountRepository _repository;
    private readonly Guid _testTenantId = Guid.NewGuid();

    public AccountRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AccountsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AccountsDbContext(options);
        _repository = new AccountRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAccountsForTenant()
    {
        // Arrange
        var account1 = new Account
        {
            AccountId = Guid.NewGuid(),
            TenantId = _testTenantId.ToString(),
            Name = "Test Account 1",
            CreatedBy = "test"
        };
        var account2 = new Account
        {
            AccountId = Guid.NewGuid(),
            TenantId = _testTenantId.ToString(),
            Name = "Test Account 2",
            CreatedBy = "test"
        };
        var otherTenantAccount = new Account
        {
            AccountId = Guid.NewGuid(),
            TenantId = Guid.NewGuid().ToString(),
            Name = "Other Tenant Account",
            CreatedBy = "test"
        };

        await _context.Accounts.AddRangeAsync(account1, account2, otherTenantAccount);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync(_testTenantId);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, a => Assert.Equal(_testTenantId.ToString(), a.TenantId));
    }

    [Fact]
    public async Task CreateAsync_AddsNewAccount()
    {
        // Arrange
        var createRequest = new CreateAccountRequest
        {
            TenantId = _testTenantId.ToString(),
            Name = "New Account",
            AccountType = "Customer",
            AccountStatus = "Active",
            Industry = "Technology"
        };

        // Act
        var result = await _repository.CreateAsync(createRequest, "test-user");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createRequest.Name, result.Name);
        Assert.Equal(createRequest.TenantId, result.TenantId);

        var savedAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == result.AccountId);
        Assert.NotNull(savedAccount);
        Assert.Equal("test-user", savedAccount.CreatedBy);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsAccount_WhenExists()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var account = new Account
        {
            AccountId = accountId,
            TenantId = _testTenantId.ToString(),
            Name = "Test Account",
            CreatedBy = "test"
        };

        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(accountId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accountId, result.AccountId);
        Assert.Equal("Test Account", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotExists()
    {
        // Act
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_SoftDeletesAccount()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var account = new Account
        {
            AccountId = accountId,
            TenantId = _testTenantId.ToString(),
            Name = "Test Account",
            IsActive = true,
            CreatedBy = "test"
        };

        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteAsync(accountId, "test-deleter");

        // Assert
        Assert.True(result);

        var deletedAccount = await _context.Accounts.IgnoreQueryFilters()
            .FirstOrDefaultAsync(a => a.AccountId == accountId);
        Assert.NotNull(deletedAccount);
        Assert.False(deletedAccount.IsActive);
        Assert.Equal("test-deleter", deletedAccount.UpdatedBy);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrue_WhenAccountExists()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            TenantId = _testTenantId.ToString(),
            Name = "Existing Account",
            CreatedBy = "test"
        };

        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.ExistsAsync("Existing Account", _testTenantId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsFalse_WhenAccountDoesNotExist()
    {
        // Act
        var result = await _repository.ExistsAsync("Non-Existent Account", _testTenantId);

        // Assert
        Assert.False(result);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
