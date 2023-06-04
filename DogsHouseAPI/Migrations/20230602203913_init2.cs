using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogsHouseAPI.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dogs",
                table: "dogs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "dogs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "dogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dogs",
                table: "dogs",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_dogs",
                table: "dogs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "dogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "dogs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dogs",
                table: "dogs",
                column: "Id");
        }
    }
}
