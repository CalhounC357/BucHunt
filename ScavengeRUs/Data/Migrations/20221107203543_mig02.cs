using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScavengeRUs.Data.Migrations
{
    public partial class mig02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessCodeId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccessCodeId",
                table: "AspNetUsers",
                column: "AccessCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AccessCode_AccessCodeId",
                table: "AspNetUsers",
                column: "AccessCodeId",
                principalTable: "AccessCode",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AccessCode_AccessCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AccessCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessCodeId",
                table: "AspNetUsers");
        }
    }
}
