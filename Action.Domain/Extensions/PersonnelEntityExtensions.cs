using Microsoft.EntityFrameworkCore;

namespace Action.Domain.Extensions
{
    public static class PersonnelEntityExtensions
    {
        public static void AddPersonnelContext(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Personnel>().ToTable(nameof(Personnel).ToUpper()).HasKey(x => x.Id);
            modelBuilder.Entity<Personnel>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Personnel>().OwnsMany(x => x.Licenses,
                a =>
                {
                    a.ToTable(nameof(License).ToUpper())
                    .WithOwner()
                    .HasForeignKey(nameof(License.PersonnelId));
                    a.HasKey(nameof(License.Id));
                });
        }
    }
}
