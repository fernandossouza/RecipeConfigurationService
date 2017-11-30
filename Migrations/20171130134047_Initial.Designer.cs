﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using recipeconfigurationservice.Data;
using recipeconfigurationservice.Model;
using System;

namespace recipeconfigurationservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171130134047_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("recipeconfigurationservice.Model.ApiConfiguration", b =>
                {
                    b.Property<int>("apiConfigurationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("endPoint")
                        .HasMaxLength(100);

                    b.Property<string>("method")
                        .HasMaxLength(10);

                    b.HasKey("apiConfigurationId");

                    b.ToTable("ApiConfiguration");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ApiLoad", b =>
                {
                    b.Property<int>("apiLoadId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("endPoint")
                        .HasMaxLength(100);

                    b.Property<string>("method")
                        .HasMaxLength(10);

                    b.HasKey("apiLoadId");

                    b.ToTable("Apiloads");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.Extract", b =>
                {
                    b.Property<int>("extractId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description")
                        .HasMaxLength(200);

                    b.Property<string>("enabled")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("extractId");

                    b.ToTable("Extracts");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ExtractConfiguration", b =>
                {
                    b.Property<int>("extractConfigurationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("apiConfigurationId");

                    b.Property<string>("description")
                        .HasMaxLength(200);

                    b.Property<int?>("extractId");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("sqlConfigurationId");

                    b.Property<int>("type");

                    b.HasKey("extractConfigurationId");

                    b.HasIndex("apiConfigurationId");

                    b.HasIndex("extractId");

                    b.HasIndex("sqlConfigurationId");

                    b.ToTable("ExtractConfigurations");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ExtractInParameter", b =>
                {
                    b.Property<int>("extractInParameterId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("apiConfigurationId");

                    b.Property<string>("nameParameter")
                        .HasMaxLength(50);

                    b.Property<string>("path")
                        .HasMaxLength(50);

                    b.Property<int?>("sqlConfigurationId");

                    b.Property<int>("type");

                    b.Property<string>("value")
                        .HasMaxLength(50);

                    b.HasKey("extractInParameterId");

                    b.HasIndex("apiConfigurationId");

                    b.HasIndex("sqlConfigurationId");

                    b.ToTable("ExtractInParameters");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ExtractOutParameter", b =>
                {
                    b.Property<int>("extractOutParameterId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("apiConfigurationId");

                    b.Property<string>("localName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("path")
                        .HasMaxLength(50);

                    b.Property<int?>("sqlConfigurationId");

                    b.Property<string>("value")
                        .HasMaxLength(50);

                    b.HasKey("extractOutParameterId");

                    b.HasIndex("apiConfigurationId");

                    b.HasIndex("sqlConfigurationId");

                    b.ToTable("ExtractOutParameters");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.Load", b =>
                {
                    b.Property<int>("loadId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description")
                        .HasMaxLength(100);

                    b.Property<int>("extractId");

                    b.Property<string>("name")
                        .HasMaxLength(50);

                    b.HasKey("loadId");

                    b.ToTable("Loads");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.LoadConfiguration", b =>
                {
                    b.Property<int>("loadConfigurationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("apiLoadId");

                    b.Property<string>("description")
                        .HasMaxLength(100);

                    b.Property<int?>("loadId");

                    b.Property<string>("name")
                        .HasMaxLength(50);

                    b.Property<int?>("sqlLoadId");

                    b.Property<int>("type");

                    b.HasKey("loadConfigurationId");

                    b.HasIndex("apiLoadId");

                    b.HasIndex("loadId");

                    b.HasIndex("sqlLoadId");

                    b.ToTable("LoadConfigurations");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ParameterLoad", b =>
                {
                    b.Property<int>("parameterLoadId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("apiLoadId");

                    b.Property<string>("jsonName")
                        .HasMaxLength(50);

                    b.Property<string>("localName")
                        .HasMaxLength(50);

                    b.Property<string>("queryParameter")
                        .HasMaxLength(50);

                    b.Property<int?>("sqlLoadId");

                    b.Property<int>("type");

                    b.Property<string>("value")
                        .HasMaxLength(50);

                    b.HasKey("parameterLoadId");

                    b.HasIndex("apiLoadId");

                    b.HasIndex("sqlLoadId");

                    b.ToTable("ParameterLoads");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.SqlConfiguration", b =>
                {
                    b.Property<int>("sqlConfigurationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("commandSQL")
                        .HasMaxLength(200);

                    b.Property<string>("stringConnection")
                        .HasMaxLength(100);

                    b.Property<int>("typeDb");

                    b.HasKey("sqlConfigurationId");

                    b.ToTable("SqlConfiguration");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.SqlLoad", b =>
                {
                    b.Property<int>("sqlLoadId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("commandSQL")
                        .HasMaxLength(100);

                    b.Property<string>("stringConnection")
                        .HasMaxLength(100);

                    b.Property<int>("typeDB");

                    b.HasKey("sqlLoadId");

                    b.ToTable("SqlLoads");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ExtractConfiguration", b =>
                {
                    b.HasOne("recipeconfigurationservice.Model.ApiConfiguration", "apiConfiguration")
                        .WithMany()
                        .HasForeignKey("apiConfigurationId");

                    b.HasOne("recipeconfigurationservice.Model.Extract")
                        .WithMany("extractConfiguration")
                        .HasForeignKey("extractId");

                    b.HasOne("recipeconfigurationservice.Model.SqlConfiguration", "sqlConfiguration")
                        .WithMany()
                        .HasForeignKey("sqlConfigurationId");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ExtractInParameter", b =>
                {
                    b.HasOne("recipeconfigurationservice.Model.ApiConfiguration")
                        .WithMany("input")
                        .HasForeignKey("apiConfigurationId");

                    b.HasOne("recipeconfigurationservice.Model.SqlConfiguration")
                        .WithMany("input")
                        .HasForeignKey("sqlConfigurationId");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ExtractOutParameter", b =>
                {
                    b.HasOne("recipeconfigurationservice.Model.ApiConfiguration")
                        .WithMany("output")
                        .HasForeignKey("apiConfigurationId");

                    b.HasOne("recipeconfigurationservice.Model.SqlConfiguration")
                        .WithMany("output")
                        .HasForeignKey("sqlConfigurationId");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.LoadConfiguration", b =>
                {
                    b.HasOne("recipeconfigurationservice.Model.ApiLoad", "apiLoad")
                        .WithMany()
                        .HasForeignKey("apiLoadId");

                    b.HasOne("recipeconfigurationservice.Model.Load")
                        .WithMany("loadConfiguration")
                        .HasForeignKey("loadId");

                    b.HasOne("recipeconfigurationservice.Model.SqlLoad", "sqlLoad")
                        .WithMany()
                        .HasForeignKey("sqlLoadId");
                });

            modelBuilder.Entity("recipeconfigurationservice.Model.ParameterLoad", b =>
                {
                    b.HasOne("recipeconfigurationservice.Model.ApiLoad")
                        .WithMany("parameterLoad")
                        .HasForeignKey("apiLoadId");

                    b.HasOne("recipeconfigurationservice.Model.SqlLoad")
                        .WithMany("parameterLoad")
                        .HasForeignKey("sqlLoadId");
                });
#pragma warning restore 612, 618
        }
    }
}