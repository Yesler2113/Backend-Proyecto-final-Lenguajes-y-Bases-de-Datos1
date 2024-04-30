using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Red_Social_Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class AddVirtualClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "security",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "user_name",
                schema: "security",
                table: "users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                schema: "security",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalized_email",
                schema: "security",
                table: "users",
                newName: "NormalizedEmail");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "security",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "security",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                schema: "security",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                schema: "security",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<Guid>(
                name: "InteractionEntityId",
                schema: "public",
                table: "interactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCommentId",
                schema: "public",
                table: "comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_interactions_InteractionEntityId",
                schema: "public",
                table: "interactions",
                column: "InteractionEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_ParentCommentId",
                schema: "public",
                table: "comments",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                schema: "public",
                table: "comments",
                column: "ParentCommentId",
                principalSchema: "public",
                principalTable: "comments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments",
                column: "comment_parent_id",
                principalSchema: "public",
                principalTable: "comments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_interactions_interactions_InteractionEntityId",
                schema: "public",
                table: "interactions",
                column: "InteractionEntityId",
                principalSchema: "public",
                principalTable: "interactions",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                schema: "public",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_interactions_interactions_InteractionEntityId",
                schema: "public",
                table: "interactions");

            migrationBuilder.DropIndex(
                name: "IX_interactions_InteractionEntityId",
                schema: "public",
                table: "interactions");

            migrationBuilder.DropIndex(
                name: "IX_comments_ParentCommentId",
                schema: "public",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "InteractionEntityId",
                schema: "public",
                table: "interactions");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                schema: "public",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "security",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "security",
                table: "users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "security",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                schema: "security",
                table: "users",
                newName: "normalized_email");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "security",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                schema: "security",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                schema: "security",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "normalized_email",
                schema: "security",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_comment_parent_id",
                schema: "public",
                table: "comments",
                column: "comment_parent_id",
                principalSchema: "public",
                principalTable: "comments",
                principalColumn: "id");
        }
    }
}
