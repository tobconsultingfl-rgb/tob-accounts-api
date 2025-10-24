using MediatR;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Requests;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.CommandHandlers;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountDto?>
{
    private readonly IAccountRepository _accountRepository;

    public UpdateAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto?> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var updateAccountRequest = new UpdateAccountRequest
        {
            AccountId = request.AccountId,
            Name = request.Name,

            // CRM Business Information
            AccountType = request.AccountType,
            AccountStatus = request.AccountStatus,
            Industry = request.Industry,
            AnnualRevenue = request.AnnualRevenue,
            NumberOfEmployees = request.NumberOfEmployees,
            Website = request.Website,
            Description = request.Description,
            AccountNumber = request.AccountNumber,
            ParentAccountId = request.ParentAccountId,
            OwnerId = request.OwnerId,
            Rating = request.Rating,

            // Billing Address Information
            BillingAddressLine1 = request.BillingAddressLine1,
            BillingAddressLine2 = request.BillingAddressLine2,
            BillingCity = request.BillingCity,
            BillingState = request.BillingState,
            BillingPostalCode = request.BillingPostalCode,
            BillingCountry = request.BillingCountry,

            // Shipping Address Information
            ShippingAddressLine1 = request.ShippingAddressLine1,
            ShippingAddressLine2 = request.ShippingAddressLine2,
            ShippingCity = request.ShippingCity,
            ShippingState = request.ShippingState,
            ShippingPostalCode = request.ShippingPostalCode,
            ShippingCountry = request.ShippingCountry,

            // Contact Information
            Phone = request.Phone,
            Fax = request.Fax,
            Email = request.Email,

            IsActive = request.IsActive
        };

        return await _accountRepository.UpdateAsync(updateAccountRequest, request.UpdatedBy, cancellationToken);
    }
}
