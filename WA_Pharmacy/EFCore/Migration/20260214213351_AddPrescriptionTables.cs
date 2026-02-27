using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WA_Pharmacy.EFCore.Migration
{
    /// <inheritdoc />
    public partial class AddPrescriptionTables : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineLists_Medicines_MedicineId",
                table: "MedicineLists");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineLists_Prescriptions_PrescriptionId",
                table: "MedicineLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicineLists",
                table: "MedicineLists");

            migrationBuilder.RenameTable(
                name: "MedicineLists",
                newName: "PrescriptionDetails");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineLists_PrescriptionId",
                table: "PrescriptionDetails",
                newName: "IX_PrescriptionDetails_PrescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineLists_MedicineId",
                table: "PrescriptionDetails",
                newName: "IX_PrescriptionDetails_MedicineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionDetails",
                table: "PrescriptionDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionDetails_Medicines_MedicineId",
                table: "PrescriptionDetails",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionDetails_Prescriptions_PrescriptionId",
                table: "PrescriptionDetails",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionDetails_Medicines_MedicineId",
                table: "PrescriptionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionDetails_Prescriptions_PrescriptionId",
                table: "PrescriptionDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionDetails",
                table: "PrescriptionDetails");

            migrationBuilder.RenameTable(
                name: "PrescriptionDetails",
                newName: "MedicineLists");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionDetails_PrescriptionId",
                table: "MedicineLists",
                newName: "IX_MedicineLists_PrescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionDetails_MedicineId",
                table: "MedicineLists",
                newName: "IX_MedicineLists_MedicineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicineLists",
                table: "MedicineLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineLists_Medicines_MedicineId",
                table: "MedicineLists",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineLists_Prescriptions_PrescriptionId",
                table: "MedicineLists",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
