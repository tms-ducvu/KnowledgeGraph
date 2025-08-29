using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgeGraph.Migrations
{
    /// <inheritdoc />
    public partial class AddtableEntitiesandEntityHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EntityBusinessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EntityEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityWebsite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityIsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppEntityHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EntityBusinessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EntityEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityWebsite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityIsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEntityHistories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEntities");

            migrationBuilder.DropTable(
                name: "AppEntityHistories");
        }
    }
}
