using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespositryLayer.Migrations
{
    public partial class fundoonoteee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDetailTables",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailTables", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "NoteDetailTables",
                columns: table => new
                {
                    NoteID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reminder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archieve = table.Column<bool>(type: "bit", nullable: false),
                    PinNotes = table.Column<bool>(type: "bit", nullable: false),
                    Trash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteDetailTables", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_NoteDetailTables_UserDetailTables_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetailTables",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollabDetailTables",
                columns: table => new
                {
                    CollabID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modifiedat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    NoteID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabDetailTables", x => x.CollabID);
                    table.ForeignKey(
                        name: "FK_CollabDetailTables_NoteDetailTables_NoteID",
                        column: x => x.NoteID,
                        principalTable: "NoteDetailTables",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollabDetailTables_UserDetailTables_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetailTables",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LabelDetailTables",
                columns: table => new
                {
                    LabelID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    NoteID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelDetailTables", x => x.LabelID);
                    table.ForeignKey(
                        name: "FK_LabelDetailTables_NoteDetailTables_NoteID",
                        column: x => x.NoteID,
                        principalTable: "NoteDetailTables",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabelDetailTables_UserDetailTables_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetailTables",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabDetailTables_NoteID",
                table: "CollabDetailTables",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_CollabDetailTables_UserID",
                table: "CollabDetailTables",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LabelDetailTables_NoteID",
                table: "LabelDetailTables",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_LabelDetailTables_UserID",
                table: "LabelDetailTables",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_NoteDetailTables_UserID",
                table: "NoteDetailTables",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabDetailTables");

            migrationBuilder.DropTable(
                name: "LabelDetailTables");

            migrationBuilder.DropTable(
                name: "NoteDetailTables");

            migrationBuilder.DropTable(
                name: "UserDetailTables");
        }
    }
}
