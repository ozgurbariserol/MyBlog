using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class media : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactMail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramLink",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInLink",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("55e11478-c3c0-4078-ae42-267706d92780"),
                column: "ConcurrencyStamp",
                value: "5cdbb48a-edc0-4cf1-98ba-35eaee9fd034");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5a2b3803-cd84-457d-a8be-27d2eb84af03"),
                column: "ConcurrencyStamp",
                value: "9f816081-5351-4b11-8085-0c2d9be73cfb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fbf969a5-bddf-42f8-ab58-5fb420346f67"),
                column: "ConcurrencyStamp",
                value: "7fc85081-2ab8-4981-8958-8d0a1f9b8ee9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98f41b72-899c-475d-af3e-6085304a0500"),
                columns: new[] { "ConcurrencyStamp", "ContactMail", "InstagramLink", "LinkedInLink", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1d1b9bf-ea45-4f20-b88e-e3780f5f3b92", null, null, null, "AQAAAAIAAYagAAAAEN0amIWtPMmSphwO5wOWJkSdKUPUo6DeQ4PSRmZXEPRskj9KEvtk3Wkl8Z3qUJ68iA==", "13532901-401f-41cd-a274-2ba8a8e65716" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c6c614f9-2a8c-42d0-8620-5eb4e1cf7cd4"),
                columns: new[] { "ConcurrencyStamp", "ContactMail", "InstagramLink", "LinkedInLink", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ec5ae1c-f7bb-4153-acda-806226595938", null, null, null, "AQAAAAIAAYagAAAAEGCjsK4PsUSDwCUHm2TId5J141mNt9BgNWxBcuG38i7XPAbjbkgZett2w0oqs2gYvQ==", "9b80975c-0f83-4896-b5a5-e7b31f01927f" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("4c569a9a-5f41-478f-9d17-69ac5b02ae0b"), "Admin Test", new DateTime(2024, 4, 20, 1, 27, 57, 422, DateTimeKind.Local).AddTicks(6445), null, null, false, null, null, "ASP.NET Core" },
                    { new Guid("d23e4f79-9600-4b5e-b3e9-756cdcacd2b1"), "Admin Test", new DateTime(2024, 4, 20, 1, 27, 57, 422, DateTimeKind.Local).AddTicks(6450), null, null, false, null, null, "Visual Studio 2022" }
                });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("125da97b-3c5c-4c01-88dc-fb850e61c1f9"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 27, 57, 422, DateTimeKind.Local).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("9f6c6c6b-f940-4c39-99d6-873dc98af56a"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 27, 57, 422, DateTimeKind.Local).AddTicks(7289));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4c569a9a-5f41-478f-9d17-69ac5b02ae0b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d23e4f79-9600-4b5e-b3e9-756cdcacd2b1"));

            migrationBuilder.DropColumn(
                name: "ContactMail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstagramLink",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedInLink",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("55e11478-c3c0-4078-ae42-267706d92780"),
                column: "ConcurrencyStamp",
                value: "bfdb3b84-f6fe-48d6-a90e-a367f023e744");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5a2b3803-cd84-457d-a8be-27d2eb84af03"),
                column: "ConcurrencyStamp",
                value: "46f99d98-810f-46a1-ad7c-b9e74c063eee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fbf969a5-bddf-42f8-ab58-5fb420346f67"),
                column: "ConcurrencyStamp",
                value: "fc9f0674-9e7d-4499-a771-b0cfe43a6a3c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98f41b72-899c-475d-af3e-6085304a0500"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "984070ec-0118-4eb3-8a25-628101ad7d17", "AQAAAAIAAYagAAAAEALH1wV/AG1onVGxfV9ZC91BUoGlaFQVAdTPzE55cTfZoXeopEs/ZAL+Q1Upy45S/Q==", "f278fd31-661f-439f-830c-c719d8dbaf96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c6c614f9-2a8c-42d0-8620-5eb4e1cf7cd4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47565cda-9d94-45f9-ad0f-fef62f750563", "AQAAAAIAAYagAAAAEFVSPIEyMAyfieB5KQlACIUTZpxMFKcOMyvrihD1CQCC1qbr9RniVCsbaKQphbzVcA==", "84376fb6-6fee-4517-aad2-b70730befd33" });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("125da97b-3c5c-4c01-88dc-fb850e61c1f9"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 1, 45, 469, DateTimeKind.Local).AddTicks(6609));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("9f6c6c6b-f940-4c39-99d6-873dc98af56a"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 1, 45, 469, DateTimeKind.Local).AddTicks(6606));
        }
    }
}
