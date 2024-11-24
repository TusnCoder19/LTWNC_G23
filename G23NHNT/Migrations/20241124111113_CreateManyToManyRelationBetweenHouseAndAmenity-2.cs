using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G23NHNT.Migrations
{
    public partial class CreateManyToManyRelationBetweenHouseAndAmenity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Amenities_IdAmenity",
                table: "AmenityHouse");

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Houses_IdHouse",
                table: "AmenityHouse");

            migrationBuilder.AddColumn<int>(
                name: "AmenityIdAmenity",
                table: "AmenityHouse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HouseIdHouse",
                table: "AmenityHouse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AmenityHouse_AmenityIdAmenity",
                table: "AmenityHouse",
                column: "AmenityIdAmenity");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityHouse_HouseIdHouse",
                table: "AmenityHouse",
                column: "HouseIdHouse");

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Amenities_AmenityIdAmenity",
                table: "AmenityHouse",
                column: "AmenityIdAmenity",
                principalTable: "Amenities",
                principalColumn: "IdAmenity",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Amenities_IdAmenity",
                table: "AmenityHouse",
                column: "IdAmenity",
                principalTable: "Amenities",
                principalColumn: "IdAmenity",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Houses_HouseIdHouse",
                table: "AmenityHouse",
                column: "HouseIdHouse",
                principalTable: "Houses",
                principalColumn: "IdHouse",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Houses_IdHouse",
                table: "AmenityHouse",
                column: "IdHouse",
                principalTable: "Houses",
                principalColumn: "IdHouse",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Amenities_AmenityIdAmenity",
                table: "AmenityHouse");

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Amenities_IdAmenity",
                table: "AmenityHouse");

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Houses_HouseIdHouse",
                table: "AmenityHouse");

            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Houses_IdHouse",
                table: "AmenityHouse");

            migrationBuilder.DropIndex(
                name: "IX_AmenityHouse_AmenityIdAmenity",
                table: "AmenityHouse");

            migrationBuilder.DropIndex(
                name: "IX_AmenityHouse_HouseIdHouse",
                table: "AmenityHouse");

            migrationBuilder.DropColumn(
                name: "AmenityIdAmenity",
                table: "AmenityHouse");

            migrationBuilder.DropColumn(
                name: "HouseIdHouse",
                table: "AmenityHouse");

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
    }
}
