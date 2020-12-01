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
    public static class DataLoaderFactory<T> where T : class, IEntity
    {
        //Func<TenantFileContext, DbSet<T>> dbSetFunc { get; init; }
        //public DataLoaderFactory(Func<TenantFileContext, DbSet<T>> func) 
        //{
        //    dbSetFunc = func;
        //}
        //public static DataLoaderById<T> CreateDataLoader(Func<TenantFileContext, DbSet<T>> func, IServiceProvider service )
        //{
        //    var ctx = service.GetRequiredService<IDbContextFactory<TenantFileContext>>();
        //    return new DataLoaderById<T>(default!, ctx, func);
        //}
    }
}
