using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Red_Social_Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class addusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments",
                column: "comment_parent_id",
                principalSchema: "public",
                principalTable: "comments",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments",
                column: "comment_parent_id",
                principalSchema: "public",
                principalTable: "comments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
