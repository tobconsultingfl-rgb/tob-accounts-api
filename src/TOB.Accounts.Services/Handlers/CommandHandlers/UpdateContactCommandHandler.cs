using MediatR;
using TOB.Accounts.Domain.Commands;
using TOB.Accounts.Domain.Models;
using TOB.Accounts.Domain.Requests;
using TOB.Accounts.Infrastructure.Repositories;

namespace TOB.Accounts.Services.Handlers.CommandHandlers;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ContactDto?>
{
    private readonly IContactRepository _contactRepository;

    public UpdateContactCommandHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<ContactDto?> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var updateContactRequest = new UpdateContactRequest
        {
            ContactId = request.ContactId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            MobileNumber = request.MobileNumber,
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            IsActive = request.IsActive
        };

        return await _contactRepository.UpdateAsync(updateContactRequest, request.UpdatedBy, cancellationToken);
    }
}
