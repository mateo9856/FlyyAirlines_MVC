using Microsoft.EntityFrameworkCore.Migrations;

namespace FlyyAirlines_MVC.Migrations
{
    public partial class AzureDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "257c15ce-3400-4077-bd29-3f8afe97b56d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a4857fd-e0b6-4260-972b-93e69254c9fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d645081-95e6-4897-86a6-34062f0ca51d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d45330b3-902b-4fc8-95fb-c4a71d0f3160");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "Permissions",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "655f3827-d7f3-47bc-8f12-239fd1f00010", "4e6a4d16-8d79-4848-9ebf-f3f680b883e1", "Admin", "ADMIN" },
                    { "694d4864-b7f1-454f-87f3-fec66add5138", "e5575351-031f-48c3-8838-8264a2b22b30", "SuperAdmin", "SUPERADMIN" },
                    { "b5c750a3-aaa1-4e39-9500-f169f993ab03", "a35fd5eb-22b1-4a4d-9bdb-cb77f42d382f", "Employee", "EMPLOYEE" },
                    { "33988f4d-bf8e-4889-b590-d7159287b66a", "3a46cef8-98d6-4067-a175-a4bef921ee81", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: "6e2575e0-aa31-4f6f-b203-a4921803186d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "00ecccba-4ed9-4004-acf7-1cbecbef3640", "AQAAAAEAACcQAAAAEJtfxkpbAJ8+HOwoeKSore1S8xfsjdFiVwJWCnm/6kmdY/o6/D2m34FsttrIkCqsPA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33988f4d-bf8e-4889-b590-d7159287b66a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "655f3827-d7f3-47bc-8f12-239fd1f00010");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "694d4864-b7f1-454f-87f3-fec66add5138");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5c750a3-aaa1-4e39-9500-f169f993ab03");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Permissions",
                newName: "PermissionId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d45330b3-902b-4fc8-95fb-c4a71d0f3160", "4896a6cd-f644-4c9d-b4ed-9fcf9d8f958b", "Admin", "ADMIN" },
                    { "4a4857fd-e0b6-4260-972b-93e69254c9fa", "67a13d16-ae3f-4dba-a129-45c80e380be5", "SuperAdmin", "SUPERADMIN" },
                    { "7d645081-95e6-4897-86a6-34062f0ca51d", "31b00a47-2e40-4118-8b76-67873e42e969", "Employee", "EMPLOYEE" },
                    { "257c15ce-3400-4077-bd29-3f8afe97b56d", "a9fdd0ed-157a-4dea-993b-b28530a32adc", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: "6e2575e0-aa31-4f6f-b203-a4921803186d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "30e4869a-d9ff-4085-9e1b-a2d0e2883414", "AQAAAAEAACcQAAAAEIqO55YwrgPMHI+1liZ9dpjXTBGKgzCr4HXk5pZdmUHOmPp6YRG8RthrWQbnHynpCw==" });
        }
    }
}
