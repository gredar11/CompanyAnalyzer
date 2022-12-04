using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedBehaviourToHeadEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_HeadEmployeeId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_HeadEmployeeId",
                table: "Departments");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadEmployeeId",
                table: "Departments",
                column: "HeadEmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_HeadEmployeeId",
                table: "Departments",
                column: "HeadEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_HeadEmployeeId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_HeadEmployeeId",
                table: "Departments");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadEmployeeId",
                table: "Departments",
                column: "HeadEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_HeadEmployeeId",
                table: "Departments",
                column: "HeadEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
