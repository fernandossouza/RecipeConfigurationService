using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeconfigurationservice.Migrations
{
    public partial class CreateLoad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stringConection",
                table: "SqlConfiguration");

            migrationBuilder.AddColumn<string>(
                name: "stringConnection",
                table: "SqlConfiguration",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Apiloads",
                columns: table => new
                {
                    apiLoadId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    endPoint = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    method = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apiloads", x => x.apiLoadId);
                });

            migrationBuilder.CreateTable(
                name: "Loads",
                columns: table => new
                {
                    loadId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    extractId = table.Column<int>(type: "int4", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loads", x => x.loadId);
                });

            migrationBuilder.CreateTable(
                name: "SqlLoads",
                columns: table => new
                {
                    sqlLoadId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    commandSQL = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    stringConnection = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    typeDB = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlLoads", x => x.sqlLoadId);
                });

            migrationBuilder.CreateTable(
                name: "LoadConfigurations",
                columns: table => new
                {
                    loadConfigurationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    apiLoadId = table.Column<int>(type: "int4", nullable: true),
                    description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    loadId = table.Column<int>(type: "int4", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    sqlLoadId = table.Column<int>(type: "int4", nullable: true),
                    type = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadConfigurations", x => x.loadConfigurationId);
                    table.ForeignKey(
                        name: "FK_LoadConfigurations_Apiloads_apiLoadId",
                        column: x => x.apiLoadId,
                        principalTable: "Apiloads",
                        principalColumn: "apiLoadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoadConfigurations_Loads_loadId",
                        column: x => x.loadId,
                        principalTable: "Loads",
                        principalColumn: "loadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoadConfigurations_SqlLoads_sqlLoadId",
                        column: x => x.sqlLoadId,
                        principalTable: "SqlLoads",
                        principalColumn: "sqlLoadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParameterLoads",
                columns: table => new
                {
                    parameterLoadId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    apiLoadId = table.Column<int>(type: "int4", nullable: true),
                    jsonName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    localName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    queryParameter = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    sqlLoadId = table.Column<int>(type: "int4", nullable: true),
                    type = table.Column<int>(type: "int4", nullable: false),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterLoads", x => x.parameterLoadId);
                    table.ForeignKey(
                        name: "FK_ParameterLoads_Apiloads_apiLoadId",
                        column: x => x.apiLoadId,
                        principalTable: "Apiloads",
                        principalColumn: "apiLoadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParameterLoads_SqlLoads_sqlLoadId",
                        column: x => x.sqlLoadId,
                        principalTable: "SqlLoads",
                        principalColumn: "sqlLoadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoadConfigurations_apiLoadId",
                table: "LoadConfigurations",
                column: "apiLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadConfigurations_loadId",
                table: "LoadConfigurations",
                column: "loadId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadConfigurations_sqlLoadId",
                table: "LoadConfigurations",
                column: "sqlLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterLoads_apiLoadId",
                table: "ParameterLoads",
                column: "apiLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterLoads_sqlLoadId",
                table: "ParameterLoads",
                column: "sqlLoadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoadConfigurations");

            migrationBuilder.DropTable(
                name: "ParameterLoads");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "Apiloads");

            migrationBuilder.DropTable(
                name: "SqlLoads");

            migrationBuilder.DropColumn(
                name: "stringConnection",
                table: "SqlConfiguration");

            migrationBuilder.AddColumn<string>(
                name: "stringConection",
                table: "SqlConfiguration",
                maxLength: 100,
                nullable: true);
        }
    }
}
