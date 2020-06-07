using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.ValueObjects;

namespace Zup.AdministracaoClientes.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable(nameof(Cliente));

            builder.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.OwnsOne(e => e.CPF, 
                builderCPF =>
                                builderCPF.Property(c => c.Value)
                                    .HasColumnName(nameof(CPF))
                                    .HasMaxLength(11)
                                    .IsRequired());

            // Telefones
            builder.OwnsMany(c => c.Telefones,
                builderTelefone =>
                                builderTelefone.Property(p => p.Value)
                                    .HasColumnName("Numero")
                                    .IsRequired());

            // Endereços
            builder.OwnsMany(c => c.Enderecos, builderEndereco =>
            {
                builderEndereco.Property(e => e.Rua)
                    .HasMaxLength(250)
                    .IsRequired();

                builderEndereco.Property(e => e.Numero)
                    .IsRequired(false);

                builderEndereco.Property(e => e.Bairro)
                    .HasMaxLength(250)
                    .IsRequired();

                builderEndereco.Property(e => e.Cidade)
                    .HasMaxLength(250)
                    .IsRequired();

                builderEndereco.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsRequired();

                builderEndereco.Property(e => e.Pais)
                    .HasMaxLength(25)
                    .IsRequired();
                    

                builderEndereco.OwnsOne(e => e.CEP,
                    builderCEP =>
                                    builderCEP.Property(p => p.Value)
                                        .HasColumnName("CEP")
                                        .IsRequired());

            });

        }
    }
}
