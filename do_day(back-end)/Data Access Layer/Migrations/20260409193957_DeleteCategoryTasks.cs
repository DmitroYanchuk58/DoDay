using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCategoryTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IdUser",
                table: "Categories",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_IdUser",
                table: "Categories",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_IdUser",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_IdUser",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTasks_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTasks_CategoryId",
                table: "CategoryTasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTasks_TaskId",
                table: "CategoryTasks",
                column: "TaskId");
        }
    }
}
