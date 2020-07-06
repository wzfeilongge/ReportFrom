﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace WZ.Report.IServices.Base
{
   public interface IBaseServices<T> where T : class
    {
        Task<T> Add(T model);
        Task<int> AddRangeModels(List<T> model);
        Task<long> GetCount(Expression<Func<T, bool>> whereLambda);
        Task<List<T>> Query(Expression<Func<T, bool>> whereLambda);
        Task<T> GetModelAsync(Expression<Func<T, bool>> whereLambda);
        Task<List<T>> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderByLambda, bool isAsc = true);
        Task<int> DelBy(Expression<Func<T, bool>> delWhere);
        Task<int> AddModel(T model);
        Task<int> Modify(T model);
        Task<int> ModifyList(IEnumerable<T> Models);
    }
}
