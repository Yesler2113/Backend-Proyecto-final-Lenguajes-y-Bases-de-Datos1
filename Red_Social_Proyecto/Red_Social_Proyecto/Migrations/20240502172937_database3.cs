using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Red_Social_Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class database3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_CommentParentId",
                schema: "public",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_CommentParentId",
                schema: "public",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "CommentParentId",
                schema: "public",
                table: "comments",
                newName: "comment_parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "comment_parent_id",
                schema: "public",
                table: "comments",
                newName: "CommentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_CommentParentId",
                schema: "public",
                table: "comments",
                column: "CommentParentId");

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
    }
}
