using MediatR;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Requests;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.CommandHandlers;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactDto>
{
    private readonly IContactRepository _contactRepository;

    public CreateContactCommandHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var createContactRequest = new CreateContactRequest
        {
            TenantId = request.TenantId,
            AccountId = request.AccountId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Salutation = request.Salutation,
            JobTitle = request.JobTitle,
            Department = request.Department,
            IsPrimaryContact = request.IsPrimaryContact,
            ReportsToId = request.ReportsToId,
            OwnerId = request.OwnerId,

            // Contact Information
            Email = request.Email,
            SecondaryEmail = request.SecondaryEmail,
            PhoneNumber = request.PhoneNumber,
            MobileNumber = request.MobileNumber,
            HomePhone = request.HomePhone,
            OtherPhone = request.OtherPhone,
            Fax = request.Fax,
            LinkedIn = request.LinkedIn,
            Twitter = request.Twitter,
            Notes = request.Notes,

            // Mailing Address Information
            MailingAddressLine1 = request.MailingAddressLine1,
            MailingAddressLine2 = request.MailingAddressLine2,
            MailingCity = request.MailingCity,
            MailingState = request.MailingState,
            MailingPostalCode = request.MailingPostalCode,
            MailingCountry = request.MailingCountry,

            // Other Address Information
            OtherAddressLine1 = request.OtherAddressLine1,
            OtherAddressLine2 = request.OtherAddressLine2,
            OtherCity = request.OtherCity,
            OtherState = request.OtherState,
            OtherPostalCode = request.OtherPostalCode,
            OtherCountry = request.OtherCountry,

            Birthdate = request.Birthdate,

            // Preferences
            DoNotCall = request.DoNotCall,
            DoNotEmail = request.DoNotEmail,
            HasOptedOutOfEmail = request.HasOptedOutOfEmail
        };

        return await _contactRepository.CreateAsync(createContactRequest, request.CreatedBy, cancellationToken);
    }
}
