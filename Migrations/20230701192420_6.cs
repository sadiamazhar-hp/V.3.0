using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V._3._0.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MedicalInfo_PatientsId",
                table: "MedicalInfo");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInfo_PatientsId",
                table: "MedicalInfo",
                column: "PatientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MedicalInfo_PatientsId",
                table: "MedicalInfo");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInfo_PatientsId",
                table: "MedicalInfo",
                column: "PatientsId",
                unique: true);
        }
    }
}
