using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Infrastructure.EF.Contexts
{
    public abstract class AuditableDbContext : DbContext
    {
        protected AuditableDbContext(DbContextOptions options) : base(options)
        {
        }

        private void ValidateIds(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty", nameof(userId));
        }

        private void ApplyAuditData(Guid userId)
        {

            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.FillDataForInsert(userId);
                        break;
                    case EntityState.Modified:
                        entry.Entity.FillDataForUpdate(userId);
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<IntermediaryEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.FillDataForInsert(userId);
                        break;
                }
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess = true)
        {
            throw new NotSupportedException("Use SaveChanges(Guid userId, Guid departmentId) instead");
        }

        public virtual int SaveChanges(Guid userId)
        {
            ValidateIds(userId);
            ApplyAuditData(userId);
            return base.SaveChanges(true);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException("Use SaveChangesAsync(Guid userId, Guid departmentId) instead");
        }

        public virtual async Task<int> SaveChangesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            ValidateIds(userId);
            ApplyAuditData(userId);
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
