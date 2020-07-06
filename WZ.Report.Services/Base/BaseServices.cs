using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WZ.Report.IRepository.Base;
using WZ.Report.IServices.Base;

namespace WZ.Report.Services.Base
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {

        public IBaseRepository<TEntity> BaseDal;

        public  async Task<TEntity> Add(TEntity model)
        {
            return await BaseDal.Add(model);
        }

        public  async Task<int> AddModel(TEntity model)
        {
            return await BaseDal.AddModel(model);
        }

        public async Task<int> AddRangeModels(List<TEntity> model)
        {

            return await BaseDal.AddRangeModels(model);
        }

        public async Task<int> DelBy(Expression<Func<TEntity, bool>> delWhere)
        {

            return await BaseDal.DelBy(delWhere);
        }

        public  async Task<long> GetCount(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await BaseDal.GetCount(whereLambda);      
        }

        public async Task<TEntity> GetModelAsync(Expression<Func<TEntity, bool>> whereLambda)
        {

            return await BaseDal.GetModelAsync(whereLambda);
        }

        public async Task<List<TEntity>> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderByLambda,bool isAsc = true)
        {
            return await BaseDal.GetPagedList(pageIndex,pageSize,whereLambda, orderByLambda, isAsc);
        }

        public async Task<int> Modify(TEntity model)
        {

            return await BaseDal.Modify(model);
        }

        public async  Task<int> ModifyList(IEnumerable<TEntity> Models)
        {
            return await BaseDal.Modify(Models);
           // throw new NotImplementedException();
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await BaseDal.Query(whereLambda);

        }
    }
}
