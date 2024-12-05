using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll_Mohamed_Bayoumi.Migrations
{
    /// <inheritdoc />
    public partial class AddIncentive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentIncentives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IncentivePercentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentIncentives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentIncentives_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeniorityIncentives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearsOfService = table.Column<int>(type: "int", nullable: false),
                    IncentivePercentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorityIncentives", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentIncentives_DepartmentId",
                table: "DepartmentIncentives",
                column: "DepartmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentIncentives");

            migrationBuilder.DropTable(
                name: "SeniorityIncentives");
        }
    }
}
