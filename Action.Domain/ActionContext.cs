using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Action.Domain
{
    public class ActionContext : DbContext
    {

        public DbSet<Personnel> Personnels { get; set; }


        public ActionContext(DbContextOptions<ActionContext> contextOptions): base(contextOptions)
        {
        }

        private const string DeletedFlag = "IsDeleted";
        private const string LastModifiedFlag = "LastModifiedTime";
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Personnel>().ToTable("PERSONNEL").HasKey(x => x.UserId);
            modelBuilder.Entity<Personnel>().HasQueryFilter(x => !x.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[DeletedFlag] = false;
                        entry.CurrentValues[LastModifiedFlag] = DateTimeOffset.Now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[DeletedFlag] = true;
                        entry.CurrentValues[LastModifiedFlag] = DateTimeOffset.Now;
                        PropagateDelete(entry);
                        break;

                    case EntityState.Modified:

                        if (entry.CurrentValues[DeletedFlag].Equals(true))
                        {
                            PropagateDelete(entry);
                        }
                        break;
                }
            }
        }

        private void PropagateDelete(EntityEntry entry)
        {
            foreach (NavigationEntry navigationEntry in entry.Navigations.Where(n => !n.Metadata.IsDependentToPrincipal()))
            {
                if (navigationEntry is CollectionEntry collectionEntry)
                {
                    foreach (object dependentEntry in collectionEntry.CurrentValue)
                    {
                        HandleDependent(Entry(dependentEntry));
                    }
                }
                else
                {
                    object dependentEntry = navigationEntry.CurrentValue;
                    if (dependentEntry != null)
                    {
                        HandleDependent(Entry(dependentEntry));
                    }
                }
            }
        }

        private void HandleDependent(EntityEntry entry)
        {
            entry.CurrentValues[DeletedFlag] = true;
            entry.CurrentValues[LastModifiedFlag] = DateTimeOffset.Now;
        }

    }
}
