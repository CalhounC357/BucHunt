using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScavengeRUs.Data.Migrations
{
    public partial class mig01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hunt_HuntId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hunt",
                table: "Hunt");

            migrationBuilder.RenameTable(
                name: "Hunt",
                newName: "Hunts");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hunts",
                table: "Hunts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccessCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HuntId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessCode_Hunts_HuntId",
                        column: x => x.HuntId,
                        principalTable: "Hunts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HuntId = table.Column<int>(type: "INTEGER", nullable: false),
                    Place = table.Column<string>(type: "TEXT", nullable: false),
                    Lat = table.Column<double>(type: "REAL", nullable: true),
                    Lon = table.Column<double>(type: "REAL", nullable: true),
                    Task = table.Column<string>(type: "TEXT", nullable: false),
                    AccessCode = table.Column<string>(type: "TEXT", nullable: true),
                    QRCode = table.Column<string>(type: "TEXT", nullable: true),
                    Answer = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessCode_HuntId",
                table: "AccessCode",
                column: "HuntId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hunts_HuntId",
                table: "AspNetUsers",
                column: "HuntId",
                principalTable: "Hunts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hunts_HuntId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AccessCode");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hunts",
                table: "Hunts");

            migrationBuilder.RenameTable(
                name: "Hunts",
                newName: "Hunt");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hunt",
                table: "Hunt",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hunt_HuntId",
                table: "AspNetUsers",
                column: "HuntId",
                principalTable: "Hunt",
                principalColumn: "Id");
        }
    }
}
