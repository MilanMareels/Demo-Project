using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AP.Demo_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "City");

            migrationBuilder.EnsureSchema(
                name: "Country");

            migrationBuilder.CreateTable(
                name: "tblCountries",
                schema: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCities",
                schema: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCities_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Country",
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Country",
                table: "tblCountries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "United States" },
                    { 2, "United Kingdom" },
                    { 3, "Belgium" },
                    { 4, "France" },
                    { 5, "Netherlands" }
                });

            migrationBuilder.InsertData(
                schema: "City",
                table: "tblCities",
                columns: new[] { "Id", "CountryId", "Name", "Population" },
                values: new object[,]
                {
                    { 1, 1, "New York", 8419600L },
                    { 2, 1, "Los Angeles", 3980400L },
                    { 3, 1, "Chicago", 2716000L },
                    { 4, 1, "Houston", 2328000L },
                    { 5, 1, "Phoenix", 1690000L },
                    { 6, 2, "London", 8982000L },
                    { 7, 2, "Birmingham", 1141000L },
                    { 8, 2, "Leeds", 789000L },
                    { 9, 2, "Glasgow", 635000L },
                    { 10, 2, "Sheffield", 584000L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_CountryId",
                schema: "City",
                table: "tblCities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_Name",
                schema: "City",
                table: "tblCities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCountries_Name",
                schema: "Country",
                table: "tblCountries",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCities",
                schema: "City");

            migrationBuilder.DropTable(
                name: "tblCountries",
                schema: "Country");
        }
    }
}
