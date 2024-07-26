using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCIdentity.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OgrenciDersler_AspNetUsers_MVCIdentityUserId1",
                table: "OgrenciDersler");

            migrationBuilder.DropIndex(
                name: "IX_OgrenciDersler_MVCIdentityUserId1",
                table: "OgrenciDersler");

            migrationBuilder.DropColumn(
                name: "MVCIdentityUserId1",
                table: "OgrenciDersler");

            migrationBuilder.AlterColumn<string>(
                name: "MVCIdentityUserId",
                table: "OgrenciDersler",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDersler_MVCIdentityUserId",
                table: "OgrenciDersler",
                column: "MVCIdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OgrenciDersler_AspNetUsers_MVCIdentityUserId",
                table: "OgrenciDersler",
                column: "MVCIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OgrenciDersler_AspNetUsers_MVCIdentityUserId",
                table: "OgrenciDersler");

            migrationBuilder.DropIndex(
                name: "IX_OgrenciDersler_MVCIdentityUserId",
                table: "OgrenciDersler");

            migrationBuilder.AlterColumn<int>(
                name: "MVCIdentityUserId",
                table: "OgrenciDersler",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "MVCIdentityUserId1",
                table: "OgrenciDersler",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDersler_MVCIdentityUserId1",
                table: "OgrenciDersler",
                column: "MVCIdentityUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OgrenciDersler_AspNetUsers_MVCIdentityUserId1",
                table: "OgrenciDersler",
                column: "MVCIdentityUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
