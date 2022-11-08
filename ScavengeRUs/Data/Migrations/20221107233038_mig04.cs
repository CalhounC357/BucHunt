using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScavengeRUs.Data.Migrations
{
    public partial class mig04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCode_Hunts_HuntId",
                table: "AccessCode");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AccessCode_AccessCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessCode",
                table: "AccessCode");

            migrationBuilder.RenameTable(
                name: "AccessCode",
                newName: "AccessCodes");

            migrationBuilder.RenameIndex(
                name: "IX_AccessCode_HuntId",
                table: "AccessCodes",
                newName: "IX_AccessCodes_HuntId");

            migrationBuilder.AlterColumn<int>(
                name: "HuntId",
                table: "AccessCodes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessCodes",
                table: "AccessCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AccessCodes_AccessCodeId",
                table: "AspNetUsers",
                column: "AccessCodeId",
                principalTable: "AccessCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AccessCodes_AccessCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessCodes",
                table: "AccessCodes");

            migrationBuilder.RenameTable(
                name: "AccessCodes",
                newName: "AccessCode");

            migrationBuilder.RenameIndex(
                name: "IX_AccessCodes_HuntId",
                table: "AccessCode",
                newName: "IX_AccessCode_HuntId");

            migrationBuilder.AlterColumn<int>(
                name: "HuntId",
                table: "AccessCode",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessCode",
                table: "AccessCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCode_Hunts_HuntId",
                table: "AccessCode",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AccessCode_AccessCodeId",
                table: "AspNetUsers",
                column: "AccessCodeId",
                principalTable: "AccessCode",
                principalColumn: "Id");
        }
    }
}
