using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V._3._0.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_Patients_PatId",
                table: "PersonalInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_Patients_PatId",
                table: "PersonalInfo",
                column: "PatId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_Patients_PatId",
                table: "PersonalInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_Patients_PatId",
                table: "PersonalInfo",
                column: "PatId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
