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
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PredictionTimestamp = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    ForecastTimestamp = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Temperature = table.Column<float>(type: "float", nullable: false),
                    Humidity = table.Column<float>(type: "float", nullable: false),
                    Windspeed = table.Column<float>(type: "float", nullable: false),
                    WeatherCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
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
                    WeatherCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuredWeatherData", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_ForecastTimestamp",
                table: "Forecasts",
                column: "ForecastTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_Id",
                table: "Forecasts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_PredictionTimestamp",
                table: "Forecasts",
                column: "PredictionTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuredWeatherData_Id",
                table: "MeasuredWeatherData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuredWeatherData_Timestamp",
                table: "MeasuredWeatherData",
                column: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forecasts");

            migrationBuilder.DropTable(
                name: "MeasuredWeatherData");
        }
    }
}
