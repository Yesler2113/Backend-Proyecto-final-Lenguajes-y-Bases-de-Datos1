using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Red_Social_Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCommetsParent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "comment_parent_id",
                schema: "public",
                table: "comments",
                newName: "CommentParentId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_comment_parent_id",
                schema: "public",
                table: "comments",
                newName: "IX_comments_CommentParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_CommentParentId",
                schema: "public",
                table: "comments",
                column: "CommentParentId",
                principalSchema: "public",
                principalTable: "comments",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_CommentParentId",
                schema: "public",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "CommentParentId",
                schema: "public",
                table: "comments",
                newName: "comment_parent_id");

            migrationBuilder.RenameIndex(
                name: "IX_comments_CommentParentId",
                schema: "public",
                table: "comments",
                newName: "IX_comments_comment_parent_id");

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
    }
}
