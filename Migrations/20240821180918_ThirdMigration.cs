using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotivationalQuotes.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Quotes_QuoteId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Users_PostedByUserId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Quotes_QuoteId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Quotes_QuoteId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_QuoteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "QuoteId1",
                table: "Users",
                newName: "FavoriteQuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_QuoteId1",
                table: "Users",
                newName: "IX_Users_FavoriteQuoteId");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserLikedQuotes",
                columns: table => new
                {
                    LikedQuotesQuoteId = table.Column<int>(type: "int", nullable: false),
                    LikesUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikedQuotes", x => new { x.LikedQuotesQuoteId, x.LikesUserId });
                    table.ForeignKey(
                        name: "FK_UserLikedQuotes_Quotes_LikedQuotesQuoteId",
                        column: x => x.LikedQuotesQuoteId,
                        principalTable: "Quotes",
                        principalColumn: "QuoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikedQuotes_Users_LikesUserId",
                        column: x => x.LikesUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserSharedQuotes",
                columns: table => new
                {
                    SharedByUserId = table.Column<int>(type: "int", nullable: false),
                    SharedQuotesQuoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSharedQuotes", x => new { x.SharedByUserId, x.SharedQuotesQuoteId });
                    table.ForeignKey(
                        name: "FK_UserSharedQuotes_Quotes_SharedQuotesQuoteId",
                        column: x => x.SharedQuotesQuoteId,
                        principalTable: "Quotes",
                        principalColumn: "QuoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSharedQuotes_Users_SharedByUserId",
                        column: x => x.SharedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedQuotes_LikesUserId",
                table: "UserLikedQuotes",
                column: "LikesUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSharedQuotes_SharedQuotesQuoteId",
                table: "UserSharedQuotes",
                column: "SharedQuotesQuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Quotes_QuoteId",
                table: "Comments",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "QuoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Users_PostedByUserId",
                table: "Quotes",
                column: "PostedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Quotes_FavoriteQuoteId",
                table: "Users",
                column: "FavoriteQuoteId",
                principalTable: "Quotes",
                principalColumn: "QuoteId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Quotes_QuoteId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Users_PostedByUserId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Quotes_FavoriteQuoteId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserLikedQuotes");

            migrationBuilder.DropTable(
                name: "UserSharedQuotes");

            migrationBuilder.RenameColumn(
                name: "FavoriteQuoteId",
                table: "Users",
                newName: "QuoteId1");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FavoriteQuoteId",
                table: "Users",
                newName: "IX_Users_QuoteId1");

            migrationBuilder.AddColumn<int>(
                name: "QuoteId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuoteId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Users_QuoteId",
                table: "Users",
                column: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Quotes_QuoteId",
                table: "Comments",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Users_PostedByUserId",
                table: "Quotes",
                column: "PostedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Quotes_QuoteId",
                table: "Users",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Quotes_QuoteId1",
                table: "Users",
                column: "QuoteId1",
                principalTable: "Quotes",
                principalColumn: "QuoteId");
        }
    }
}
