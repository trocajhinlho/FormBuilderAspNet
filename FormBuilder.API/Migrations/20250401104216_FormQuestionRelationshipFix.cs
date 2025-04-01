using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.API.Migrations
{
    /// <inheritdoc />
    public partial class FormQuestionRelationshipFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Form_Id",
                table: "Question");

            migrationBuilder.CreateIndex(
                name: "IX_Question_FormId",
                table: "Question",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Form_FormId",
                table: "Question",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Form_FormId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_FormId",
                table: "Question");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Form_Id",
                table: "Question",
                column: "Id",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
