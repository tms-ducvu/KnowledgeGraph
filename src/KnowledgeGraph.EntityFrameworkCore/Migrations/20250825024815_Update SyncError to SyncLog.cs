using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgeGraph.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSyncErrortoSyncLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SyncError",
                table: "AppContactHistories",
                newName: "SyncLog");

            migrationBuilder.AlterColumn<string>(
                name: "SyncStatus",
                table: "AppContactHistories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SyncLog",
                table: "AppContactHistories",
                newName: "SyncError");

            migrationBuilder.AlterColumn<string>(
                name: "SyncStatus",
                table: "AppContactHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
