using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Games",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                ImageUrl = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                Genre = table.Column<int>(type: "INTEGER", nullable: false),
                IsDemo = table.Column<bool>(type: "INTEGER", nullable: false),
                Publisher = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Games", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "LibraryItemModel",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LibraryItemModel", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Logins",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Logins", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Library",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Wishlist = table.Column<bool>(type: "INTEGER", nullable: false),
                LibraryItemModelId = table.Column<int>(type: "INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Library", x => x.Id);
                table.ForeignKey(
                    name: "FK_Library_LibraryItemModel_LibraryItemModelId",
                    column: x => x.LibraryItemModelId,
                    principalTable: "LibraryItemModel",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                Role = table.Column<string>(type: "TEXT", nullable: false),
                IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                LoginId = table.Column<Guid>(type: "TEXT", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Logins_LoginId",
                    column: x => x.LoginId,
                    principalTable: "Logins",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Library_LibraryItemModelId",
            table: "Library",
            column: "LibraryItemModelId");

        migrationBuilder.CreateIndex(
            name: "IX_Logins_Email",
            table: "Logins",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_LoginId",
            table: "Users",
            column: "LoginId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Games");

        migrationBuilder.DropTable(
            name: "Library");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "LibraryItemModel");

        migrationBuilder.DropTable(
            name: "Logins");
    }
}
