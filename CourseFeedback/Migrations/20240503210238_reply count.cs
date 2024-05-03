using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseFeedback.Migrations
{
    /// <inheritdoc />
    public partial class replycount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfReplies",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfReplies",
                table: "Comments");
        }
    }
}
