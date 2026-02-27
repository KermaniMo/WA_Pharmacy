using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WA_Pharmacy.EFCore.Migration
{
    /// <inheritdoc />
    public partial class AddInsuredToPrescription : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InsuredId",
                table: "Prescriptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_InsuredId",
                table: "Prescriptions",
                column: "InsuredId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Insureds_InsuredId",
                table: "Prescriptions",
                column: "InsuredId",
                principalTable: "Insureds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Insureds_InsuredId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_InsuredId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "InsuredId",
                table: "Prescriptions");
        }
    }
}
