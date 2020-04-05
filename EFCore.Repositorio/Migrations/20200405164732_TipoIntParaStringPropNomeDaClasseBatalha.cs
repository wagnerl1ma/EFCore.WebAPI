using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Repositorio.Migrations
{
    public partial class TipoIntParaStringPropNomeDaClasseBatalha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Batalhas",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Nome",
                table: "Batalhas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
