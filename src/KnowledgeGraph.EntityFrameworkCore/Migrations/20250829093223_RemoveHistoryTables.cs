using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgeGraph.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHistoryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove ReviewHistories table
            migrationBuilder.DropTable(
                name: "AppReviewHistories");

            // Remove EntityHistories table  
            migrationBuilder.DropTable(
                name: "AppEntityHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate EntityHistories table
            migrationBuilder.CreateTable(
                name: "AppEntityHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EntityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EntityBusinessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityAddress = table.Column<string>(type: "text", nullable: true),
                    EntityPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EntityEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityIsActive = table.Column<bool>(type: "bit", nullable: false),
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

            // Recreate ReviewHistories table
            migrationBuilder.CreateTable(
                name: "AppReviewHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReviewPlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewRating = table.Column<int>(type: "int", nullable: false),
                    ReviewContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewSyncTime = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_AppReviewHistories", x => x.Id);
                });
        }
    }
}
