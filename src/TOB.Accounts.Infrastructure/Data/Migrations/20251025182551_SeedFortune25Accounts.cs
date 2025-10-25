using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TOB.Accounts.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFortune25Accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "AccountNumber", "AccountStatusId", "AccountTypeId", "AnnualRevenue", "BillingAddressLine1", "BillingAddressLine2", "BillingCity", "BillingCountry", "BillingPostalCode", "BillingState", "CreatedAt", "CreatedBy", "Description", "Email", "Fax", "IndustryId", "IsActive", "Name", "NumberOfEmployees", "OwnerId", "ParentAccountId", "Phone", "Rating", "ShippingAddressLine1", "ShippingAddressLine2", "ShippingCity", "ShippingCountry", "ShippingPostalCode", "ShippingState", "TenantId", "UpdatedAt", "UpdatedBy", "Website" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0003-000000000001"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Multinational retail corporation operating hypermarkets, discount stores, and grocery stores", null, null, new Guid("00000000-0000-0000-0002-000000000036"), true, "Walmart Inc.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.walmart.com" },
                    { new Guid("00000000-0000-0000-0003-000000000002"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Global technology company focusing on e-commerce, cloud computing, digital streaming, and artificial intelligence", null, null, new Guid("00000000-0000-0000-0002-000000000013"), true, "Amazon.com Inc.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.amazon.com" },
                    { new Guid("00000000-0000-0000-0003-000000000003"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Diversified health care company offering health insurance and health care services", null, null, new Guid("00000000-0000-0000-0002-000000000021"), true, "UnitedHealth Group Inc.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.unitedhealthgroup.com" },
                    { new Guid("00000000-0000-0000-0003-000000000004"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Technology company designing and manufacturing consumer electronics, software, and online services", null, null, new Guid("00000000-0000-0000-0002-000000000024"), true, "Apple Inc.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.apple.com" },
                    { new Guid("00000000-0000-0000-0003-000000000005"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Healthcare company providing pharmacy services, health insurance, and retail clinics", null, null, new Guid("00000000-0000-0000-0002-000000000021"), true, "CVS Health Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.cvshealth.com" },
                    { new Guid("00000000-0000-0000-0003-000000000006"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Multinational conglomerate holding company with diverse business interests including insurance, railroads, and utilities", null, null, new Guid("00000000-0000-0000-0002-000000000025"), true, "Berkshire Hathaway Inc.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.berkshirehathaway.com" },
                    { new Guid("00000000-0000-0000-0003-000000000007"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Multinational conglomerate and parent company of Google, specializing in internet services and products", null, null, new Guid("00000000-0000-0000-0002-000000000024"), true, "Alphabet Inc.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.abc.xyz" },
                    { new Guid("00000000-0000-0000-0003-000000000008"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "American multinational oil and gas corporation engaged in exploration, production, and refining", null, null, new Guid("00000000-0000-0000-0002-000000000032"), true, "Exxon Mobil Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.exxonmobil.com" },
                    { new Guid("00000000-0000-0000-0003-000000000009"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Healthcare company providing pharmaceuticals and medical supplies distribution services", null, null, new Guid("00000000-0000-0000-0002-000000000033"), true, "McKesson Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.mckesson.com" },
                    { new Guid("00000000-0000-0000-0003-000000000010"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Pharmaceutical sourcing and distribution services company", null, null, new Guid("00000000-0000-0000-0002-000000000033"), true, "Cencora Inc.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.cencora.com" },
                    { new Guid("00000000-0000-0000-0003-000000000011"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Multinational corporation operating membership-only warehouse clubs", null, null, new Guid("00000000-0000-0000-0002-000000000036"), true, "Costco Wholesale Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.costco.com" },
                    { new Guid("00000000-0000-0000-0003-000000000012"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Multinational investment bank and financial services holding company", null, null, new Guid("00000000-0000-0000-0002-000000000007"), true, "JPMorgan Chase & Co.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.jpmorganchase.com" },
                    { new Guid("00000000-0000-0000-0003-000000000013"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Healthcare services company providing pharmaceuticals and medical products distribution", null, null, new Guid("00000000-0000-0000-0002-000000000033"), true, "Cardinal Health Inc.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.cardinalhealth.com" },
                    { new Guid("00000000-0000-0000-0003-000000000014"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Multinational energy corporation engaged in oil and gas exploration, production, and refining", null, null, new Guid("00000000-0000-0000-0002-000000000032"), true, "Chevron Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.chevron.com" },
                    { new Guid("00000000-0000-0000-0003-000000000015"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "American multinational automobile manufacturer designing and selling vehicles and automotive parts", null, null, new Guid("00000000-0000-0000-0002-000000000006"), true, "Ford Motor Company", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.ford.com" },
                    { new Guid("00000000-0000-0000-0003-000000000016"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "American multinational automotive manufacturing company producing vehicles under multiple brands", null, null, new Guid("00000000-0000-0000-0002-000000000006"), true, "General Motors Company", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.gm.com" },
                    { new Guid("00000000-0000-0000-0003-000000000017"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "American petroleum refining, marketing, and transportation company", null, null, new Guid("00000000-0000-0000-0002-000000000032"), true, "Marathon Petroleum Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.marathonpetroleum.com" },
                    { new Guid("00000000-0000-0000-0003-000000000018"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Managed care organization providing health insurance programs and services", null, null, new Guid("00000000-0000-0000-0002-000000000021"), true, "Centene Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.centene.com" },
                    { new Guid("00000000-0000-0000-0003-000000000019"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Multinational investment bank and financial services holding company", null, null, new Guid("00000000-0000-0000-0002-000000000007"), true, "Bank of America Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.bankofamerica.com" },
                    { new Guid("00000000-0000-0000-0003-000000000020"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Energy manufacturing and logistics company specializing in refining, chemicals, and midstream operations", null, null, new Guid("00000000-0000-0000-0002-000000000032"), true, "Phillips 66", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.phillips66.com" },
                    { new Guid("00000000-0000-0000-0003-000000000021"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Global health services company providing insurance and related health care products and services", null, null, new Guid("00000000-0000-0000-0002-000000000025"), true, "The Cigna Group", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.cigna.com" },
                    { new Guid("00000000-0000-0000-0003-000000000022"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Technology corporation developing computer software, consumer electronics, and cloud computing services", null, null, new Guid("00000000-0000-0000-0002-000000000038"), true, "Microsoft Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.microsoft.com" },
                    { new Guid("00000000-0000-0000-0003-000000000023"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Multinational manufacturer and marketer of petroleum-based and low-carbon liquid fuels", null, null, new Guid("00000000-0000-0000-0002-000000000032"), true, "Valero Energy Corporation", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.valero.com" },
                    { new Guid("00000000-0000-0000-0003-000000000024"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Retail company operating supermarkets and multi-department stores", null, null, new Guid("00000000-0000-0000-0002-000000000036"), true, "The Kroger Co.", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.thekrogerco.com" },
                    { new Guid("00000000-0000-0000-0003-000000000025"), null, new Guid("00000000-0000-0000-0001-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, null, null, null, null, null, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Government-sponsored enterprise providing liquidity and stability to the U.S. housing and mortgage markets", null, null, new Guid("00000000-0000-0000-0002-000000000018"), true, "Fannie Mae", null, null, null, null, null, null, null, null, null, null, null, "A8D1B6F8-D91A-4641-AFE1-04FADA37C2AE", null, null, "https://www.fanniemae.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000001"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000002"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000004"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000005"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000006"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000007"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000008"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000009"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000010"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000011"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000012"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000013"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000014"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000015"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000016"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000017"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000018"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000019"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000020"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000021"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000022"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000023"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000024"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000025"));
        }
    }
}
