using MusicalogData.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicalogData.Repository
{
    /// <summary>
    /// Provides generic repository functionality for use with the Placement Allocation database context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class MusicalogRepository<T> : IRepository<T> where T : class
    {
        #region "Properties"

        private MusicalogDBContext _dbContext;
        private int _OperationStatus;

        #endregion

        public MusicalogRepository(MusicalogDBContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("MusicalogDBContext", "Database context object was not provided.");
            }

            _dbContext = dbContext;
        }

        #region "Synchronous Generic Repository Methods"

        /// <summary>
        /// Last operation status:
        /// 1 - Item to be deleted did not exist in context.
        /// </summary>
        /// <returns></returns>
        public int GetOperationStatus()
        {
            return _OperationStatus;
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList<T>();
        }

        public List<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return _dbContext.Set<T>().Where(whereCondition).ToList<T>();
        }

        /// <summary>
        /// Return a single item whose key field(s) match the given key values.
        /// </summary>
        /// <param name="keyValues">keyValues must be in the exact order as they appear in entity.</param>
        /// <returns></returns>
        public T GetSingleByID(params object[] keyValues)
        {
            if (keyValues != null && keyValues.Count() > 0)
            {
                return _dbContext.Set<T>().Find(keyValues);
            }

            return null;
        }

        public T GetSingle(Expression<Func<T, bool>> whereCondition)
        {
            return _dbContext.Set<T>().FirstOrDefault<T>(whereCondition);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>().AsQueryable<T>();
        }

        public bool Add(T item)
        {
            bool result = false;

            if (item != null)
            {
                _dbContext.Set<T>().Add(item);
                int dbResult = _dbContext.SaveChanges();

                if (dbResult > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool Edit(T item)
        {
            bool result = false;

            if (item != null)
            {
                _dbContext.Set<T>().Attach(item);
                _dbContext.Entry(item).State = EntityState.Modified;
                int dbResult = _dbContext.SaveChanges();

                if (dbResult > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool Delete(T item)
        {
            bool result = false;

            if (item != null)
            {
                _dbContext.Set<T>().Remove(item);
                int dbResult = _dbContext.SaveChanges();

                if (dbResult > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool DeleteByID(params object[] keyValues)
        {
            bool result = false;

            if (keyValues != null && keyValues.Count() > 0)
            {
                T item = _dbContext.Set<T>().Find(keyValues);
                if (item != null)
                {
                    _dbContext.Set<T>().Remove(item);

                    int dbResult = _dbContext.SaveChanges();
                    if (dbResult > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    // Indicate that item to delete did not exist.
                    _OperationStatus = 1;
                }
            }

            return result;
        }

        #endregion

        #region "Dispose Methods"

        /// <summary>
        /// Dispose the dbContext.
        /// </summary>
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }

        #endregion
    }
}
