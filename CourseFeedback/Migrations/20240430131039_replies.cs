using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseFeedback.Migrations
{
    /// <inheritdoc />
    public partial class replies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommentsId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentsId",
                table: "Comments",
                column: "CommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentsId",
                table: "Comments",
                column: "CommentsId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentsId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentsId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentsId",
                table: "Comments");
        }
    }
}
