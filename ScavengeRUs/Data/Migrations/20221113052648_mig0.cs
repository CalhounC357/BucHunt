using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScavengeRUs.Data.Migrations
{
    public partial class mig0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hunts_huntId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "huntId",
                table: "AspNetUsers",
                newName: "HuntId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_huntId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_HuntId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hunts_HuntId",
                table: "AspNetUsers",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hunts_HuntId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "HuntId",
                table: "AspNetUsers",
                newName: "huntId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_HuntId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_huntId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hunts_huntId",
                table: "AspNetUsers",
                column: "huntId",
                principalTable: "Hunts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
