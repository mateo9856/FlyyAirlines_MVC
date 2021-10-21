using Microsoft.EntityFrameworkCore.Migrations;

namespace FlyyAirlines_MVC.Migrations
{
    public partial class IdentityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44f39d30-4dce-4c79-809b-9b39cc3368ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60b4fb79-a5e6-4f62-acbe-bd15e52911d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "679e0ccf-9ce3-43a9-8a9f-79e998e24c5f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "931f7e79-c2e6-46b9-b6ab-e7608f700bb3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: "6213f891-dae1-4bb4-801f-db3c0d0ddbe9");

            migrationBuilder.AlterColumn<long>(
                name: "PersonIdentify",
                table: "Reservations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11daa9a9-7b12-4ceb-8f2d-c5316922f2bd", "0e5c5bcc-f49e-4ee8-9769-ad28dfa598da", "Admin", "ADMIN" },
                    { "c23aff9c-5cc7-43ed-8a99-f59289da3d50", "d4e1cc75-e58f-4fb8-a8c7-b0e265a1d923", "SuperAdmin", "SUPERADMIN" },
                    { "1ea70af8-b08c-4ffa-9335-d20bfb744a75", "82783a2a-133e-4ca5-8e61-74db0c72cee0", "Employee", "EMPLOYEE" },
                    { "b66c34c0-e2fc-44c7-b5c9-834fa72582cd", "162aa3e0-3496-49ab-aa2f-106c852c6a70", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bf06c815-a898-4fb0-b0d1-2ea6595312b4", 0, "46373504-0dc3-495f-9a3f-3014070b22ab", null, "mateuszAdmin@flyy.com", false, false, null, "Mateusz", "MATEUSZADMIN@FLYY.COM", "SUPER@DMIN", null, "AQAAAAEAACcQAAAAEKMSsYJp/kTNx4oq1ESXR7epGGvwOrBZ9rwZddITHgwduSZn18c1gZv+eT9vzaiz3w==", null, false, "SuperAdmin", "", "Magdziak", false, "Super@Dmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11daa9a9-7b12-4ceb-8f2d-c5316922f2bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ea70af8-b08c-4ffa-9335-d20bfb744a75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b66c34c0-e2fc-44c7-b5c9-834fa72582cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c23aff9c-5cc7-43ed-8a99-f59289da3d50");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: "bf06c815-a898-4fb0-b0d1-2ea6595312b4");

            migrationBuilder.AlterColumn<int>(
                name: "PersonIdentify",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60b4fb79-a5e6-4f62-acbe-bd15e52911d5", "93124ee8-8994-471b-afe3-54d977324ed9", "Admin", "ADMIN" },
                    { "44f39d30-4dce-4c79-809b-9b39cc3368ba", "f8afd653-c2a3-46ee-8acc-b2e57825384e", "SuperAdmin", "SUPERADMIN" },
                    { "931f7e79-c2e6-46b9-b6ab-e7608f700bb3", "47ed1a9e-a47e-499b-9ddb-f5147684daa1", "Employee", "EMPLOYEE" },
                    { "679e0ccf-9ce3-43a9-8a9f-79e998e24c5f", "4d9a46ef-86d4-417d-9d4a-1c28802e097c", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6213f891-dae1-4bb4-801f-db3c0d0ddbe9", 0, "0702aa34-5b5a-4217-b7f7-187f23b77d22", null, "mateuszAdmin@flyy.com", false, false, null, "Mateusz", "MATEUSZADMIN@FLYY.COM", "SUPER@DMIN", null, "AQAAAAEAACcQAAAAEBI9CNTc+6TOH+dQdV/QVHPESpt6ZIOhwcawD9OQJc7GKlX7NX9zySqqh7X0L4fj7w==", null, false, "SuperAdmin", "", "Magdziak", false, "Super@Dmin" });
        }
    }
}
