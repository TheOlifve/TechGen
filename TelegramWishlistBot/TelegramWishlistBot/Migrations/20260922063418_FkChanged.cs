using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramWishlistBot.Migrations
{
    /// <inheritdoc />
    public partial class FkChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemUrls_WishlistItems_ItemId",
                table: "ItemUrls");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ItemUrls",
                newName: "WishlistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemUrls_ItemId",
                table: "ItemUrls",
                newName: "IX_ItemUrls_WishlistItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUrls_WishlistItems_WishlistItemId",
                table: "ItemUrls",
                column: "WishlistItemId",
                principalTable: "WishlistItems",
                principalColumn: "WishlistItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemUrls_WishlistItems_WishlistItemId",
                table: "ItemUrls");

            migrationBuilder.RenameColumn(
                name: "WishlistItemId",
                table: "ItemUrls",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemUrls_WishlistItemId",
                table: "ItemUrls",
                newName: "IX_ItemUrls_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUrls_WishlistItems_ItemId",
                table: "ItemUrls",
                column: "ItemId",
                principalTable: "WishlistItems",
                principalColumn: "WishlistItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
