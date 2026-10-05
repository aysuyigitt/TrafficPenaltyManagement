using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafficPenaltyManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PenaltyTypeId",
                table: "Penalties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_PenaltyTypeId",
                table: "Penalties",
                column: "PenaltyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Penalties_PenaltyTypes_PenaltyTypeId",
                table: "Penalties",
                column: "PenaltyTypeId",
                principalTable: "PenaltyTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Penalties_PenaltyTypes_PenaltyTypeId",
                table: "Penalties");

            migrationBuilder.DropIndex(
                name: "IX_Penalties_PenaltyTypeId",
                table: "Penalties");

            migrationBuilder.DropColumn(
                name: "PenaltyTypeId",
                table: "Penalties");
        }
    }
}
