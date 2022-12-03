using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyBasketballLeague.Infrastructure.Migrations
{
    public partial class AdministratorRoleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b5df5b4b-aa27-47b1-85d3-fc24b33f43d6", "fc4be5f7-1ad1-434c-8982-5a368d6b6108", "Administrator", null });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "05a1e706-e884-447c-8152-6f67231e2e10",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "239d00be-13ff-47d9-8731-9d2438392089", "AQAAAAEAACcQAAAAEEsBnr7keO9pCzKNDGgYxtmGfH7kIqeWZvydJGozqgqgbaB7FYHL4vjUj8UA7VMZSQ==", "ad865918-f06b-4262-a40b-094f02de362f" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "7e170021-8670-45dc-8352-67d285dbd759",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "07465453-2b7f-480b-83f4-351f48f13a1b", "AQAAAAEAACcQAAAAECeT19AEwKYg3U47I1KQj3N3cK9zfD8Qd401SFNAq4KR6TqDV98JoGEyAmoEzyttvw==", "a5454d9a-1b12-4346-a66b-2b5c6122b30f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5df5b4b-aa27-47b1-85d3-fc24b33f43d6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05a1e706-e884-447c-8152-6f67231e2e10",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcd81efb-0089-49ae-bfd2-023b07499bc3", "AQAAAAEAACcQAAAAEBnAqmLelzW/7rDnFJ2ARaE14qiDsX5Lung/Z7KIm1R5yxV1hSS7Tvmw0ZVGNtWtyQ==", "61fa6a8f-0dce-42b7-81e1-256f7610ccf9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e170021-8670-45dc-8352-67d285dbd759",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e8111ba-2a36-4c1b-9bd5-d92d1c9ce83c", "AQAAAAEAACcQAAAAEPT98OUOKk8frbnOb1I80EzmxpOG8Crd5fPMiRnS7dFsSrfTvy2gIJkGQ9To6tRrXA==", "6b00e4ae-0e20-4f93-b605-f66561692bb1" });
        }
    }
}
