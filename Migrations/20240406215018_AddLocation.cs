using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherChecker.Migrations
{
    /// <inheritdoc />
    public partial class AddLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "MeasuredWeatherData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Forecasts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<float>(type: "float", nullable: false),
                    Longitude = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuredWeatherData_LocationId",
                table: "MeasuredWeatherData",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_LocationId",
                table: "Forecasts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Id",
                table: "Location",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name",
                table: "Location",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Forecasts_Location_LocationId",
                table: "Forecasts",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuredWeatherData_Location_LocationId",
                table: "MeasuredWeatherData",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forecasts_Location_LocationId",
                table: "Forecasts");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasuredWeatherData_Location_LocationId",
                table: "MeasuredWeatherData");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_MeasuredWeatherData_LocationId",
                table: "MeasuredWeatherData");

            migrationBuilder.DropIndex(
                name: "IX_Forecasts_LocationId",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MeasuredWeatherData");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Forecasts");
        }
    }
}
