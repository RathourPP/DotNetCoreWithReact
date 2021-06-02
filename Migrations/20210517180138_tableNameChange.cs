using Microsoft.EntityFrameworkCore.Migrations;

namespace DummyProjectApi.Migrations
{
    public partial class tableNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeBasicInformation",
                table: "EmployeeBasicInformation");

            migrationBuilder.RenameTable(
                name: "EmployeeBasicInformation",
                newName: "Registration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registration",
                table: "Registration",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registration",
                table: "Registration");

            migrationBuilder.RenameTable(
                name: "Registration",
                newName: "EmployeeBasicInformation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeBasicInformation",
                table: "EmployeeBasicInformation",
                column: "Id");
        }
    }
}
