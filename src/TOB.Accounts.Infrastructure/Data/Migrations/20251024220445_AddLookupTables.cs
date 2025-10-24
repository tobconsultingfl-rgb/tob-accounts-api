using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TOB.Accounts.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountStatus",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountType",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountStatusId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountTypeId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IndustryId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountStatuses",
                columns: table => new
                {
                    AccountStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatuses", x => x.AccountStatusId);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    AccountTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.AccountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    IndustryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.IndustryId);
                });

            migrationBuilder.InsertData(
                table: "AccountStatuses",
                columns: new[] { "AccountStatusId", "Description", "DisplayOrder", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0001-000000000001"), "Active account with ongoing business", 1, true, "Active" },
                    { new Guid("00000000-0000-0000-0001-000000000002"), "Inactive account with no current business", 2, true, "Inactive" },
                    { new Guid("00000000-0000-0000-0001-000000000003"), "Account temporarily on hold", 3, true, "On Hold" },
                    { new Guid("00000000-0000-0000-0001-000000000004"), "Closed account", 4, true, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Description", "DisplayOrder", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Existing customer with active business", 1, true, "Customer" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Potential customer", 2, true, "Prospect" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Business partner", 3, true, "Partner" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Supplier or vendor", 4, true, "Vendor" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Business competitor", 5, true, "Competitor" }
                });

            migrationBuilder.InsertData(
                table: "Industries",
                columns: new[] { "IndustryId", "Description", "DisplayOrder", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0002-000000000001"), "Software, hardware, and IT services", 1, true, "Technology" },
                    { new Guid("00000000-0000-0000-0002-000000000002"), "Medical and healthcare services", 2, true, "Healthcare" },
                    { new Guid("00000000-0000-0000-0002-000000000003"), "Banking, insurance, and financial institutions", 3, true, "Financial Services" },
                    { new Guid("00000000-0000-0000-0002-000000000004"), "Production and manufacturing", 4, true, "Manufacturing" },
                    { new Guid("00000000-0000-0000-0002-000000000005"), "Retail and e-commerce", 5, true, "Retail" },
                    { new Guid("00000000-0000-0000-0002-000000000006"), "Educational institutions and services", 6, true, "Education" },
                    { new Guid("00000000-0000-0000-0002-000000000007"), "Real estate and property management", 7, true, "Real Estate" },
                    { new Guid("00000000-0000-0000-0002-000000000008"), "Professional consulting services", 8, true, "Consulting" },
                    { new Guid("00000000-0000-0000-0002-000000000009"), "Transportation and logistics", 9, true, "Transportation" },
                    { new Guid("00000000-0000-0000-0002-000000000010"), "Energy and utilities", 10, true, "Energy" },
                    { new Guid("00000000-0000-0000-0002-000000000011"), "Media, entertainment, and publishing", 11, true, "Media & Entertainment" },
                    { new Guid("00000000-0000-0000-0002-000000000012"), "Agriculture and farming", 12, true, "Agriculture" },
                    { new Guid("00000000-0000-0000-0002-000000000013"), "Telecommunications services", 13, true, "Telecommunications" },
                    { new Guid("00000000-0000-0000-0002-000000000014"), "Hotels, restaurants, and hospitality", 14, true, "Hospitality" },
                    { new Guid("00000000-0000-0000-0002-000000000015"), "Other industries", 99, true, "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountStatusId",
                table: "Accounts",
                column: "AccountStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_IndustryId",
                table: "Accounts",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatuses_DisplayOrder",
                table: "AccountStatuses",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatuses_IsActive",
                table: "AccountStatuses",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatuses_Name",
                table: "AccountStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_DisplayOrder",
                table: "AccountTypes",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_IsActive",
                table: "AccountTypes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_Name",
                table: "AccountTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Industries_DisplayOrder",
                table: "Industries",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Industries_IsActive",
                table: "Industries",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Industries_Name",
                table: "Industries",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountStatuses_AccountStatusId",
                table: "Accounts",
                column: "AccountStatusId",
                principalTable: "AccountStatuses",
                principalColumn: "AccountStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountTypes_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId",
                principalTable: "AccountTypes",
                principalColumn: "AccountTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Industries_IndustryId",
                table: "Accounts",
                column: "IndustryId",
                principalTable: "Industries",
                principalColumn: "IndustryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountStatuses_AccountStatusId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountTypes_AccountTypeId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Industries_IndustryId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountStatuses");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountStatusId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_IndustryId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountStatusId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountTypeId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Accounts");

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

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountStatus",
                table: "Accounts",
                column: "AccountStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountType",
                table: "Accounts",
                column: "AccountType");
        }
    }
}
