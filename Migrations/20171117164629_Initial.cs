using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeconfigurationservice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extracts",
                columns: table => new
                {
                    extractId = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extracts", x => x.extractId);
                });

            migrationBuilder.CreateTable(
                name: "ExtractConfigurations",
                columns: table => new
                {
                    extractConfigurationId = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    commandSQL = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    endPoint = table.Column<string>(type: "text", nullable: true),
                    extractId = table.Column<long>(type: "int8", nullable: true),
                    method = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    stringConection = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractConfigurations", x => x.extractConfigurationId);
                    table.ForeignKey(
                        name: "FK_ExtractConfigurations_Extracts_extractId",
                        column: x => x.extractId,
                        principalTable: "Extracts",
                        principalColumn: "extractId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtractInParameters",
                columns: table => new
                {
                    extractInParameterId = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    extractConfigurationId = table.Column<long>(type: "int8", nullable: true),
                    path = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractInParameters", x => x.extractInParameterId);
                    table.ForeignKey(
                        name: "FK_ExtractInParameters_ExtractConfigurations_extractConfigurationId",
                        column: x => x.extractConfigurationId,
                        principalTable: "ExtractConfigurations",
                        principalColumn: "extractConfigurationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtractOutParameters",
                columns: table => new
                {
                    extractOutParameterId = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    extractConfigurationId = table.Column<long>(type: "int8", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    path = table.Column<string>(type: "text", nullable: true),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractOutParameters", x => x.extractOutParameterId);
                    table.ForeignKey(
                        name: "FK_ExtractOutParameters_ExtractConfigurations_extractConfigurationId",
                        column: x => x.extractConfigurationId,
                        principalTable: "ExtractConfigurations",
                        principalColumn: "extractConfigurationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtractConfigurations_extractId",
                table: "ExtractConfigurations",
                column: "extractId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractInParameters_extractConfigurationId",
                table: "ExtractInParameters",
                column: "extractConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractOutParameters_extractConfigurationId",
                table: "ExtractOutParameters",
                column: "extractConfigurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtractInParameters");

            migrationBuilder.DropTable(
                name: "ExtractOutParameters");

            migrationBuilder.DropTable(
                name: "ExtractConfigurations");

            migrationBuilder.DropTable(
                name: "Extracts");
        }
    }
}
