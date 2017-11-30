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
                    endPoint = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    method = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiConfiguration", x => x.apiConfigurationId);
                });

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
                name: "SqlConfiguration",
                columns: table => new
                {
                    sqlConfigurationId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    commandSQL = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    stringConnection = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    typeDb = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlConfiguration", x => x.sqlConfigurationId);
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
                    apiConfigurationId = table.Column<int>(type: "int4", nullable: true),
                    nameParameter = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    path = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    sqlConfigurationId = table.Column<int>(type: "int4", nullable: true),
                    type = table.Column<int>(type: "int4", nullable: false),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractInParameters", x => x.extractInParameterId);
                    table.ForeignKey(
                        name: "FK_ExtractInParameters_ApiConfiguration_apiConfigurationId",
                        column: x => x.apiConfigurationId,
                        principalTable: "ApiConfiguration",
                        principalColumn: "apiConfigurationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtractInParameters_SqlConfiguration_sqlConfigurationId",
                        column: x => x.sqlConfigurationId,
                        principalTable: "SqlConfiguration",
                        principalColumn: "sqlConfigurationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtractOutParameters",
                columns: table => new
                {
                    extractOutParameterId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    apiConfigurationId = table.Column<int>(type: "int4", nullable: true),
                    localName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    path = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    sqlConfigurationId = table.Column<int>(type: "int4", nullable: true),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtractOutParameters", x => x.extractOutParameterId);
                    table.ForeignKey(
                        name: "FK_ExtractOutParameters_ApiConfiguration_apiConfigurationId",
                        column: x => x.apiConfigurationId,
                        principalTable: "ApiConfiguration",
                        principalColumn: "apiConfigurationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtractOutParameters_SqlConfiguration_sqlConfigurationId",
                        column: x => x.sqlConfigurationId,
                        principalTable: "SqlConfiguration",
                        principalColumn: "sqlConfigurationId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_ExtractInParameters_apiConfigurationId",
                table: "ExtractInParameters",
                column: "apiConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractInParameters_sqlConfigurationId",
                table: "ExtractInParameters",
                column: "sqlConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractOutParameters_apiConfigurationId",
                table: "ExtractOutParameters",
                column: "apiConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractOutParameters_sqlConfigurationId",
                table: "ExtractOutParameters",
                column: "sqlConfigurationId");

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
                name: "ExtractConfigurations");

            migrationBuilder.DropTable(
                name: "ExtractInParameters");

            migrationBuilder.DropTable(
                name: "ExtractOutParameters");

            migrationBuilder.DropTable(
                name: "LoadConfigurations");

            migrationBuilder.DropTable(
                name: "ParameterLoads");

            migrationBuilder.DropTable(
                name: "Extracts");

            migrationBuilder.DropTable(
                name: "ApiConfiguration");

            migrationBuilder.DropTable(
                name: "SqlConfiguration");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "Apiloads");

            migrationBuilder.DropTable(
                name: "SqlLoads");
        }
    }
}
