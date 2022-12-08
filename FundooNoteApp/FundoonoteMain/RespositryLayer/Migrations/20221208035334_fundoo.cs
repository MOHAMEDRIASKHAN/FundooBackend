using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespositryLayer.Migrations
{
    public partial class fundoo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTables",
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
                    table.PrimaryKey("PK_UserTables", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "NoteDetailTable",
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
                    table.PrimaryKey("PK_NoteDetailTable", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_NoteDetailTable_UserTables_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTables",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CollabDetailTable",
                columns: table => new
                {
                    CollabID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modifiedat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NoteID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabDetailTable", x => x.CollabID);
                    table.ForeignKey(
                        name: "FK_CollabDetailTable_NoteDetailTable_NoteID",
                        column: x => x.NoteID,
                        principalTable: "NoteDetailTable",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabDetailTable_UserTables_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTables",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LabelTables",
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
                    table.PrimaryKey("PK_LabelTables", x => x.LabelID);
                    table.ForeignKey(
                        name: "FK_LabelTables_NoteDetailTable_NoteID",
                        column: x => x.NoteID,
                        principalTable: "NoteDetailTable",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LabelTables_UserTables_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTables",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabDetailTable_NoteID",
                table: "CollabDetailTable",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_CollabDetailTable_UserId",
                table: "CollabDetailTable",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTables_NoteID",
                table: "LabelTables",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTables_UserID",
                table: "LabelTables",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_NoteDetailTable_UserID",
                table: "NoteDetailTable",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabDetailTable");

            migrationBuilder.DropTable(
                name: "LabelTables");

            migrationBuilder.DropTable(
                name: "NoteDetailTable");

            migrationBuilder.DropTable(
                name: "UserTables");
        }
    }
}
