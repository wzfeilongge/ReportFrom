using FreeSql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.Common;
using WZ.Report.IRepository.Base;
using WZ.Report.Model;

namespace WZ.Report.Repository.Base
{
    public class BaseRepository<TEntity> : IRepository.Base.IBaseRepository<TEntity> where TEntity : class, new()
    {
        internal DbSet<TEntity> Domain { get; set; }

        public Reportcontext Context = Reportcontext.GetDbContext;

        public BaseRepository()
        {
            Domain = Context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity model)
        {
            await Domain.AddAsync(model);
            return await Context.SaveChangesAsync().ConfigureAwait(false) > 0 ? model : null;
        }

        public async Task<int> AddModel(TEntity model)
        {
            await Domain.AddAsync(model);       
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> AddRangeModels(List<TEntity> model)
        {
             await Domain.AddRangeAsync(model);
            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> DelBy(Expression<Func<TEntity, bool>> delWhere)
        {
            return await Domain.Where(delWhere).NoTracking().ToDelete().ExecuteAffrowsAsync();
        }

        public async Task<TEntity> GetModelAsync(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await Domain.Where(whereLambda).NoTracking().ToOneAsync();
        }

        public async Task<List<TEntity>> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderByLambda,bool isAsc = true)
        {
            if (isAsc)
            {
              return await Domain.Where(whereLambda).Count( out var total).NoTracking().Page(pageIndex, pageSize).OrderBy(orderByLambda).ToListAsync();
            }
            else
            {
               return await Domain.Where(whereLambda).Count(out var total).NoTracking().Page(pageIndex, pageSize).OrderByDescending(orderByLambda).ToListAsync();
            }
        }

        public async Task<int> Modify(TEntity model)
        {
            await Domain.UpdateAsync(model);

            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await Domain.Where(whereLambda).NoTracking().ToListAsync();
        }

        public async Task<long> GetCount(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await Domain.Where(whereLambda).NoTracking().CountAsync();            
        }

        public async Task<int> Modify(IEnumerable<TEntity> models)
        {
            await Domain.UpdateRangeAsync(models);

            return await Context.SaveChangesAsync().ConfigureAwait(false);
        }

    }
}
