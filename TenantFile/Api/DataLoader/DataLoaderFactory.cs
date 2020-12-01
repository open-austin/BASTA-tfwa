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
    public interface IDataLoaderFactory//<T> where T : class, IEntity
    {
        //Func<IDbContextFactory<TenantFileContext>, DbSet<T>> dbSetFunc { get; init; }
        public DataLoaderById<Type> CreateDataLoader(IServiceProvider service);
        Type dbSetType { get; init; }
    }

    public class DataLoaderFactory//<T> : IDataLoaderFactory<T> where T : class, IEntity
    {
        //public DataLoaderFactory(Func<IDbContextFactory<TenantFileContext>, DbSet<T>> func)
        //{
        //    //dbSetFunc = func;
        //}
        public IEntity dbSetType { get; init; } = null!;
        // public Func<IDbContextFactory<TenantFileContext>, DbSet<T>> dbSetFunc { get ; init; }
        public DataLoaderFactory(IEntity entity)
        {
            dbSetType = entity;
        }
        public DataLoaderById<IEntity> CreateDataLoader(IServiceProvider service)
        {
            var ctx = service.GetRequiredService<IDbContextFactory<TenantFileContext>>();
            var dbSetInfo = ctx.GetType().GetProperties()
              .Where(c => c.PropertyType == typeof(DbSet<IEntity>)).FirstOrDefault()!;
            return new DataLoaderById<IEntity>(default!, ctx,
                func => (DbSet<IEntity>)func.GetType().InvokeMember(
                dbSetInfo.Name,
                System.Reflection.BindingFlags.GetProperty,
                binder: default,
                target: ctx,
                null)!);
        }
        public  DbSet<IEntity> GetDbSet(TenantFileContext context)
        { 
            var t = dbSetType.GetType();


               var dbSetInfo = context.GetType().GetProperties().Where(c => c.PropertyType == typeof(DbSet<>)).FirstOrDefault()!;

            return (DbSet<IEntity>)context.GetType().InvokeMember(
                dbSetInfo.Name,
                System.Reflection.BindingFlags.GetProperty,
                binder: default,
                target: context,
                null)!;
        }
    }


    