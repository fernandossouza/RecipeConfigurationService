using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace recipeconfigurationservice.Migrations
{
    public partial class ChangeParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtractInParameters_ExtractConfigurations_extractConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtractOutParameters_ExtractConfigurations_extractConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropIndex(
                name: "IX_ExtractOutParameters_extractConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropIndex(
                name: "IX_ExtractInParameters_extractConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.DropColumn(
                name: "extractConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropColumn(
                name: "name",
                table: "ExtractOutParameters");

            migrationBuilder.DropColumn(
                name: "extractConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.AlterColumn<string>(
                name: "stringConection",
                table: "SqlConfiguration",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "commandSQL",
                table: "SqlConfiguration",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "ExtractOutParameters",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "ExtractOutParameters",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "apiConfigurationId",
                table: "ExtractOutParameters",
                type: "int4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "localName",
                table: "ExtractOutParameters",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "sqlConfigurationId",
                table: "ExtractOutParameters",
                type: "int4",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "ExtractInParameters",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "ExtractInParameters",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "apiConfigurationId",
                table: "ExtractInParameters",
                type: "int4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sqlConfigurationId",
                table: "ExtractInParameters",
                type: "int4",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "method",
                table: "ApiConfiguration",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "endPoint",
                table: "ApiConfiguration",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtractOutParameters_apiConfigurationId",
                table: "ExtractOutParameters",
                column: "apiConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractOutParameters_sqlConfigurationId",
                table: "ExtractOutParameters",
                column: "sqlConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractInParameters_apiConfigurationId",
                table: "ExtractInParameters",
                column: "apiConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractInParameters_sqlConfigurationId",
                table: "ExtractInParameters",
                column: "sqlConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtractInParameters_ApiConfiguration_apiConfigurationId",
                table: "ExtractInParameters",
                column: "apiConfigurationId",
                principalTable: "ApiConfiguration",
                principalColumn: "apiConfigurationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtractInParameters_SqlConfiguration_sqlConfigurationId",
                table: "ExtractInParameters",
                column: "sqlConfigurationId",
                principalTable: "SqlConfiguration",
                principalColumn: "sqlConfigurationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtractOutParameters_ApiConfiguration_apiConfigurationId",
                table: "ExtractOutParameters",
                column: "apiConfigurationId",
                principalTable: "ApiConfiguration",
                principalColumn: "apiConfigurationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtractOutParameters_SqlConfiguration_sqlConfigurationId",
                table: "ExtractOutParameters",
                column: "sqlConfigurationId",
                principalTable: "SqlConfiguration",
                principalColumn: "sqlConfigurationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtractInParameters_ApiConfiguration_apiConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtractInParameters_SqlConfiguration_sqlConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtractOutParameters_ApiConfiguration_apiConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtractOutParameters_SqlConfiguration_sqlConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropIndex(
                name: "IX_ExtractOutParameters_apiConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropIndex(
                name: "IX_ExtractOutParameters_sqlConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropIndex(
                name: "IX_ExtractInParameters_apiConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.DropIndex(
                name: "IX_ExtractInParameters_sqlConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.DropColumn(
                name: "apiConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropColumn(
                name: "localName",
                table: "ExtractOutParameters");

            migrationBuilder.DropColumn(
                name: "sqlConfigurationId",
                table: "ExtractOutParameters");

            migrationBuilder.DropColumn(
                name: "apiConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.DropColumn(
                name: "sqlConfigurationId",
                table: "ExtractInParameters");

            migrationBuilder.AlterColumn<string>(
                name: "stringConection",
                table: "SqlConfiguration",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "commandSQL",
                table: "SqlConfiguration",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "ExtractOutParameters",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "ExtractOutParameters",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "extractConfigurationId",
                table: "ExtractOutParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "ExtractOutParameters",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "ExtractInParameters",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "ExtractInParameters",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "extractConfigurationId",
                table: "ExtractInParameters",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "method",
                table: "ApiConfiguration",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "endPoint",
                table: "ApiConfiguration",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtractOutParameters_extractConfigurationId",
                table: "ExtractOutParameters",
                column: "extractConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtractInParameters_extractConfigurationId",
                table: "ExtractInParameters",
                column: "extractConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtractInParameters_ExtractConfigurations_extractConfigurationId",
                table: "ExtractInParameters",
                column: "extractConfigurationId",
                principalTable: "ExtractConfigurations",
                principalColumn: "extractConfigurationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtractOutParameters_ExtractConfigurations_extractConfigurationId",
                table: "ExtractOutParameters",
                column: "extractConfigurationId",
                principalTable: "ExtractConfigurations",
                principalColumn: "extractConfigurationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
