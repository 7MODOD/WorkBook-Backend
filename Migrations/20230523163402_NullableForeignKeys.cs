using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkBook.Migrations
{
    /// <inheritdoc />
    public partial class NullableForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Customer_CustomerId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Worker_WorkerId",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Project",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Comment",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Comment",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Customer_CustomerId",
                table: "Comment",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Worker_WorkerId",
                table: "Comment",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Customer_CustomerId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Worker_WorkerId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Project",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Comment",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Comment",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Customer_CustomerId",
                table: "Comment",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Worker_WorkerId",
                table: "Comment",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
