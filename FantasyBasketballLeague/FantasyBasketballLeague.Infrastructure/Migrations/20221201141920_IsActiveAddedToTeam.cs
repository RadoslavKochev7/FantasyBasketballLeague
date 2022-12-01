using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyBasketballLeague.Infrastructure.Migrations
{
    public partial class IsActiveAddedToTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "05a1e706-e884-447c-8152-6f67231e2e10",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "fcd81efb-0089-49ae-bfd2-023b07499bc3", "AQAAAAEAACcQAAAAEBnAqmLelzW/7rDnFJ2ARaE14qiDsX5Lung/Z7KIm1R5yxV1hSS7Tvmw0ZVGNtWtyQ==", "61fa6a8f-0dce-42b7-81e1-256f7610ccf9" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "7e170021-8670-45dc-8352-67d285dbd759",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2e8111ba-2a36-4c1b-9bd5-d92d1c9ce83c", "AQAAAAEAACcQAAAAEPT98OUOKk8frbnOb1I80EzmxpOG8Crd5fPMiRnS7dFsSrfTvy2gIJkGQ9To6tRrXA==", "6b00e4ae-0e20-4f93-b605-f66561692bb1" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Teams");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05a1e706-e884-447c-8152-6f67231e2e10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e27433d9-9e78-4eab-bf7c-24300dd97f64", "AQAAAAEAACcQAAAAEC4QZOifynjAUAEKGSkJyUVr8r8OtsS/2SRYudbX0jJo6W6g0KrlOHrDaex5yDSQKw==", "297ee370-0d1b-4077-8a7e-efecc3ef6ddc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e170021-8670-45dc-8352-67d285dbd759",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa07d6f5-2487-4a5e-b5b8-b42e3baed93e", "AQAAAAEAACcQAAAAEOGhrfgUCunp8bWxGpxNR51hsPR5bv5yo3vSHXvhavNl8rl1XT+ukpicu130VS5xHA==", "e53c7e82-b2e5-4cdf-be61-08a5627090ef" });
        }
    }
}
