using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedForwardBusinessEntities.Migrations
{
    public partial class FeedbackSchedulingDetailsNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackSchedulingDetails",
                columns: table => new
                {
                    FeedbackSchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feedbackToUserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    feedbackToName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    feedbackFromUserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    feedbackFromName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: true),
                    FeedbackSessionSID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackSchedulingDetails", x => x.FeedbackSchID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackSchedulingDetails");
        }
    }
}
