using HamedStack.TheRepository;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;
using CaseStudy.Infrastructure.Identity;
using HamedStack.TheAggregateRoot.Abstractions;
using HamedStack.TheRepository.EntityFrameworkCore.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CaseStudy.Infrastructure;

public class IdentityDbContextBase : IdentityDbContext<ApplicationUser>, IUnitOfWork
{
    private readonly ILogger<IdentityDbContextBase> _logger;
    private IDbContextTransaction? _dbContextTransaction;

    public IdentityDbContextBase(DbContextOptions options, ILogger<IdentityDbContextBase> logger)
        : base(options)
    {
        _logger = logger;
    }

    public async Task<IDbTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        _dbContextTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        return _dbContextTransaction.GetDbTransaction();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContextTransaction != null)
            await _dbContextTransaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_dbContextTransaction != null)
                await _dbContextTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.Dispose();
                _dbContextTransaction = null;
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new DomainEventOutboxInterceptor());
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        optionsBuilder.AddInterceptors(new AuditInterceptor());
        optionsBuilder.AddInterceptors(new PerformanceInterceptor(_logger));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetRowVersion(modelBuilder);
        SetSoftDeleteQueryFilter(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void SetRowVersion(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(t => typeof(IRowVersion).IsAssignableFrom(t.ClrType));

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType.ClrType)
                .Property("RowVersion")
                .IsRowVersion();
        }
    }

    private static void SetSoftDeleteQueryFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType)) continue;

            var entityClrType = entityType.ClrType;
            var parameter = Expression.Parameter(entityClrType, "e");
            var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
            var filterExpression =
                Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);
            modelBuilder.Entity(entityClrType).HasQueryFilter(filterExpression);
        }
    }
}