using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Red_Social_Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_user_id",
                schema: "security",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_users_followed_id",
                schema: "security",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_users_follower_id",
                schema: "security",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_interactions_users_user_id",
                schema: "security",
                table: "interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_users_user_id",
                schema: "security",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_publications_users_user_id",
                schema: "security",
                table: "publications");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "biography",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "links",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "photo_url",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "registration_date",
                schema: "security",
                table: "users");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "publications",
                schema: "security",
                newName: "publications",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "notifications",
                schema: "security",
                newName: "notifications",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "interactions",
                schema: "security",
                newName: "interactions",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "follows",
                schema: "security",
                newName: "follows",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "comments",
                schema: "security",
                newName: "comments",
                newSchema: "public");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    registration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    photo_url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    biography = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    links = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_users_Id",
                        column: x => x.Id,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_user_id",
                schema: "public",
                table: "comments",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_users_followed_id",
                schema: "public",
                table: "follows",
                column: "followed_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_users_follower_id",
                schema: "public",
                table: "follows",
                column: "follower_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_interactions_users_user_id",
                schema: "public",
                table: "interactions",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_users_user_id",
                schema: "public",
                table: "notifications",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_publications_users_user_id",
                schema: "public",
                table: "publications",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_user_id",
                schema: "public",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_users_followed_id",
                schema: "public",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_users_follower_id",
                schema: "public",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_interactions_users_user_id",
                schema: "public",
                table: "interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_users_user_id",
                schema: "public",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_publications_users_user_id",
                schema: "public",
                table: "publications");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.RenameTable(
                name: "publications",
                schema: "public",
                newName: "publications",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "notifications",
                schema: "public",
                newName: "notifications",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "interactions",
                schema: "public",
                newName: "interactions",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "follows",
                schema: "public",
                newName: "follows",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "comments",
                schema: "public",
                newName: "comments",
                newSchema: "security");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "security",
                table: "users",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "biography",
                schema: "security",
                table: "users",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "links",
                schema: "security",
                table: "users",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                schema: "security",
                table: "users",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "registration_date",
                schema: "security",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_user_id",
                schema: "security",
                table: "comments",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_users_followed_id",
                schema: "security",
                table: "follows",
                column: "followed_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_users_follower_id",
                schema: "security",
                table: "follows",
                column: "follower_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_interactions_users_user_id",
                schema: "security",
                table: "interactions",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_users_user_id",
                schema: "security",
                table: "notifications",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_publications_users_user_id",
                schema: "security",
                table: "publications",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
