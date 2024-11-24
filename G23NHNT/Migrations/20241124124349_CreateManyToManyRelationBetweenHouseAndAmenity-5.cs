using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G23NHNT.Migrations
{
    public partial class CreateManyToManyRelationBetweenHouseAndAmenity5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmenityIdAmenity",
                table: "Houses",
                type: "int",
                nullable: true);

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
    }
}
