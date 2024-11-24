using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G23NHNT.Migrations
{
    public partial class CreateManyToManyRelationBetweenHouseAndAmenity3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Amenities_AmenityIdAmenity",
                table: "AmenityHouse");

            migrationBuilder.AlterColumn<int>(
                name: "AmenityIdAmenity",
                table: "AmenityHouse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Amenities_AmenityIdAmenity",
                table: "AmenityHouse",
                column: "AmenityIdAmenity",
                principalTable: "Amenities",
                principalColumn: "IdAmenity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmenityHouse_Amenities_AmenityIdAmenity",
                table: "AmenityHouse");

            migrationBuilder.AlterColumn<int>(
                name: "AmenityIdAmenity",
                table: "AmenityHouse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AmenityHouse_Amenities_AmenityIdAmenity",
                table: "AmenityHouse",
                column: "AmenityIdAmenity",
                principalTable: "Amenities",
                principalColumn: "IdAmenity",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
