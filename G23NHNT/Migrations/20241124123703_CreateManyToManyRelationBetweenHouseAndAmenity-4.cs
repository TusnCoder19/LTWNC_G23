using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G23NHNT.Migrations
{
    public partial class CreateManyToManyRelationBetweenHouseAndAmenity4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityHouse");

            migrationBuilder.AddColumn<int>(
                name: "AmenityIdAmenity",
                table: "Houses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmenityIds",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_AmenityIdAmenity",
                table: "Houses",
                column: "AmenityIdAmenity");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Amenities_AmenityIdAmenity",
                table: "Houses",
                column: "AmenityIdAmenity",
                principalTable: "Amenities",
                principalColumn: "IdAmenity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Amenities_AmenityIdAmenity",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_AmenityIdAmenity",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "AmenityIdAmenity",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "AmenityIds",
                table: "Houses");

            migrationBuilder.CreateTable(
                name: "AmenityHouse",
                columns: table => new
                {
                    IdAmenity = table.Column<int>(type: "int", nullable: false),
                    IdHouse = table.Column<int>(type: "int", nullable: false),
                    HouseIdHouse = table.Column<int>(type: "int", nullable: false),
                    AmenityIdAmenity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityHouse", x => new { x.IdAmenity, x.IdHouse });
                    table.ForeignKey(
                        name: "FK_AmenityHouse_Amenities_AmenityIdAmenity",
                        column: x => x.AmenityIdAmenity,
                        principalTable: "Amenities",
                        principalColumn: "IdAmenity");
                    table.ForeignKey(
                        name: "FK_AmenityHouse_Amenities_IdAmenity",
                        column: x => x.IdAmenity,
                        principalTable: "Amenities",
                        principalColumn: "IdAmenity",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AmenityHouse_Houses_HouseIdHouse",
                        column: x => x.HouseIdHouse,
                        principalTable: "Houses",
                        principalColumn: "IdHouse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityHouse_Houses_IdHouse",
                        column: x => x.IdHouse,
                        principalTable: "Houses",
                        principalColumn: "IdHouse",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityHouse_AmenityIdAmenity",
                table: "AmenityHouse",
                column: "AmenityIdAmenity");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityHouse_HouseIdHouse",
                table: "AmenityHouse",
                column: "HouseIdHouse");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityHouse_IdHouse",
                table: "AmenityHouse",
                column: "IdHouse");
        }
    }
}
