using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class media2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("55e11478-c3c0-4078-ae42-267706d92780"),
                column: "ConcurrencyStamp",
                value: "abe6e41f-8a8e-42f0-b873-96b954c437e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5a2b3803-cd84-457d-a8be-27d2eb84af03"),
                column: "ConcurrencyStamp",
                value: "036c3e40-c656-43b9-9553-b1f1a1428066");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fbf969a5-bddf-42f8-ab58-5fb420346f67"),
                column: "ConcurrencyStamp",
                value: "40222b06-b71f-42cd-92b3-c1726044bf9a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98f41b72-899c-475d-af3e-6085304a0500"),
                columns: new[] { "ConcurrencyStamp", "ContactMail", "InstagramLink", "LinkedInLink", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8a1194c-bde9-4755-8014-5dda0287ee12", "", "", "", "AQAAAAIAAYagAAAAEF2bQ9OiyVhXuavIGL8AHQea8m7bTFNEu4n6ApWbfptTh6sI5Ik2QskUeguMEV/qdQ==", "8402c702-ce00-4b72-9e60-9487ec4ce97c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c6c614f9-2a8c-42d0-8620-5eb4e1cf7cd4"),
                columns: new[] { "ConcurrencyStamp", "ContactMail", "InstagramLink", "LinkedInLink", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01bb407e-0eb1-4b1a-8a30-f4fa0f6728f6", "", "", "", "AQAAAAIAAYagAAAAEMwaxtZAC0Iv1KqAtDFhh9PiH8NNsqenUWWr65WDJ6TGc50nF8PtUicprCRmjTLk+Q==", "26f847b7-c646-4a28-a567-e60716cc1442" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4c569a9a-5f41-478f-9d17-69ac5b02ae0b"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 41, 14, 719, DateTimeKind.Local).AddTicks(186));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d23e4f79-9600-4b5e-b3e9-756cdcacd2b1"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 41, 14, 719, DateTimeKind.Local).AddTicks(189));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("125da97b-3c5c-4c01-88dc-fb850e61c1f9"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 41, 14, 719, DateTimeKind.Local).AddTicks(843));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("9f6c6c6b-f940-4c39-99d6-873dc98af56a"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 41, 14, 719, DateTimeKind.Local).AddTicks(840));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4c569a9a-5f41-478f-9d17-69ac5b02ae0b"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 27, 57, 422, DateTimeKind.Local).AddTicks(6445));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d23e4f79-9600-4b5e-b3e9-756cdcacd2b1"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 1, 27, 57, 422, DateTimeKind.Local).AddTicks(6450));

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
    }
}
