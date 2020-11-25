using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicalogData.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        #region "Sync Methods"
        
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> whereCondition);
        T GetSingleByID(params object[] keyValues);
        T GetSingle(Expression<Func<T, bool>> whereCondition);
        IQueryable<T> GetQueryable();

        bool Add(T item);
        bool Edit(T item);
        bool Delete(T item);
        bool DeleteByID(params object[] keyValues);
        int GetOperationStatus();

        #endregion
    }
}
