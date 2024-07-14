using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dd3acea-ed65-4ec9-812b-02183cf1d78b", null, "User", "USER" },
                    { "0fdc670e-7bed-4e65-9ba6-4fae8f194bfe", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0abcc174-052e-4776-b72d-03b9c5f8cdb4", 0, "3e4f4a55-1660-40e9-9391-078b8b403420", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEN6aQ8Tn8pIRNTGEw3GRIDMoYvls3bPyWTUAQYJj8AIzKJqGMKeaPhpu2XcNazYPfw==", null, false, "a7912abd-77fa-40ae-81d8-1e1fc9057868", false, "admin@bookstore.com" },
                    { "fe774faf-01ed-4fa3-9f7a-0a5b3685f2a4", 0, "931d35cb-5944-456d-832d-f0af492e9150", "user@bookstore.com", false, "Boobo", "Yoo", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEKs7rfFSS4BeFwKihQlVhqn2zs1Xd8m7SV+QLsVxlOs8707EP0RHoGLfUHnjHbCjTQ==", null, false, "6a0d303f-5f94-4804-b37e-b577a37da33c", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0fdc670e-7bed-4e65-9ba6-4fae8f194bfe", "0abcc174-052e-4776-b72d-03b9c5f8cdb4" },
                    { "0dd3acea-ed65-4ec9-812b-02183cf1d78b", "fe774faf-01ed-4fa3-9f7a-0a5b3685f2a4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0fdc670e-7bed-4e65-9ba6-4fae8f194bfe", "0abcc174-052e-4776-b72d-03b9c5f8cdb4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0dd3acea-ed65-4ec9-812b-02183cf1d78b", "fe774faf-01ed-4fa3-9f7a-0a5b3685f2a4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dd3acea-ed65-4ec9-812b-02183cf1d78b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fdc670e-7bed-4e65-9ba6-4fae8f194bfe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0abcc174-052e-4776-b72d-03b9c5f8cdb4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe774faf-01ed-4fa3-9f7a-0a5b3685f2a4");
        }
    }
}
