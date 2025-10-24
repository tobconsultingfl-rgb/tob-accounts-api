using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TOB.Accounts.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnhancedCRMEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Contacts",
                newName: "OtherState");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Contacts",
                newName: "Salutation");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Contacts",
                newName: "OtherCountry");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Contacts",
                newName: "OtherCity");

            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Contacts",
                newName: "OtherAddressLine2");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                table: "Contacts",
                newName: "OtherAddressLine1");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Accounts",
                newName: "ShippingState");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactPhone",
                table: "Accounts",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactName",
                table: "Accounts",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactEmail",
                table: "Accounts",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Accounts",
                newName: "ShippingPostalCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Accounts",
                newName: "ShippingCountry");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Accounts",
                newName: "ShippingCity");

            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Accounts",
                newName: "ShippingAddressLine2");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                table: "Accounts",
                newName: "ShippingAddressLine1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Contacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DoNotCall",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DoNotEmail",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasOptedOutOfEmail",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HomePhone",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryContact",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Contacts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddressLine1",
                table: "Contacts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingAddressLine2",
                table: "Contacts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingCity",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingCountry",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingPostalCode",
                table: "Contacts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MailingState",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Contacts",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherPhone",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherPostalCode",
                table: "Contacts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReportsToId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryEmail",
                table: "Contacts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Contacts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountStatus",
                table: "Accounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "Accounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualRevenue",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddressLine1",
                table: "Accounts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddressLine2",
                table: "Accounts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingCity",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingCountry",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingPostalCode",
                table: "Accounts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingState",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Accounts",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Accounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEmployees",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentAccountId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FirstName_LastName",
                table: "Contacts",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_IsPrimaryContact",
                table: "Contacts",
                column: "IsPrimaryContact");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_OwnerId",
                table: "Contacts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ReportsToId",
                table: "Contacts",
                column: "ReportsToId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountStatus",
                table: "Accounts",
                column: "AccountStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountType",
                table: "Accounts",
                column: "AccountType");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts",
                column: "ParentAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_FirstName_LastName",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_IsPrimaryContact",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_OwnerId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ReportsToId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountStatus",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountType",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DoNotCall",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DoNotEmail",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "HasOptedOutOfEmail",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "HomePhone",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "IsPrimaryContact",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MailingAddressLine1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MailingAddressLine2",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MailingCity",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MailingCountry",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MailingPostalCode",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MailingState",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "OtherPhone",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "OtherPostalCode",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ReportsToId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "SecondaryEmail",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AnnualRevenue",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BillingAddressLine1",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BillingAddressLine2",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BillingCity",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BillingCountry",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BillingPostalCode",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BillingState",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "NumberOfEmployees",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ParentAccountId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Salutation",
                table: "Contacts",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "OtherState",
                table: "Contacts",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "OtherCountry",
                table: "Contacts",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "OtherCity",
                table: "Contacts",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "OtherAddressLine2",
                table: "Contacts",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "OtherAddressLine1",
                table: "Contacts",
                newName: "AddressLine1");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Accounts",
                newName: "PrimaryContactName");

            migrationBuilder.RenameColumn(
                name: "ShippingState",
                table: "Accounts",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "ShippingPostalCode",
                table: "Accounts",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "ShippingCountry",
                table: "Accounts",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "ShippingCity",
                table: "Accounts",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressLine2",
                table: "Accounts",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "ShippingAddressLine1",
                table: "Accounts",
                newName: "AddressLine1");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Accounts",
                newName: "PrimaryContactPhone");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Accounts",
                newName: "PrimaryContactEmail");
        }
    }
}
