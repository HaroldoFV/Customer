using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer.Infra.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class AjustaKeyCustomerAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Address",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Address",
                newName: "Id");
        }
    }
}
