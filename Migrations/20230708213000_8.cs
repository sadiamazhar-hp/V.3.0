using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V._3._0.Migrations
{
    /// <inheritdoc />
    public partial class _8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "MedicalInfo",
                newName: "ImageFile");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "MedicalInfo",
                newName: "ImageFileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                table: "MedicalInfo",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "ImageFile",
                table: "MedicalInfo",
                newName: "Image");
        }
    }
}
