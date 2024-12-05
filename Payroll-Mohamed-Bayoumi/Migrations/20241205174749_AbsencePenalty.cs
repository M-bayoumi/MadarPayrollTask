using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll_Mohamed_Bayoumi.Migrations
{
    /// <inheritdoc />
    public partial class AbsencePenalty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbsencePenalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbsenceDays = table.Column<int>(type: "int", nullable: false),
                    PenaltyPercentage = table.Column<double>(type: "float", nullable: false),
                    IsBonus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsencePenalties", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsencePenalties");
        }
    }
}
