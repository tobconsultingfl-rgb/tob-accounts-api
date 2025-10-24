using System.ComponentModel.DataAnnotations;

namespace TOB.Accounts.Domain.Requests;

public class UpdateContactRequest
{
    [Required(ErrorMessage = "Contact ID is required")]
    public Guid ContactId { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public required string LastName { get; set; }

    [MaxLength(100, ErrorMessage = "Middle name cannot exceed 100 characters")]
    public string? MiddleName { get; set; }

    [MaxLength(20, ErrorMessage = "Salutation cannot exceed 20 characters")]
    public string? Salutation { get; set; }

    [MaxLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
    public string? JobTitle { get; set; }

    [MaxLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
    public string? Department { get; set; }

    public bool IsPrimaryContact { get; set; } = false;

    public Guid? ReportsToId { get; set; }

    public Guid? OwnerId { get; set; }

    // Contact Information
    [MaxLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string? Email { get; set; }

    [MaxLength(200, ErrorMessage = "Secondary email cannot exceed 200 characters")]
    [EmailAddress(ErrorMessage = "Invalid secondary email address format")]
    public string? SecondaryEmail { get; set; }

    [MaxLength(50, ErrorMessage = "Phone number cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string? PhoneNumber { get; set; }

    [MaxLength(50, ErrorMessage = "Mobile number cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid mobile number format")]
    public string? MobileNumber { get; set; }

    [MaxLength(50, ErrorMessage = "Home phone cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid home phone format")]
    public string? HomePhone { get; set; }

    [MaxLength(50, ErrorMessage = "Other phone cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid other phone format")]
    public string? OtherPhone { get; set; }

    [MaxLength(50, ErrorMessage = "Fax cannot exceed 50 characters")]
    [Phone(ErrorMessage = "Invalid fax number format")]
    public string? Fax { get; set; }

    [MaxLength(200, ErrorMessage = "LinkedIn cannot exceed 200 characters")]
    public string? LinkedIn { get; set; }

    [MaxLength(200, ErrorMessage = "Twitter cannot exceed 200 characters")]
    public string? Twitter { get; set; }

    [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters")]
    public string? Notes { get; set; }

    // Mailing Address Information
    [MaxLength(500, ErrorMessage = "Mailing address line 1 cannot exceed 500 characters")]
    public string? MailingAddressLine1 { get; set; }

    [MaxLength(500, ErrorMessage = "Mailing address line 2 cannot exceed 500 characters")]
    public string? MailingAddressLine2 { get; set; }

    [MaxLength(100, ErrorMessage = "Mailing city cannot exceed 100 characters")]
    public string? MailingCity { get; set; }

    [MaxLength(100, ErrorMessage = "Mailing state cannot exceed 100 characters")]
    public string? MailingState { get; set; }

    [MaxLength(20, ErrorMessage = "Mailing postal code cannot exceed 20 characters")]
    public string? MailingPostalCode { get; set; }

    [MaxLength(100, ErrorMessage = "Mailing country cannot exceed 100 characters")]
    public string? MailingCountry { get; set; }

    // Other Address Information
    [MaxLength(500, ErrorMessage = "Other address line 1 cannot exceed 500 characters")]
    public string? OtherAddressLine1 { get; set; }

    [MaxLength(500, ErrorMessage = "Other address line 2 cannot exceed 500 characters")]
    public string? OtherAddressLine2 { get; set; }

    [MaxLength(100, ErrorMessage = "Other city cannot exceed 100 characters")]
    public string? OtherCity { get; set; }

    [MaxLength(100, ErrorMessage = "Other state cannot exceed 100 characters")]
    public string? OtherState { get; set; }

    [MaxLength(20, ErrorMessage = "Other postal code cannot exceed 20 characters")]
    public string? OtherPostalCode { get; set; }

    [MaxLength(100, ErrorMessage = "Other country cannot exceed 100 characters")]
    public string? OtherCountry { get; set; }

    public DateTime? Birthdate { get; set; }

    // Preferences
    public bool DoNotCall { get; set; } = false;

    public bool DoNotEmail { get; set; } = false;

    public bool HasOptedOutOfEmail { get; set; } = false;

    public bool IsActive { get; set; } = true;
}
