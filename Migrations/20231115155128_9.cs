using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace V._3._0.Migrations
{
    /// <inheritdoc />
    public partial class _9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientPayments",
                columns: table => new
                {
                    PayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    PatientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPayments", x => x.PayId);
                    table.ForeignKey(
                        name: "FK_PatientPayments_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    Sno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    procedure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    PatientPaymentPayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.Sno);
                    table.ForeignKey(
                        name: "FK_PaymentDetails_PatientPayments_PatientPaymentPayId",
                        column: x => x.PatientPaymentPayId,
                        principalTable: "PatientPayments",
                        principalColumn: "PayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientPayments_PatientsId",
                table: "PatientPayments",
                column: "PatientsId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_PatientPaymentPayId",
                table: "PaymentDetails",
                column: "PatientPaymentPayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropTable(
                name: "PatientPayments");
        }
    }
}
