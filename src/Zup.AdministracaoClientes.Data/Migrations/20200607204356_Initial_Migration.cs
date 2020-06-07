using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zup.AdministracaoClientes.Data.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    CPF = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Numero = table.Column<short>(nullable: true),
                    Bairro = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Pais = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    CEP = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => new { x.ClienteId, x.Id });
                    table.ForeignKey(
                        name: "FK_Endereco_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => new { x.ClienteId, x.Id });
                    table.ForeignKey(
                        name: "FK_Telefone_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
