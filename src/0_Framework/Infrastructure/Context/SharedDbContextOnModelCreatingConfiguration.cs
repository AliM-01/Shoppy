using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastructure.Context
{
    public static class SharedDbContextOnModelCreatingConfiguration
    {
        public static void ApplySharedDbContextOnModelCreatingConfiguration(this ModelBuilder builder)
        {
            #region Restrict DeleteBehavior

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            #endregion

            #region Query Filters

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var isDeleteProperty = entityType.FindProperty("IsDelete");
                if (isDeleteProperty != null && isDeleteProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "p");
                    var filter = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, isDeleteProperty.PropertyInfo),
                            Expression.Constant(false, typeof(bool))
                        )
                        , parameter);
                    entityType.SetQueryFilter(filter);
                }
            }

            #endregion

            #region Decimals have 18,6 Range

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }

            #endregion
        }
    }
}