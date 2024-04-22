using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class github : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GithubLink",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("55e11478-c3c0-4078-ae42-267706d92780"),
                column: "ConcurrencyStamp",
                value: "986531eb-29f9-457c-a6b5-5a07c0151a48");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5a2b3803-cd84-457d-a8be-27d2eb84af03"),
                column: "ConcurrencyStamp",
                value: "f3667576-e086-4b0f-b9ad-61d06e8b6bde");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fbf969a5-bddf-42f8-ab58-5fb420346f67"),
                column: "ConcurrencyStamp",
                value: "e976664f-a6b3-470d-b4a5-fc4c33b362d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98f41b72-899c-475d-af3e-6085304a0500"),
                columns: new[] { "ConcurrencyStamp", "ContactMail", "GithubLink", "InstagramLink", "LinkedInLink", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e3dc211-1c49-4121-8b26-243af17c2896", "superiletisim@gmail.com", null, "www.instagram.com.tr", "www.linkedin.com", "AQAAAAIAAYagAAAAEM+8/GbEiZ+mYyZCX1/3ywbwzYafhIIsphK11lnYDV+MJOGcSoB0coP/CQ9Se0FHeQ==", "93069f99-362f-47aa-a2d5-01dec3245c7d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c6c614f9-2a8c-42d0-8620-5eb4e1cf7cd4"),
                columns: new[] { "ConcurrencyStamp", "ContactMail", "GithubLink", "InstagramLink", "LinkedInLink", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ecd6355-e1c2-42e7-84f9-04b755efda2f", "adminiletisime@gmail.com", null, "www.instagram.com.tr", "www.linkedin.com", "AQAAAAIAAYagAAAAEIwEtbmysqR3iL3e2bl6Ksf+2qzeZxvFEW9HRZBwtIFYr3gfXvloxTDKhhTFMbASnQ==", "67814a05-502c-46d6-83fa-074792218e2a" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4c569a9a-5f41-478f-9d17-69ac5b02ae0b"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 16, 41, 24, 184, DateTimeKind.Local).AddTicks(2778));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d23e4f79-9600-4b5e-b3e9-756cdcacd2b1"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 16, 41, 24, 184, DateTimeKind.Local).AddTicks(2791));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("125da97b-3c5c-4c01-88dc-fb850e61c1f9"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 16, 41, 24, 184, DateTimeKind.Local).AddTicks(4246));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("9f6c6c6b-f940-4c39-99d6-873dc98af56a"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 20, 16, 41, 24, 184, DateTimeKind.Local).AddTicks(4235));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GithubLink",
                table: "AspNetUsers");

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
    }
}
