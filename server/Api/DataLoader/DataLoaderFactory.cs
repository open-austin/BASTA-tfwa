using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TenantFile.Api.Common;
using TenantFile.Api.Models;

namespace TenantFile.Api.DataLoader
{
    public interface IDataLoaderFactory
    {
        public DataLoaderById<T> CreateDataLoader<T>(IServiceProvider service) where T : class, IEntity;
    }

    public class DataLoaderFactory : IDataLoaderFactory
    {
        public DataLoaderById<T> CreateDataLoader<T>(IServiceProvider service) where T : class, IEntity
        {
            var ctx = service.GetRequiredService<IDbContextFactory<TenantFileContext>>();

            var dbSetInfo = ctx;
           
            return new DataLoaderById<T>(service.GetRequiredService<IBatchScheduler>(),
                                         ctx,
                                         func => GetDbSet<T>(func));
        }
        public DbSet<T> GetDbSet<T>(TenantFileContext context) where T : class, IEntity
        {
            {
                var dbSetInfo = context.GetType().GetProperties().Where(c => c.PropertyType == typeof(DbSet<T>)).FirstOrDefault()!;

                return (DbSet<T>)context.GetType().InvokeMember(
                    dbSetInfo.Name,
                    System.Reflection.BindingFlags.GetProperty,
                    binder: default,
                    target: context,
                    null)!;
            }
        }
    }
}

