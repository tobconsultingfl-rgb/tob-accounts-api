using Microsoft.EntityFrameworkCore;

namespace TOB.Accounts.Infrastructure.Data;

public class AccountsDbContext : DbContext
{
    public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<AccountDocument> AccountDocuments { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<AccountStatus> AccountStatuses { get; set; }
    public DbSet<Industry> Industries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Account entity
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.HasIndex(e => e.TenantId)
                .HasDatabaseName("IX_Accounts_TenantId");

            entity.HasIndex(e => new { e.TenantId, e.Name })
                .HasDatabaseName("IX_Accounts_TenantId_Name");

            entity.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_Accounts_IsActive");

            entity.HasIndex(e => e.AccountTypeId)
                .HasDatabaseName("IX_Accounts_AccountTypeId");

            entity.HasIndex(e => e.AccountStatusId)
                .HasDatabaseName("IX_Accounts_AccountStatusId");

            entity.HasIndex(e => e.IndustryId)
                .HasDatabaseName("IX_Accounts_IndustryId");

            entity.HasIndex(e => e.OwnerId)
                .HasDatabaseName("IX_Accounts_OwnerId");

            entity.HasIndex(e => e.ParentAccountId)
                .HasDatabaseName("IX_Accounts_ParentAccountId");

            entity.HasIndex(e => e.AccountNumber)
                .HasDatabaseName("IX_Accounts_AccountNumber");

            // Configure one-to-many relationship: Account -> Contacts
            entity.HasMany(a => a.Contacts)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure default values
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);
        });

        // Configure Contact entity
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId);

            entity.HasIndex(e => e.TenantId)
                .HasDatabaseName("IX_Contacts_TenantId");

            entity.HasIndex(e => e.AccountId)
                .HasDatabaseName("IX_Contacts_AccountId");

            entity.HasIndex(e => new { e.TenantId, e.AccountId })
                .HasDatabaseName("IX_Contacts_TenantId_AccountId");

            entity.HasIndex(e => e.Email)
                .HasDatabaseName("IX_Contacts_Email");

            entity.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_Contacts_IsActive");

            entity.HasIndex(e => new { e.FirstName, e.LastName })
                .HasDatabaseName("IX_Contacts_FirstName_LastName");

            entity.HasIndex(e => e.IsPrimaryContact)
                .HasDatabaseName("IX_Contacts_IsPrimaryContact");

            entity.HasIndex(e => e.OwnerId)
                .HasDatabaseName("IX_Contacts_OwnerId");

            entity.HasIndex(e => e.ReportsToId)
                .HasDatabaseName("IX_Contacts_ReportsToId");

            // Configure default values
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.IsPrimaryContact)
                .HasDefaultValue(false);

            entity.Property(e => e.DoNotCall)
                .HasDefaultValue(false);

            entity.Property(e => e.DoNotEmail)
                .HasDefaultValue(false);

            entity.Property(e => e.HasOptedOutOfEmail)
                .HasDefaultValue(false);
        });

        // Configure AccountDocument entity
        modelBuilder.Entity<AccountDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId);

            entity.HasIndex(e => e.TenantId)
                .HasDatabaseName("IX_AccountDocuments_TenantId");

            entity.HasIndex(e => e.AccountId)
                .HasDatabaseName("IX_AccountDocuments_AccountId");

            entity.HasIndex(e => new { e.TenantId, e.AccountId })
                .HasDatabaseName("IX_AccountDocuments_TenantId_AccountId");

            entity.HasIndex(e => e.Category)
                .HasDatabaseName("IX_AccountDocuments_Category");

            entity.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_AccountDocuments_IsActive");

            entity.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_AccountDocuments_CreatedAt");

            // Configure one-to-many relationship: Account -> AccountDocuments
            entity.HasOne(d => d.Account)
                .WithMany(a => a.Documents)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure default values
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);
        });

        // Configure AccountType entity
        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.AccountTypeId);

            entity.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("IX_AccountTypes_Name");

            entity.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_AccountTypes_IsActive");

            entity.HasIndex(e => e.DisplayOrder)
                .HasDatabaseName("IX_AccountTypes_DisplayOrder");

            // Configure one-to-many relationship: AccountType -> Accounts
            entity.HasMany(at => at.Accounts)
                .WithOne(a => a.AccountType)
                .HasForeignKey(a => a.AccountTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure AccountStatus entity
        modelBuilder.Entity<AccountStatus>(entity =>
        {
            entity.HasKey(e => e.AccountStatusId);

            entity.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("IX_AccountStatuses_Name");

            entity.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_AccountStatuses_IsActive");

            entity.HasIndex(e => e.DisplayOrder)
                .HasDatabaseName("IX_AccountStatuses_DisplayOrder");

            // Configure one-to-many relationship: AccountStatus -> Accounts
            entity.HasMany(ast => ast.Accounts)
                .WithOne(a => a.AccountStatus)
                .HasForeignKey(a => a.AccountStatusId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Industry entity
        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.IndustryId);

            entity.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("IX_Industries_Name");

            entity.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_Industries_IsActive");

            entity.HasIndex(e => e.DisplayOrder)
                .HasDatabaseName("IX_Industries_DisplayOrder");

            // Configure one-to-many relationship: Industry -> Accounts
            entity.HasMany(i => i.Accounts)
                .WithOne(a => a.Industry)
                .HasForeignKey(a => a.IndustryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Seed data for lookup tables
        SeedLookupData(modelBuilder);

        // Seed Fortune 25 accounts
        SeedAccountData(modelBuilder);

        // Global query filter for soft deletes
        modelBuilder.Entity<Account>().HasQueryFilter(a => a.IsActive);
        modelBuilder.Entity<Contact>().HasQueryFilter(c => c.IsActive);
        modelBuilder.Entity<AccountDocument>().HasQueryFilter(d => d.IsActive);
        modelBuilder.Entity<AccountType>().HasQueryFilter(at => at.IsActive);
        modelBuilder.Entity<AccountStatus>().HasQueryFilter(ast => ast.IsActive);
        modelBuilder.Entity<Industry>().HasQueryFilter(i => i.IsActive);
    }

    private void SeedLookupData(ModelBuilder modelBuilder)
    {
        // Seed AccountTypes
        modelBuilder.Entity<AccountType>().HasData(
            new AccountType { AccountTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Customer", Description = "Existing customer with active business", IsActive = true, DisplayOrder = 1 },
            new AccountType { AccountTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Prospect", Description = "Potential customer", IsActive = true, DisplayOrder = 2 },
            new AccountType { AccountTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Partner", Description = "Business partner", IsActive = true, DisplayOrder = 3 },
            new AccountType { AccountTypeId = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Vendor", Description = "Supplier or vendor", IsActive = true, DisplayOrder = 4 },
            new AccountType { AccountTypeId = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Competitor", Description = "Business competitor", IsActive = true, DisplayOrder = 5 }
        );

        // Seed AccountStatuses
        modelBuilder.Entity<AccountStatus>().HasData(
            new AccountStatus { AccountStatusId = Guid.Parse("00000000-0000-0000-0001-000000000001"), Name = "Active", Description = "Active account with ongoing business", IsActive = true, DisplayOrder = 1 },
            new AccountStatus { AccountStatusId = Guid.Parse("00000000-0000-0000-0001-000000000002"), Name = "Inactive", Description = "Inactive account with no current business", IsActive = true, DisplayOrder = 2 },
            new AccountStatus { AccountStatusId = Guid.Parse("00000000-0000-0000-0001-000000000003"), Name = "On Hold", Description = "Account temporarily on hold", IsActive = true, DisplayOrder = 3 },
            new AccountStatus { AccountStatusId = Guid.Parse("00000000-0000-0000-0001-000000000004"), Name = "Closed", Description = "Closed account", IsActive = true, DisplayOrder = 4 }
        );

        // Seed Industries
        modelBuilder.Entity<Industry>().HasData(
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000001"), Name = "Accounting", Description = "Accounting and bookkeeping services", IsActive = true, DisplayOrder = 1 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000002"), Name = "Advertising & Marketing", Description = "Advertising agencies and marketing services", IsActive = true, DisplayOrder = 2 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000003"), Name = "Aerospace & Defense", Description = "Aerospace and defense industries", IsActive = true, DisplayOrder = 3 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000004"), Name = "Agriculture", Description = "Agriculture, farming, and agribusiness", IsActive = true, DisplayOrder = 4 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000005"), Name = "Architecture & Engineering", Description = "Architecture and engineering services", IsActive = true, DisplayOrder = 5 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000006"), Name = "Automotive", Description = "Automotive manufacturing and services", IsActive = true, DisplayOrder = 6 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000007"), Name = "Banking", Description = "Banking and financial institutions", IsActive = true, DisplayOrder = 7 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000008"), Name = "Biotechnology", Description = "Biotechnology and life sciences", IsActive = true, DisplayOrder = 8 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000009"), Name = "Chemicals", Description = "Chemical manufacturing and distribution", IsActive = true, DisplayOrder = 9 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000010"), Name = "Construction", Description = "Construction and building services", IsActive = true, DisplayOrder = 10 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000011"), Name = "Consulting", Description = "Management and professional consulting", IsActive = true, DisplayOrder = 11 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000012"), Name = "Consumer Goods", Description = "Consumer products and packaged goods", IsActive = true, DisplayOrder = 12 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000013"), Name = "E-commerce", Description = "Online retail and e-commerce platforms", IsActive = true, DisplayOrder = 13 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000014"), Name = "Education", Description = "Educational institutions and services", IsActive = true, DisplayOrder = 14 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000015"), Name = "Energy & Utilities", Description = "Energy production and utility services", IsActive = true, DisplayOrder = 15 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000016"), Name = "Entertainment", Description = "Entertainment and event services", IsActive = true, DisplayOrder = 16 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000017"), Name = "Environmental Services", Description = "Environmental and sustainability services", IsActive = true, DisplayOrder = 17 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000018"), Name = "Financial Services", Description = "Financial planning and investment services", IsActive = true, DisplayOrder = 18 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000019"), Name = "Food & Beverage", Description = "Food and beverage production and services", IsActive = true, DisplayOrder = 19 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000020"), Name = "Government", Description = "Government agencies and public sector", IsActive = true, DisplayOrder = 20 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000021"), Name = "Healthcare", Description = "Healthcare providers and medical services", IsActive = true, DisplayOrder = 21 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000022"), Name = "Hospitality", Description = "Hotels, resorts, and hospitality services", IsActive = true, DisplayOrder = 22 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000023"), Name = "Human Resources", Description = "HR services and staffing agencies", IsActive = true, DisplayOrder = 23 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000024"), Name = "Information Technology", Description = "IT services and technology consulting", IsActive = true, DisplayOrder = 24 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000025"), Name = "Insurance", Description = "Insurance providers and brokers", IsActive = true, DisplayOrder = 25 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000026"), Name = "Legal Services", Description = "Law firms and legal services", IsActive = true, DisplayOrder = 26 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000027"), Name = "Logistics & Supply Chain", Description = "Logistics and supply chain management", IsActive = true, DisplayOrder = 27 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000028"), Name = "Manufacturing", Description = "Industrial and product manufacturing", IsActive = true, DisplayOrder = 28 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000029"), Name = "Media & Publishing", Description = "Media, publishing, and broadcasting", IsActive = true, DisplayOrder = 29 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000030"), Name = "Mining & Metals", Description = "Mining, metals, and mineral extraction", IsActive = true, DisplayOrder = 30 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000031"), Name = "Non-Profit", Description = "Non-profit organizations and charities", IsActive = true, DisplayOrder = 31 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000032"), Name = "Oil & Gas", Description = "Oil and gas exploration and production", IsActive = true, DisplayOrder = 32 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000033"), Name = "Pharmaceuticals", Description = "Pharmaceutical development and distribution", IsActive = true, DisplayOrder = 33 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000034"), Name = "Real Estate", Description = "Real estate and property management", IsActive = true, DisplayOrder = 34 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000035"), Name = "Restaurants", Description = "Restaurants and food service", IsActive = true, DisplayOrder = 35 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000036"), Name = "Retail", Description = "Retail stores and shops", IsActive = true, DisplayOrder = 36 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000037"), Name = "Security", Description = "Security services and products", IsActive = true, DisplayOrder = 37 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000038"), Name = "Software", Description = "Software development and SaaS", IsActive = true, DisplayOrder = 38 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000039"), Name = "Sports & Recreation", Description = "Sports, fitness, and recreational services", IsActive = true, DisplayOrder = 39 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000040"), Name = "Telecommunications", Description = "Telecommunications and internet services", IsActive = true, DisplayOrder = 40 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000041"), Name = "Textiles & Apparel", Description = "Textiles, apparel, and fashion", IsActive = true, DisplayOrder = 41 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000042"), Name = "Transportation", Description = "Transportation and shipping services", IsActive = true, DisplayOrder = 42 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000043"), Name = "Travel & Tourism", Description = "Travel agencies and tourism services", IsActive = true, DisplayOrder = 43 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000044"), Name = "Wholesale & Distribution", Description = "Wholesale and distribution services", IsActive = true, DisplayOrder = 44 },
            new Industry { IndustryId = Guid.Parse("00000000-0000-0000-0002-000000000099"), Name = "Other", Description = "Other industries not listed", IsActive = true, DisplayOrder = 99 }
        );
    }

    private void SeedAccountData(ModelBuilder modelBuilder)
    {
        var tenantId = "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE";
        var customerTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"); // Customer
        var activeStatusId = Guid.Parse("00000000-0000-0000-0001-000000000001"); // Active

        // Industry IDs from seed data
        var retailIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000036");
        var ecommerceIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000013");
        var healthcareIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000021");
        var itIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000024");
        var insuranceIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000025");
        var oilGasIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000032");
        var pharmaceuticalsIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000033");
        var bankingIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000007");
        var automotiveIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000006");
        var financialServicesIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000018");
        var softwareIndustryId = Guid.Parse("00000000-0000-0000-0002-000000000038");

        // Seed Fortune 25 Companies
        modelBuilder.Entity<Account>().HasData(
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000001"),
                TenantId = tenantId,
                Name = "Walmart Inc.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = retailIndustryId,
                Website = "https://www.walmart.com",
                Description = "Multinational retail corporation operating hypermarkets, discount stores, and grocery stores",
                BillingAddressLine1 = "702 S.W. 8th St.",
                BillingCity = "Bentonville",
                BillingState = "AR",
                BillingPostalCode = "72716",
                Phone = "1-479-273-4000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000002"),
                TenantId = tenantId,
                Name = "Amazon.com Inc.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = ecommerceIndustryId,
                Website = "https://www.amazon.com",
                Description = "Global technology company focusing on e-commerce, cloud computing, digital streaming, and artificial intelligence",
                BillingAddressLine1 = "410 Terry Avenue North",
                BillingCity = "Seattle",
                BillingState = "WA",
                BillingPostalCode = "98109",
                Phone = "1-206-266-1000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000003"),
                TenantId = tenantId,
                Name = "UnitedHealth Group Inc.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = healthcareIndustryId,
                Website = "https://www.unitedhealthgroup.com",
                Description = "Diversified health care company offering health insurance and health care services",
                BillingAddressLine1 = "11000 Optum Circle",
                BillingCity = "Eden Prairie",
                BillingState = "MN",
                BillingPostalCode = "55344",
                Phone = "1-800-328-5979",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000004"),
                TenantId = tenantId,
                Name = "Apple Inc.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = itIndustryId,
                Website = "https://www.apple.com",
                Description = "Technology company designing and manufacturing consumer electronics, software, and online services",
                BillingAddressLine1 = "1 Apple Park Way",
                BillingCity = "Cupertino",
                BillingState = "CA",
                BillingPostalCode = "95014",
                Phone = "1-408-996-1010",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000005"),
                TenantId = tenantId,
                Name = "CVS Health Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = healthcareIndustryId,
                Website = "https://www.cvshealth.com",
                Description = "Healthcare company providing pharmacy services, health insurance, and retail clinics",
                BillingAddressLine1 = "1 CVS Drive",
                BillingCity = "Woonsocket",
                BillingState = "RI",
                BillingPostalCode = "02895",
                Phone = "1-800-746-7287",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000006"),
                TenantId = tenantId,
                Name = "Berkshire Hathaway Inc.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = insuranceIndustryId,
                Website = "https://www.berkshirehathaway.com",
                Description = "Multinational conglomerate holding company with diverse business interests including insurance, railroads, and utilities",
                BillingAddressLine1 = "3555 Farnam Street",
                BillingCity = "Omaha",
                BillingState = "NE",
                BillingPostalCode = "68131",
                Phone = "1-402-346-1400",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000007"),
                TenantId = tenantId,
                Name = "Alphabet Inc.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = itIndustryId,
                Website = "https://www.abc.xyz",
                Description = "Multinational conglomerate and parent company of Google, specializing in internet services and products",
                BillingAddressLine1 = "1600 Amphitheatre Parkway",
                BillingCity = "Mountain View",
                BillingState = "CA",
                BillingPostalCode = "94043",
                Phone = "1-650-253-0000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000008"),
                TenantId = tenantId,
                Name = "Exxon Mobil Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = oilGasIndustryId,
                Website = "https://www.exxonmobil.com",
                Description = "American multinational oil and gas corporation engaged in exploration, production, and refining",
                BillingAddressLine1 = "5959 Las Colinas Boulevard",
                BillingCity = "Irving",
                BillingState = "TX",
                BillingPostalCode = "75039",
                Phone = "1-972-940-6000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000009"),
                TenantId = tenantId,
                Name = "McKesson Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = pharmaceuticalsIndustryId,
                Website = "https://www.mckesson.com",
                Description = "Healthcare company providing pharmaceuticals and medical supplies distribution services",
                BillingAddressLine1 = "6535 N. State Highway 161",
                BillingCity = "Irving",
                BillingState = "TX",
                BillingPostalCode = "75039",
                Phone = "1-972-446-4800",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000010"),
                TenantId = tenantId,
                Name = "Cencora Inc.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = pharmaceuticalsIndustryId,
                Website = "https://www.cencora.com",
                Description = "Pharmaceutical sourcing and distribution services company",
                BillingAddressLine1 = "1 West First Avenue",
                BillingCity = "Conshohocken",
                BillingState = "PA",
                BillingPostalCode = "19428",
                Phone = "1-610-727-7000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000011"),
                TenantId = tenantId,
                Name = "Costco Wholesale Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = retailIndustryId,
                Website = "https://www.costco.com",
                Description = "Multinational corporation operating membership-only warehouse clubs",
                BillingAddressLine1 = "999 Lake Drive",
                BillingCity = "Issaquah",
                BillingState = "WA",
                BillingPostalCode = "98027",
                Phone = "1-425-313-8100",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000012"),
                TenantId = tenantId,
                Name = "JPMorgan Chase & Co.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = bankingIndustryId,
                Website = "https://www.jpmorganchase.com",
                Description = "Multinational investment bank and financial services holding company",
                BillingAddressLine1 = "383 Madison Avenue",
                BillingCity = "New York",
                BillingState = "NY",
                BillingPostalCode = "10179",
                Phone = "1-212-270-6000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000013"),
                TenantId = tenantId,
                Name = "Cardinal Health Inc.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = pharmaceuticalsIndustryId,
                Website = "https://www.cardinalhealth.com",
                Description = "Healthcare services company providing pharmaceuticals and medical products distribution",
                BillingAddressLine1 = "7000 Cardinal Place",
                BillingCity = "Dublin",
                BillingState = "OH",
                BillingPostalCode = "43017",
                Phone = "1-614-757-5000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000014"),
                TenantId = tenantId,
                Name = "Chevron Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = oilGasIndustryId,
                Website = "https://www.chevron.com",
                Description = "Multinational energy corporation engaged in oil and gas exploration, production, and refining",
                BillingAddressLine1 = "6001 Bollinger Canyon Road",
                BillingCity = "San Ramon",
                BillingState = "CA",
                BillingPostalCode = "94583",
                Phone = "1-925-842-1000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000015"),
                TenantId = tenantId,
                Name = "Ford Motor Company",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = automotiveIndustryId,
                Website = "https://www.ford.com",
                Description = "American multinational automobile manufacturer designing and selling vehicles and automotive parts",
                BillingAddressLine1 = "One American Road",
                BillingCity = "Dearborn",
                BillingState = "MI",
                BillingPostalCode = "48126",
                Phone = "1-313-322-3000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000016"),
                TenantId = tenantId,
                Name = "General Motors Company",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = automotiveIndustryId,
                Website = "https://www.gm.com",
                Description = "American multinational automotive manufacturing company producing vehicles under multiple brands",
                BillingAddressLine1 = "300 Renaissance Center",
                BillingCity = "Detroit",
                BillingState = "MI",
                BillingPostalCode = "48265",
                Phone = "1-313-556-5000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000017"),
                TenantId = tenantId,
                Name = "Marathon Petroleum Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = oilGasIndustryId,
                Website = "https://www.marathonpetroleum.com",
                Description = "American petroleum refining, marketing, and transportation company",
                BillingAddressLine1 = "539 South Main Street",
                BillingCity = "Findlay",
                BillingState = "OH",
                BillingPostalCode = "45840",
                Phone = "1-419-422-2121",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000018"),
                TenantId = tenantId,
                Name = "Centene Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = healthcareIndustryId,
                Website = "https://www.centene.com",
                Description = "Managed care organization providing health insurance programs and services",
                BillingAddressLine1 = "7700 Forsyth Boulevard",
                BillingCity = "St. Louis",
                BillingState = "MO",
                BillingPostalCode = "63105",
                Phone = "1-314-725-4477",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000019"),
                TenantId = tenantId,
                Name = "Bank of America Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = bankingIndustryId,
                Website = "https://www.bankofamerica.com",
                Description = "Multinational investment bank and financial services holding company",
                BillingAddressLine1 = "100 North Tryon Street",
                BillingCity = "Charlotte",
                BillingState = "NC",
                BillingPostalCode = "28255",
                Phone = "1-704-386-8486",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000020"),
                TenantId = tenantId,
                Name = "Phillips 66",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = oilGasIndustryId,
                Website = "https://www.phillips66.com",
                Description = "Energy manufacturing and logistics company specializing in refining, chemicals, and midstream operations",
                BillingAddressLine1 = "2331 Citywest Blvd.",
                BillingCity = "Houston",
                BillingState = "TX",
                BillingPostalCode = "77042",
                Phone = "1-281-293-6600",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000021"),
                TenantId = tenantId,
                Name = "The Cigna Group",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = insuranceIndustryId,
                Website = "https://www.cigna.com",
                Description = "Global health services company providing insurance and related health care products and services",
                BillingAddressLine1 = "900 Cottage Grove Road",
                BillingCity = "Bloomfield",
                BillingState = "CT",
                BillingPostalCode = "06002",
                Phone = "1-860-226-6000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000022"),
                TenantId = tenantId,
                Name = "Microsoft Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = softwareIndustryId,
                Website = "https://www.microsoft.com",
                Description = "Technology corporation developing computer software, consumer electronics, and cloud computing services",
                BillingAddressLine1 = "One Microsoft Way",
                BillingCity = "Redmond",
                BillingState = "WA",
                BillingPostalCode = "98052",
                Phone = "1-425-882-8080",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000023"),
                TenantId = tenantId,
                Name = "Valero Energy Corporation",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = oilGasIndustryId,
                Website = "https://www.valero.com",
                Description = "Multinational manufacturer and marketer of petroleum-based and low-carbon liquid fuels",
                BillingAddressLine1 = "One Valero Way",
                BillingCity = "San Antonio",
                BillingState = "TX",
                BillingPostalCode = "78249",
                Phone = "1-210-345-2000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000024"),
                TenantId = tenantId,
                Name = "The Kroger Co.",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = retailIndustryId,
                Website = "https://www.thekrogerco.com",
                Description = "Retail company operating supermarkets and multi-department stores",
                BillingAddressLine1 = "1014 Vine Street",
                BillingCity = "Cincinnati",
                BillingState = "OH",
                BillingPostalCode = "45202",
                Phone = "1-513-762-4000",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0003-000000000025"),
                TenantId = tenantId,
                Name = "Fannie Mae",
                AccountTypeId = customerTypeId,
                AccountStatusId = activeStatusId,
                IndustryId = financialServicesIndustryId,
                Website = "https://www.fanniemae.com",
                Description = "Government-sponsored enterprise providing liquidity and stability to the U.S. housing and mortgage markets",
                BillingAddressLine1 = "1100 15th Street, NW",
                BillingCity = "Washington",
                BillingState = "DC",
                BillingPostalCode = "20005",
                Phone = "1-800-232-6643",
                IsActive = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Property("CreatedAt").CurrentValue == null)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
