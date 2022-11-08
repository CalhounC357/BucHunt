using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScavengeRUs.Data.Migrations
{
    public partial class mig05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AccessCodes_AccessCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hunts_HuntId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AccessCodes_AccessCodeId",
                table: "AspNetUsers",
                column: "AccessCodeId",
                principalTable: "AccessCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_AspNetUsers_AccessCodes_AccessCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hunts_HuntId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AccessCodes_AccessCodeId",
                table: "AspNetUsers",
                column: "AccessCodeId",
                principalTable: "AccessCodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hunts_HuntId",
                table: "AspNetUsers",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id");
        }
    }
}
