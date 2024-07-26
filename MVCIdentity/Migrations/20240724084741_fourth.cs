using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCIdentity.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OgrenciDersler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersId = table.Column<int>(type: "int", nullable: false),
                    MVCIdentityUserId = table.Column<int>(type: "int", nullable: false),
                    MVCIdentityUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciDersler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrenciDersler_AspNetUsers_MVCIdentityUserId1",
                        column: x => x.MVCIdentityUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OgrenciDersler_Ders_DersId",
                        column: x => x.DersId,
                        principalTable: "Ders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDersler_DersId",
                table: "OgrenciDersler",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDersler_MVCIdentityUserId1",
                table: "OgrenciDersler",
                column: "MVCIdentityUserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgrenciDersler");
        }
    }
}
