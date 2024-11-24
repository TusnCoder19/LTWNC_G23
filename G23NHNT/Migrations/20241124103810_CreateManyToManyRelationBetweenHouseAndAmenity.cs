using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G23NHNT.Migrations
{
    public partial class CreateManyToManyRelationBetweenHouseAndAmenity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Amenities_IdAmenitiesIdAmenity",
                table: "AmenityHouse");

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Houses_IdHousesIdHouse",
                table: "AmenityHouse");

            migrationBuilder.RenameColumn(
                name: "IdHousesIdHouse",
                table: "AmenityHouse",
                newName: "IdHouse");

            migrationBuilder.RenameColumn(
                name: "IdAmenitiesIdAmenity",
                table: "AmenityHouse",
                newName: "IdAmenity");

            migrationBuilder.RenameIndex(
                name: "IX_AmenityHouse_IdHousesIdHouse",
                table: "AmenityHouse",
                newName: "IX_AmenityHouse_IdHouse");

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Amenities_IdAmenity",
                table: "AmenityHouse",
                column: "IdAmenity",
                principalTable: "Amenities",
                principalColumn: "IdAmenity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Houses_IdHouse",
                table: "AmenityHouse",
                column: "IdHouse",
                principalTable: "Houses",
                principalColumn: "IdHouse",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Amenities_IdAmenity",
                table: "AmenityHouse");

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Houses_IdHouse",
                table: "AmenityHouse");

            migrationBuilder.RenameColumn(
                name: "IdHouse",
                table: "AmenityHouse",
                newName: "IdHousesIdHouse");

            migrationBuilder.RenameColumn(
                name: "IdAmenity",
                table: "AmenityHouse",
                newName: "IdAmenitiesIdAmenity");

            migrationBuilder.RenameIndex(
                name: "IX_AmenityHouse_IdHouse",
                table: "AmenityHouse",
                newName: "IX_AmenityHouse_IdHousesIdHouse");

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Amenities_IdAmenitiesIdAmenity",
                table: "AmenityHouse",
                column: "IdAmenitiesIdAmenity",
                principalTable: "Amenities",
                principalColumn: "IdAmenity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Houses_IdHousesIdHouse",
                table: "AmenityHouse",
                column: "IdHousesIdHouse",
                principalTable: "Houses",
                principalColumn: "IdHouse",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
