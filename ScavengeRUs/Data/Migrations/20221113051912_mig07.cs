using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScavengeRUs.Data.Migrations
{
    public partial class mig07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes");

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

            migrationBuilder.AlterColumn<int>(
                name: "huntId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hunts_huntId",
                table: "AspNetUsers",
                column: "huntId",
                principalTable: "Hunts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes");

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

            migrationBuilder.AlterColumn<int>(
                name: "HuntId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hunts_HuntId",
                table: "AspNetUsers",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
