﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projeto_dotnet_sql.Models;

#nullable disable

namespace projeto_dotnet_sql.Migrations
{
    [DbContext(typeof(ConcessionariaContext))]
    partial class ConcessionariaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("projeto_dotnet_sql.Models.Acessorio", b =>
                {
                    b.Property<int>("AcessorioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcessorioId"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VeiculoNumeroChassi")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.HasKey("AcessorioId");

                    b.HasIndex("VeiculoNumeroChassi");

                    b.ToTable("Acessorios");
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Proprietario", b =>
                {
                    b.Property<string>("CpfCnpj")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CEP")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Cidade")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IndicadorPessoa")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UF")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("CpfCnpj");

                    b.ToTable("Proprietarios");
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Telefone", b =>
                {
                    b.Property<int>("TelefoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TelefoneId"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ProprietarioCpfCnpj")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("TelefoneId");

                    b.HasIndex("ProprietarioCpfCnpj");

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Veiculo", b =>
                {
                    b.Property<string>("NumeroChassi")
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("Cor")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProprietarioCpfCnpj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quilometragem")
                        .HasColumnType("float");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.Property<string>("VersaoSistema")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("NumeroChassi");

                    b.ToTable("Veiculos");
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Venda", b =>
                {
                    b.Property<int>("VendaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VendaId"), 1L, 1);

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime2");

                    b.Property<double>("ValorVenda")
                        .HasColumnType("float");

                    b.Property<string>("VeiculoNumeroChassi")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<int>("VendedorId")
                        .HasColumnType("int");

                    b.HasKey("VendaId");

                    b.HasIndex("VeiculoNumeroChassi");

                    b.HasIndex("VendedorId");

                    b.ToTable("Vendas");
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Vendedor", b =>
                {
                    b.Property<int>("VendedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VendedorId"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("SalarioBase")
                        .HasColumnType("float");

                    b.HasKey("VendedorId");

                    b.ToTable("Vendedores");
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Acessorio", b =>
                {
                    b.HasOne("projeto_dotnet_sql.Models.Veiculo", null)
                        .WithMany("Acessorios")
                        .HasForeignKey("VeiculoNumeroChassi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Telefone", b =>
                {
                    b.HasOne("projeto_dotnet_sql.Models.Proprietario", null)
                        .WithMany("Telefones")
                        .HasForeignKey("ProprietarioCpfCnpj")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Venda", b =>
                {
                    b.HasOne("projeto_dotnet_sql.Models.Veiculo", "Veiculo")
                        .WithMany()
                        .HasForeignKey("VeiculoNumeroChassi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projeto_dotnet_sql.Models.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Veiculo");

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Proprietario", b =>
                {
                    b.Navigation("Telefones");
                });

            modelBuilder.Entity("projeto_dotnet_sql.Models.Veiculo", b =>
                {
                    b.Navigation("Acessorios");
                });
#pragma warning restore 612, 618
        }
    }
}
