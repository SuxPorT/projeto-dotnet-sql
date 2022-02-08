using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_dotnet_sql.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proprietarios",
                columns: table => new
                {
                    CpfCnpj = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IndicadorPessoa = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UF = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietarios", x => x.CpfCnpj);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    NumeroChassi = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Quilometragem = table.Column<double>(type: "float", nullable: false),
                    VersaoSistema = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ProprietarioCpfCnpj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.NumeroChassi);
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    VendedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SalarioBase = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.VendedorId);
                });

            migrationBuilder.CreateTable(
                name: "Telefones",
                columns: table => new
                {
                    TelefoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProprietarioCpfCnpj = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefones", x => x.TelefoneId);
                    table.ForeignKey(
                        name: "FK_Telefones_Proprietarios_ProprietarioCpfCnpj",
                        column: x => x.ProprietarioCpfCnpj,
                        principalTable: "Proprietarios",
                        principalColumn: "CpfCnpj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acessorios",
                columns: table => new
                {
                    AcessorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeiculoNumeroChassi = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acessorios", x => x.AcessorioId);
                    table.ForeignKey(
                        name: "FK_Acessorios_Veiculos_VeiculoNumeroChassi",
                        column: x => x.VeiculoNumeroChassi,
                        principalTable: "Veiculos",
                        principalColumn: "NumeroChassi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    VendaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorVenda = table.Column<double>(type: "float", nullable: false),
                    VeiculoNumeroChassi = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.VendaId);
                    table.ForeignKey(
                        name: "FK_Vendas_Veiculos_VeiculoNumeroChassi",
                        column: x => x.VeiculoNumeroChassi,
                        principalTable: "Veiculos",
                        principalColumn: "NumeroChassi",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendas_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "VendedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acessorios_VeiculoNumeroChassi",
                table: "Acessorios",
                column: "VeiculoNumeroChassi");

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_ProprietarioCpfCnpj",
                table: "Telefones",
                column: "ProprietarioCpfCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VeiculoNumeroChassi",
                table: "Vendas",
                column: "VeiculoNumeroChassi");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VendedorId",
                table: "Vendas",
                column: "VendedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acessorios");

            migrationBuilder.DropTable(
                name: "Telefones");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "Proprietarios");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Vendedores");
        }
    }
}
