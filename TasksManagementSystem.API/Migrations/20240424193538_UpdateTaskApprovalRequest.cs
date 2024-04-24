using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksManagementSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskApprovalRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraWork",
                table: "TaskApprovalRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraWork",
                table: "TaskApprovalRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
