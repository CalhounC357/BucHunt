using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScavengeRUs.Data.Migrations
{
    public partial class mig06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Hunts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvitationText",
                table: "Hunts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Hunts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Hunts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Hunts");

            migrationBuilder.DropColumn(
                name: "InvitationText",
                table: "Hunts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Hunts");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Hunts");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCodes_Hunts_HuntId",
                table: "AccessCodes",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
