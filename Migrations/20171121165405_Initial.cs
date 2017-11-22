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
                name: "ApiConfiguration",
                columns: table => new
                {
                    apiConfigurationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    endPoint = table.Column<string>(type: "text", nullable: true),
                    method = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiConfiguration", x => x.apiConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "Extracts",
                columns: table => new
                {
                    extractId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    enabled = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extracts", x => x.extractId);
                });

            migrationBuilder.CreateTable(
                name: "SqlConfiguration",
                columns: table => new
                {
                    sqlConfigurationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    commandSQL = table.Column<string>(type: "text", nullable: true),
                    stringConection = table.Column<string>(type: "text", nullable: true),
                    typeDb = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlConfiguration", x => x.sqlConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "ExtractConfigurations",
                columns: table => new
                {
                    extractConfigurationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    apiConfigurationId = table.Column<int>(type: "int4", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    extractId = table.Column<int>(type: "int4", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    sqlConfigurationId = table.Column<int>(type: "int4", nullable: true),
                    type = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractConfigurations", x => x.extractConfigurationId);
                    table.ForeignKey(
                        name: "FK_ExtractConfigurations_ApiConfiguration_apiConfigurationId",
                        column: x => x.apiConfigurationId,
                        principalTable: "ApiConfiguration",
                        principalColumn: "apiConfigurationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtractConfigurations_Extracts_extractId",
                        column: x => x.extractId,
                        principalTable: "Extracts",
                        principalColumn: "extractId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtractConfigurations_SqlConfiguration_sqlConfigurationId",
                        column: x => x.sqlConfigurationId,
                        principalTable: "SqlConfiguration",
                        principalColumn: "sqlConfigurationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtractInParameters",
                columns: table => new
                {
                    extractInParameterId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    extractConfigurationId = table.Column<int>(type: "int4", nullable: true),
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
                    extractOutParameterId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    extractConfigurationId = table.Column<int>(type: "int4", nullable: true),
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
                name: "IX_ExtractConfigurations_apiConfigurationId",
                table: "ExtractConfigurations",
                column: "apiConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractConfigurations_extractId",
                table: "ExtractConfigurations",
                column: "extractId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractConfigurations_sqlConfigurationId",
                table: "ExtractConfigurations",
                column: "sqlConfigurationId");

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
                name: "ApiConfiguration");

            migrationBuilder.DropTable(
                name: "Extracts");

            migrationBuilder.DropTable(
                name: "SqlConfiguration");
        }
    }
}
