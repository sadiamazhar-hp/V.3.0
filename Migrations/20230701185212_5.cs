using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V._3._0.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalInfo_Patients_PatId",
                table: "MedicalInfo");

            migrationBuilder.RenameColumn(
                name: "PatId",
                table: "MedicalInfo",
                newName: "PatientsId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalInfo_PatId",
                table: "MedicalInfo",
                newName: "IX_MedicalInfo_PatientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalInfo_Patients_PatientsId",
                table: "MedicalInfo",
                column: "PatientsId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalInfo_Patients_PatientsId",
                table: "MedicalInfo");

            migrationBuilder.RenameColumn(
                name: "PatientsId",
                table: "MedicalInfo",
                newName: "PatId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalInfo_PatientsId",
                table: "MedicalInfo",
                newName: "IX_MedicalInfo_PatId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalInfo_Patients_PatId",
                table: "MedicalInfo",
                column: "PatId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
