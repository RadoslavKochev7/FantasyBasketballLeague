using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyBasketballLeague.Infrastructure.Migrations
{
    public partial class NullableTeamLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05a1e706-e884-447c-8152-6f67231e2e10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c469a294-dede-4369-b3a3-11e924c377bd", "AQAAAAEAACcQAAAAEOJDW/4wlC8dCen+lmYZPDcjlVASjLTBV+/Kkjm+vWLBTBjJMNL6SJrXUfmuExq12A==", "47d15095-0263-4743-9207-73fa59b5838b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e170021-8670-45dc-8352-67d285dbd759",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07b5d297-44c9-4c82-9ca9-6056e31e819f", "AQAAAAEAACcQAAAAEC7z6aSRc3+lTI2YKACRJJsrag8NqIpXVuCm+EjwTBQ+VZEglbJJc6svuI8WbMnsSQ==", "2cd03d70-3aa9-47a9-9914-a8907b7d41ca" });
        }
    }
}
