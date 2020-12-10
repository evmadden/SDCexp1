using Microsoft.EntityFrameworkCore.Migrations;

namespace SDCode.Web.Migrations
{
    public partial class AddConsentAgreeLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AgreeLanguage",
                table: "Consents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreeLanguage",
                table: "Consents");
        }
    }
}
