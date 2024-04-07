using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherChecker.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Locations",
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
                    table.PrimaryKey("PK_Locations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecasts_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MeasuredWeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Temperature = table.Column<float>(type: "float", nullable: false),
                    Humidity = table.Column<float>(type: "float", nullable: false),
                    Windspeed = table.Column<float>(type: "float", nullable: false),
                    WindDirection = table.Column<float>(type: "float", nullable: false),
                    WeatherCode = table.Column<int>(type: "int", nullable: false),
                    CloudCoverTotal = table.Column<float>(type: "float", nullable: false),
                    SurfacePressure = table.Column<float>(type: "float", nullable: false),
                    Visibility = table.Column<float>(type: "float", nullable: false),
                    PrecipitationProbability = table.Column<float>(type: "float", nullable: false),
                    Rain = table.Column<float>(type: "float", nullable: false),
                    Showers = table.Column<float>(type: "float", nullable: false),
                    Snowfall = table.Column<float>(type: "float", nullable: false),
                    SoilTemperature_6cm = table.Column<float>(type: "float", nullable: false),
                    SoilMoisture_1_3cm = table.Column<float>(type: "float", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuredWeatherData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasuredWeatherData_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ForecastWeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PredictionTimestamp = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Temperature = table.Column<float>(type: "float", nullable: false),
                    Humidity = table.Column<float>(type: "float", nullable: false),
                    Windspeed = table.Column<float>(type: "float", nullable: false),
                    WindDirection = table.Column<float>(type: "float", nullable: false),
                    WeatherCode = table.Column<int>(type: "int", nullable: false),
                    CloudCoverTotal = table.Column<float>(type: "float", nullable: false),
                    SurfacePressure = table.Column<float>(type: "float", nullable: false),
                    Visibility = table.Column<float>(type: "float", nullable: false),
                    PrecipitationProbability = table.Column<float>(type: "float", nullable: false),
                    Rain = table.Column<float>(type: "float", nullable: false),
                    Showers = table.Column<float>(type: "float", nullable: false),
                    Snowfall = table.Column<float>(type: "float", nullable: false),
                    SoilTemperature_6cm = table.Column<float>(type: "float", nullable: false),
                    SoilMoisture_1_3cm = table.Column<float>(type: "float", nullable: false),
                    ForecastId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastWeatherData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForecastWeatherData_Forecasts_ForecastId",
                        column: x => x.ForecastId,
                        principalTable: "Forecasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_LocationId",
                table: "Forecasts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ForecastWeatherData_ForecastId",
                table: "ForecastWeatherData",
                column: "ForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_ForecastWeatherData_Id",
                table: "ForecastWeatherData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ForecastWeatherData_PredictionTimestamp",
                table: "ForecastWeatherData",
                column: "PredictionTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Id",
                table: "Locations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Name",
                table: "Locations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuredWeatherData_Id",
                table: "MeasuredWeatherData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuredWeatherData_LocationId",
                table: "MeasuredWeatherData",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuredWeatherData_Timestamp",
                table: "MeasuredWeatherData",
                column: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForecastWeatherData");

            migrationBuilder.DropTable(
                name: "MeasuredWeatherData");

            migrationBuilder.DropTable(
                name: "Forecasts");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
