using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TOB.Accounts.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BlobUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    BlobContainer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BlobName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_AccountDocuments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDocuments_AccountId",
                table: "AccountDocuments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDocuments_Category",
                table: "AccountDocuments",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDocuments_CreatedAt",
                table: "AccountDocuments",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDocuments_IsActive",
                table: "AccountDocuments",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDocuments_TenantId",
                table: "AccountDocuments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDocuments_TenantId_AccountId",
                table: "AccountDocuments",
                columns: new[] { "TenantId", "AccountId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDocuments");
        }
    }
}
