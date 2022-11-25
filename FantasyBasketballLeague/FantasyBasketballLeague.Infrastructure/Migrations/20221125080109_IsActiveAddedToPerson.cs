using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyBasketballLeague.Infrastructure.Migrations
{
    public partial class IsActiveAddedToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "LeagueId",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Coaches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BasketballPlayers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "05a1e706-e884-447c-8152-6f67231e2e10",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e27433d9-9e78-4eab-bf7c-24300dd97f64", "AQAAAAEAACcQAAAAEC4QZOifynjAUAEKGSkJyUVr8r8OtsS/2SRYudbX0jJo6W6g0KrlOHrDaex5yDSQKw==", "297ee370-0d1b-4077-8a7e-efecc3ef6ddc" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "7e170021-8670-45dc-8352-67d285dbd759",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "aa07d6f5-2487-4a5e-b5b8-b42e3baed93e", "AQAAAAEAACcQAAAAEOGhrfgUCunp8bWxGpxNR51hsPR5bv5yo3vSHXvhavNl8rl1XT+ukpicu130VS5xHA==", "e53c7e82-b2e5-4cdf-be61-08a5627090ef" });

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "BasketballPlayers",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsActive",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BasketballPlayers");

            migrationBuilder.AlterColumn<int>(
                name: "LeagueId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05a1e706-e884-447c-8152-6f67231e2e10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8263ee88-6f31-468a-b289-2310cd49ce3a", "AQAAAAEAACcQAAAAECaSC0RDdgUXWU+ZPT/ovVu56b0N91vreHKlId5v0kDEeybrprTiI5CWe6Cw9upLMg==", "27dc7547-cb1f-4a5e-972c-c434656b7da2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e170021-8670-45dc-8352-67d285dbd759",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bc9e575-6d89-40e5-bd18-b75dca7d916e", "AQAAAAEAACcQAAAAECTaI+/ADs1sTrXamOFU7dJ6vn0lifijxDaqxKe/pB8SOdkfjcs88l8nGwNsqsI9uQ==", "2576d75d-61b1-4b2c-8790-51014e6f76d3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
