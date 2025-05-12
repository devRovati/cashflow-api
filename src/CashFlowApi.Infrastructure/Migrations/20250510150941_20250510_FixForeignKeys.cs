using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlowApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20250510_FixForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_users_created_by",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_categories_users_updated_by",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_users_created_by",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_users_updated_by",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_categories_users_created_by",
                table: "categories",
                column: "created_by",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_categories_users_updated_by",
                table: "categories",
                column: "updated_by",
                principalTable: "users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_users_created_by",
                table: "transactions",
                column: "created_by",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_users_updated_by",
                table: "transactions",
                column: "updated_by",
                principalTable: "users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_users_created_by",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_categories_users_updated_by",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_users_created_by",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_users_updated_by",
                table: "transactions");

            migrationBuilder.AddForeignKey(
                name: "fk_categories_users_created_by",
                table: "categories",
                column: "created_by",
                principalTable: "categories",
                principalColumn: "category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_categories_users_updated_by",
                table: "categories",
                column: "updated_by",
                principalTable: "categories",
                principalColumn: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_users_created_by",
                table: "transactions",
                column: "created_by",
                principalTable: "transactions",
                principalColumn: "transaction_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_users_updated_by",
                table: "transactions",
                column: "updated_by",
                principalTable: "transactions",
                principalColumn: "transaction_id");
        }
    }
}
