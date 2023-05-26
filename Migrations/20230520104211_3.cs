using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V._3._0.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_Patients_NameId",
                table: "PersonalInfo");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInfo_NameId",
                table: "PersonalInfo");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "PersonalInfo");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PersonalInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PersonalInfo");

            migrationBuilder.AddColumn<int>(
                name: "NameId",
                table: "PersonalInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfo_NameId",
                table: "PersonalInfo",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_Patients_NameId",
                table: "PersonalInfo",
                column: "NameId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
