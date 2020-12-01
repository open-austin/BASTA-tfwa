using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TenantFile.Api.Common;
using TenantFile.Api.Models;

namespace TenantFile.Api.DataLoader
{
    public interface IDataLoaderFactory<T> where T : class, IEntity
    {
        //Func<IDbContextFactory<TenantFileContext>, DbSet<T>> dbSetFunc { get; init; }
        public DataLoaderById<T> CreateDataLoader(IServiceProvider service);
        // Type dbSetType { get; init; }
    }

    public static class DataLoaderFactory<T> /*: IDataLoaderFactory<T>*/  where T : class, IEntity
    {
        //public DataLoaderFactory(Func<IDbContextFactory<TenantFileContext>, DbSet<T>> func)
        //{
        //    //dbSetFunc = func;
        //}
        //public IEntity[] dbSetType { get; init; } = null!;
        // public Func<IDbContextFactory<TenantFileContext>, DbSet<T>> dbSetFunc { get ; init; }

        public static DataLoaderById<T> CreateDataLoader(IServiceProvider service)
        {
            var ctx = service.GetRequiredService<IDbContextFactory<TenantFileContext>>();

            var dbSetInfo = ctx;
            //return new DataLoaderById<T>(default!, ctx,
            //    func => (DbSet<T>)func
            //    .GetType().InvokeMember(
            //    dbSetInfo.Name,
            //    System.Reflection.BindingFlags.GetProperty,
            //    binder: default,
            //    target: func,
            //    null)!);  
            return new DataLoaderById<T>(default!, ctx,
                func => GetDbSet(func));
        }
        public static DbSet<T> GetDbSet(TenantFileContext context)
        {
            //var t = dbSetType.GetType();


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

