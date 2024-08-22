using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotivationalQuotes.Migrations
{
    /// <inheritdoc />
    public partial class changes2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropIndex(
                name: "IX_Users_FavoriteQuoteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FavoriteQuoteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "FavoriteQuotes",
                columns: table => new
                {
                    FavoriteQuoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteQuotes", x => x.FavoriteQuoteId);
                    table.ForeignKey(
                        name: "FK_FavoriteQuotes_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "QuoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteQuotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteQuotes_QuoteId",
                table: "FavoriteQuotes",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteQuotes_UserId",
                table: "FavoriteQuotes",
                column: "UserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Users_PostedByUserId",
                table: "Quotes");

            migrationBuilder.DropTable(
                name: "FavoriteQuotes");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteQuoteId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "IX_Users_FavoriteQuoteId",
                table: "Users",
                column: "FavoriteQuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedQuotes_LikesUserId",
                table: "UserLikedQuotes",
                column: "LikesUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSharedQuotes_SharedQuotesQuoteId",
                table: "UserSharedQuotes",
                column: "SharedQuotesQuoteId");

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
    }
}
