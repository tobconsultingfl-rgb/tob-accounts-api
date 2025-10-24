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
                    { new Guid("00000000-0000-0000-0002-000000000001"), "Accounting and bookkeeping services", 1, true, "Accounting" },
                    { new Guid("00000000-0000-0000-0002-000000000002"), "Advertising agencies and marketing services", 2, true, "Advertising & Marketing" },
                    { new Guid("00000000-0000-0000-0002-000000000003"), "Aerospace and defense industries", 3, true, "Aerospace & Defense" },
                    { new Guid("00000000-0000-0000-0002-000000000004"), "Agriculture, farming, and agribusiness", 4, true, "Agriculture" },
                    { new Guid("00000000-0000-0000-0002-000000000005"), "Architecture and engineering services", 5, true, "Architecture & Engineering" },
                    { new Guid("00000000-0000-0000-0002-000000000006"), "Automotive manufacturing and services", 6, true, "Automotive" },
                    { new Guid("00000000-0000-0000-0002-000000000007"), "Banking and financial institutions", 7, true, "Banking" },
                    { new Guid("00000000-0000-0000-0002-000000000008"), "Biotechnology and life sciences", 8, true, "Biotechnology" },
                    { new Guid("00000000-0000-0000-0002-000000000009"), "Chemical manufacturing and distribution", 9, true, "Chemicals" },
                    { new Guid("00000000-0000-0000-0002-000000000010"), "Construction and building services", 10, true, "Construction" },
                    { new Guid("00000000-0000-0000-0002-000000000011"), "Management and professional consulting", 11, true, "Consulting" },
                    { new Guid("00000000-0000-0000-0002-000000000012"), "Consumer products and packaged goods", 12, true, "Consumer Goods" },
                    { new Guid("00000000-0000-0000-0002-000000000013"), "Online retail and e-commerce platforms", 13, true, "E-commerce" },
                    { new Guid("00000000-0000-0000-0002-000000000014"), "Educational institutions and services", 14, true, "Education" },
                    { new Guid("00000000-0000-0000-0002-000000000015"), "Energy production and utility services", 15, true, "Energy & Utilities" },
                    { new Guid("00000000-0000-0000-0002-000000000016"), "Entertainment and event services", 16, true, "Entertainment" },
                    { new Guid("00000000-0000-0000-0002-000000000017"), "Environmental and sustainability services", 17, true, "Environmental Services" },
                    { new Guid("00000000-0000-0000-0002-000000000018"), "Financial planning and investment services", 18, true, "Financial Services" },
                    { new Guid("00000000-0000-0000-0002-000000000019"), "Food and beverage production and services", 19, true, "Food & Beverage" },
                    { new Guid("00000000-0000-0000-0002-000000000020"), "Government agencies and public sector", 20, true, "Government" },
                    { new Guid("00000000-0000-0000-0002-000000000021"), "Healthcare providers and medical services", 21, true, "Healthcare" },
                    { new Guid("00000000-0000-0000-0002-000000000022"), "Hotels, resorts, and hospitality services", 22, true, "Hospitality" },
                    { new Guid("00000000-0000-0000-0002-000000000023"), "HR services and staffing agencies", 23, true, "Human Resources" },
                    { new Guid("00000000-0000-0000-0002-000000000024"), "IT services and technology consulting", 24, true, "Information Technology" },
                    { new Guid("00000000-0000-0000-0002-000000000025"), "Insurance providers and brokers", 25, true, "Insurance" },
                    { new Guid("00000000-0000-0000-0002-000000000026"), "Law firms and legal services", 26, true, "Legal Services" },
                    { new Guid("00000000-0000-0000-0002-000000000027"), "Logistics and supply chain management", 27, true, "Logistics & Supply Chain" },
                    { new Guid("00000000-0000-0000-0002-000000000028"), "Industrial and product manufacturing", 28, true, "Manufacturing" },
                    { new Guid("00000000-0000-0000-0002-000000000029"), "Media, publishing, and broadcasting", 29, true, "Media & Publishing" },
                    { new Guid("00000000-0000-0000-0002-000000000030"), "Mining, metals, and mineral extraction", 30, true, "Mining & Metals" },
                    { new Guid("00000000-0000-0000-0002-000000000031"), "Non-profit organizations and charities", 31, true, "Non-Profit" },
                    { new Guid("00000000-0000-0000-0002-000000000032"), "Oil and gas exploration and production", 32, true, "Oil & Gas" },
                    { new Guid("00000000-0000-0000-0002-000000000033"), "Pharmaceutical development and distribution", 33, true, "Pharmaceuticals" },
                    { new Guid("00000000-0000-0000-0002-000000000034"), "Real estate and property management", 34, true, "Real Estate" },
                    { new Guid("00000000-0000-0000-0002-000000000035"), "Restaurants and food service", 35, true, "Restaurants" },
                    { new Guid("00000000-0000-0000-0002-000000000036"), "Retail stores and shops", 36, true, "Retail" },
                    { new Guid("00000000-0000-0000-0002-000000000037"), "Security services and products", 37, true, "Security" },
                    { new Guid("00000000-0000-0000-0002-000000000038"), "Software development and SaaS", 38, true, "Software" },
                    { new Guid("00000000-0000-0000-0002-000000000039"), "Sports, fitness, and recreational services", 39, true, "Sports & Recreation" },
                    { new Guid("00000000-0000-0000-0002-000000000040"), "Telecommunications and internet services", 40, true, "Telecommunications" },
                    { new Guid("00000000-0000-0000-0002-000000000041"), "Textiles, apparel, and fashion", 41, true, "Textiles & Apparel" },
                    { new Guid("00000000-0000-0000-0002-000000000042"), "Transportation and shipping services", 42, true, "Transportation" },
                    { new Guid("00000000-0000-0000-0002-000000000043"), "Travel agencies and tourism services", 43, true, "Travel & Tourism" },
                    { new Guid("00000000-0000-0000-0002-000000000044"), "Wholesale and distribution services", 44, true, "Wholesale & Distribution" },
                    { new Guid("00000000-0000-0000-0002-000000000099"), "Other industries not listed", 99, true, "Other" }
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
