using HamedStack.TheRepository;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using CaseStudy.Infrastructure.Identity;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}