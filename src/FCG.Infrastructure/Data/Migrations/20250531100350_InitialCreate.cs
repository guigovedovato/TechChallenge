using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
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

        migrationBuilder.InsertData(
            table: "Logins",
            columns: new[] { "Id", "CreatedAt", "Email", "Password", "UpdatedAt" },
            values: new object[] { new Guid("e402588b-c931-437d-83ac-941010840391"), new DateTime(2025, 5, 31, 10, 3, 50, 110, DateTimeKind.Utc).AddTicks(9688), "admin@email.com", "AQAAAAIAAYagAAAAEFvxevg2Oz/DiArPRXiGGbDy29yxw2ZrroJzNh68WHeARuCaupa6cqkHRj2x0B2S8A==", new DateTime(2025, 5, 31, 10, 3, 50, 110, DateTimeKind.Utc).AddTicks(9691) });

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "CreatedAt", "FirstName", "IsActive", "LastName", "LoginId", "Role", "UpdatedAt" },
            values: new object[] { new Guid("bb96f61c-72c9-410c-8b3e-8b1b636a718f"), new DateTime(2025, 5, 31, 10, 3, 50, 111, DateTimeKind.Utc).AddTicks(4371), "Admin", true, "User", new Guid("e402588b-c931-437d-83ac-941010840391"), "Admin", new DateTime(2025, 5, 31, 10, 3, 50, 111, DateTimeKind.Utc).AddTicks(4373) });

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
            name: "Users");

        migrationBuilder.DropTable(
            name: "Logins");
    }
}
