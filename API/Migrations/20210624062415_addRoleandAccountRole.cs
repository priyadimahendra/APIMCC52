using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addRoleandAccountRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_M_Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_M_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_T_AccountRole",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_T_AccountRole", x => new { x.NIK, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tb_T_AccountRole_tb_M_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_M_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_T_AccountRole_tb_T_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_T_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_T_AccountRole_RoleId",
                table: "tb_T_AccountRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_T_AccountRole");

            migrationBuilder.DropTable(
                name: "tb_M_Role");
        }
    }
}
