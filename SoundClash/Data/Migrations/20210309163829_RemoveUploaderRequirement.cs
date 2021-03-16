using Microsoft.EntityFrameworkCore.Migrations;

namespace SoundClash.Data.Migrations
{
    public partial class RemoveUploaderRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sound_AspNetUsers_UploaderId",
                table: "Sound");

            migrationBuilder.AlterColumn<string>(
                name: "UploaderId",
                table: "Sound",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Sound_AspNetUsers_UploaderId",
                table: "Sound",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sound_AspNetUsers_UploaderId",
                table: "Sound");

            migrationBuilder.AlterColumn<string>(
                name: "UploaderId",
                table: "Sound",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sound_AspNetUsers_UploaderId",
                table: "Sound",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
