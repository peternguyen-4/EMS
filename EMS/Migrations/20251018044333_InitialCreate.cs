using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    notificationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userID = table.Column<int>(type: "INTEGER", nullable: false),
                    creationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    terminationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.notificationID);
                });

            migrationBuilder.CreateTable(
                name: "SoilSamples",
                columns: table => new
                {
                    sampleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firmness = table.Column<int>(type: "INTEGER", nullable: false),
                    pH = table.Column<float>(type: "REAL", nullable: false),
                    density = table.Column<float>(type: "REAL", nullable: false),
                    moisture = table.Column<float>(type: "REAL", nullable: false),
                    nitrogen = table.Column<float>(type: "REAL", nullable: false),
                    organicMatter = table.Column<float>(type: "REAL", nullable: false),
                    microbiology = table.Column<string>(type: "TEXT", nullable: false),
                    contaminants = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilSamples", x => x.sampleID);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    speciesID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    speciesName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.speciesID);
                });

            migrationBuilder.CreateTable(
                name: "SpeciesData",
                columns: table => new
                {
                    sampleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    speciesID = table.Column<int>(type: "INTEGER", nullable: false),
                    populationCount = table.Column<int>(type: "INTEGER", nullable: false),
                    scatCount = table.Column<int>(type: "INTEGER", nullable: false),
                    reproductiveFactor = table.Column<float>(type: "REAL", nullable: false),
                    knownHabitats = table.Column<string>(type: "TEXT", nullable: false),
                    healthConcerns = table.Column<string>(type: "TEXT", nullable: false),
                    additionalNotes = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciesData", x => x.sampleID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    taskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userID = table.Column<int>(type: "INTEGER", nullable: false),
                    creationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.taskID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userName = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    firstName = table.Column<string>(type: "TEXT", nullable: false),
                    lastName = table.Column<string>(type: "TEXT", nullable: false),
                    role = table.Column<string>(type: "TEXT", nullable: false),
                    assignedZone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "WaterSamples",
                columns: table => new
                {
                    sampleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    pH = table.Column<float>(type: "REAL", nullable: false),
                    dissolvedOxygen = table.Column<float>(type: "REAL", nullable: false),
                    salinity = table.Column<float>(type: "REAL", nullable: false),
                    turbidity = table.Column<float>(type: "REAL", nullable: false),
                    hardness = table.Column<float>(type: "REAL", nullable: false),
                    eutrophicPotential = table.Column<float>(type: "REAL", nullable: false),
                    microbiology = table.Column<string>(type: "TEXT", nullable: false),
                    contaminants = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterSamples", x => x.sampleID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SoilSamples");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "SpeciesData");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WaterSamples");
        }
    }
}
