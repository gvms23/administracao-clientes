using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zup.AdministracaoClientes.Data.Context.Configuration;
using Zup.AdministracaoClientes.Data.Types;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.Entities.Base;

namespace Zup.AdministracaoClientes.Data.Context
{
    public class AdministracaoClientesContext : DbContext
    {
        public const string DATABASE_NAME = "AdministracaoClientes";

        private readonly ConnectionStringsType _connectionStrings;

        public static readonly LoggerFactory _debugLoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        public AdministracaoClientesContext(
                            DbContextOptions options,
                            IOptions<ConnectionStringsType> _optionsConnectionStrings)
                        : base(options)
        {
            _connectionStrings = _optionsConnectionStrings.Value;
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdministracaoClientesContext).Assembly);

            modelBuilder.ApplyGlobalStandards();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionStrings.AdministracaoClientesContext)
                              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }

            optionsBuilder.EnableSensitiveDataLogging();

            if (Debugger.IsAttached)
                optionsBuilder.UseLoggerFactory(_debugLoggerFactory);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            ChangeTracker.Entries().ToList().ForEach(entry =>
            {
                if (!(entry.Entity is EntityBase trackableEntity))
                    return;

                switch (entry.State)
                {
                    case EntityState.Added:
                        trackableEntity.CreatedDate = DateTime.Now;
                        trackableEntity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        trackableEntity.ModifiedDate = DateTime.Now;
                        break;
                }
            });
        }
    }
}
