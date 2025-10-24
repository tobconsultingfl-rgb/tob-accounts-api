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

            entity.HasIndex(e => e.AccountType)
                .HasDatabaseName("IX_Accounts_AccountType");

            entity.HasIndex(e => e.AccountStatus)
                .HasDatabaseName("IX_Accounts_AccountStatus");

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

        // Global query filter for soft deletes
        modelBuilder.Entity<Account>().HasQueryFilter(a => a.IsActive);
        modelBuilder.Entity<Contact>().HasQueryFilter(c => c.IsActive);
        modelBuilder.Entity<AccountDocument>().HasQueryFilter(d => d.IsActive);
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
