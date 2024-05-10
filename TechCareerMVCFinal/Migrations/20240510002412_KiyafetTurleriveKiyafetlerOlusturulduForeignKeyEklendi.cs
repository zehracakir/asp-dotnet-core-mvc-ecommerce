using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechCareerMVCFinal.Migrations
{
    /// <inheritdoc />
    public partial class KiyafetTurleriveKiyafetlerOlusturulduForeignKeyEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KiyafetTurleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiyafetTurleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kiyafetler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KiyafetAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fiyati = table.Column<double>(type: "float", nullable: false),
                    Rengi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KiyafetTuruId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kiyafetler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kiyafetler_KiyafetTurleri_KiyafetTuruId",
                        column: x => x.KiyafetTuruId,
                        principalTable: "KiyafetTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kiyafetler_KiyafetTuruId",
                table: "Kiyafetler",
                column: "KiyafetTuruId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kiyafetler");

            migrationBuilder.DropTable(
                name: "KiyafetTurleri");
        }
    }
}
