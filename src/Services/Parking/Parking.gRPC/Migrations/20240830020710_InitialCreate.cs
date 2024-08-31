using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parking.gRPC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrecosBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValorHoraInicial = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorHoraAdicional = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecosBase", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PrecosBase",
                columns: new[] { "Id", "DataFim", "DataInicio", "ValorHoraAdicional", "ValorHoraInicial" },
                values: new object[,]
                {
                    { new Guid("ab82400c-427d-4fa3-b190-6c2e9ffb380d"), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.00m, 2.00m },
                    { new Guid("df523227-26c2-4593-9127-5c6da024c622"), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.75m, 1.50m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrecosBase");
        }
    }
}
