using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UpdateNametbEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_{tb_M_Education}_tb_M_University_UniversityId",
                table: "{tb_M_Education}");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_T_Profiling_{tb_M_Education}_EducationId",
                table: "tb_T_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_{tb_M_Education}",
                table: "{tb_M_Education}");

            migrationBuilder.RenameTable(
                name: "{tb_M_Education}",
                newName: "tb_M_Education");

            migrationBuilder.RenameIndex(
                name: "IX_{tb_M_Education}_UniversityId",
                table: "tb_M_Education",
                newName: "IX_tb_M_Education_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_M_Education",
                table: "tb_M_Education",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_M_Education_tb_M_University_UniversityId",
                table: "tb_M_Education",
                column: "UniversityId",
                principalTable: "tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_T_Profiling_tb_M_Education_EducationId",
                table: "tb_T_Profiling",
                column: "EducationId",
                principalTable: "tb_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_M_Education_tb_M_University_UniversityId",
                table: "tb_M_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_T_Profiling_tb_M_Education_EducationId",
                table: "tb_T_Profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_M_Education",
                table: "tb_M_Education");

            migrationBuilder.RenameTable(
                name: "tb_M_Education",
                newName: "{tb_M_Education}");

            migrationBuilder.RenameIndex(
                name: "IX_tb_M_Education_UniversityId",
                table: "{tb_M_Education}",
                newName: "IX_{tb_M_Education}_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_{tb_M_Education}",
                table: "{tb_M_Education}",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_{tb_M_Education}_tb_M_University_UniversityId",
                table: "{tb_M_Education}",
                column: "UniversityId",
                principalTable: "tb_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_T_Profiling_{tb_M_Education}_EducationId",
                table: "tb_T_Profiling",
                column: "EducationId",
                principalTable: "{tb_M_Education}",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
