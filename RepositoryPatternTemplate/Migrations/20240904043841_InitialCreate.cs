using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepositoryPatternTemplate.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeatherDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TemperatureC = table.Column<int>(type: "integer", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "Summary", "TemperatureC", "WeatherDate" },
                values: new object[,]
                {
                    { 1, "Hot", 25, new DateTime(2024, 9, 4, 4, 38, 40, 973, DateTimeKind.Utc).AddTicks(795) },
                    { 2, "Warm", 20, new DateTime(2024, 9, 4, 4, 38, 40, 973, DateTimeKind.Utc).AddTicks(797) },
                    { 3, "Cool", 15, new DateTime(2024, 9, 4, 4, 38, 40, 973, DateTimeKind.Utc).AddTicks(799) },
                    { 4, "Cold", 10, new DateTime(2024, 9, 4, 4, 38, 40, 973, DateTimeKind.Utc).AddTicks(800) },
                    { 5, "Freezing", 5, new DateTime(2024, 9, 4, 4, 38, 40, 973, DateTimeKind.Utc).AddTicks(801) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecasts");
        }
    }
}
