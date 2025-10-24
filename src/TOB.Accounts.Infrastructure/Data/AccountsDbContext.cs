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
