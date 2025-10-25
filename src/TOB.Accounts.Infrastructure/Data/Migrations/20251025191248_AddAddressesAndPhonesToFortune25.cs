using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TOB.Accounts.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressesAndPhonesToFortune25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000001"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "702 S.W. 8th St.", "Bentonville", "72716", "AR", "1-479-273-4000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000002"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "410 Terry Avenue North", "Seattle", "98109", "WA", "1-206-266-1000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000003"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "11000 Optum Circle", "Eden Prairie", "55344", "MN", "1-800-328-5979" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000004"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "1 Apple Park Way", "Cupertino", "95014", "CA", "1-408-996-1010" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000005"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "1 CVS Drive", "Woonsocket", "02895", "RI", "1-800-746-7287" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000006"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "3555 Farnam Street", "Omaha", "68131", "NE", "1-402-346-1400" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000007"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "1600 Amphitheatre Parkway", "Mountain View", "94043", "CA", "1-650-253-0000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000008"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "5959 Las Colinas Boulevard", "Irving", "75039", "TX", "1-972-940-6000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000009"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "6535 N. State Highway 161", "Irving", "75039", "TX", "1-972-446-4800" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000010"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "1 West First Avenue", "Conshohocken", "19428", "PA", "1-610-727-7000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000011"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "999 Lake Drive", "Issaquah", "98027", "WA", "1-425-313-8100" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000012"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "383 Madison Avenue", "New York", "10179", "NY", "1-212-270-6000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000013"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "7000 Cardinal Place", "Dublin", "43017", "OH", "1-614-757-5000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000014"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "6001 Bollinger Canyon Road", "San Ramon", "94583", "CA", "1-925-842-1000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000015"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "One American Road", "Dearborn", "48126", "MI", "1-313-322-3000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000016"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "300 Renaissance Center", "Detroit", "48265", "MI", "1-313-556-5000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000017"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "539 South Main Street", "Findlay", "45840", "OH", "1-419-422-2121" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000018"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "7700 Forsyth Boulevard", "St. Louis", "63105", "MO", "1-314-725-4477" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000019"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "100 North Tryon Street", "Charlotte", "28255", "NC", "1-704-386-8486" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000020"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "2331 Citywest Blvd.", "Houston", "77042", "TX", "1-281-293-6600" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000021"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "900 Cottage Grove Road", "Bloomfield", "06002", "CT", "1-860-226-6000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000022"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "One Microsoft Way", "Redmond", "98052", "WA", "1-425-882-8080" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000023"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "One Valero Way", "San Antonio", "78249", "TX", "1-210-345-2000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000024"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "1014 Vine Street", "Cincinnati", "45202", "OH", "1-513-762-4000" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000025"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { "1100 15th Street, NW", "Washington", "20005", "DC", "1-800-232-6643" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000001"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000002"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000003"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000004"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000005"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000006"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000007"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000008"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000009"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000010"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000011"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000012"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000013"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000014"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000015"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000016"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000017"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000018"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000019"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000020"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000021"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000022"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000023"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000024"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: new Guid("00000000-0000-0000-0003-000000000025"),
                columns: new[] { "BillingAddressLine1", "BillingCity", "BillingPostalCode", "BillingState", "Phone" },
                values: new object[] { null, null, null, null, null });
        }
    }
}
