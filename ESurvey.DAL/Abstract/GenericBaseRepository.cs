using ESurvey.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq.Expressions;


namespace ESurvey.DAL.Abstract
{
    public class GenericBaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Fileds
        private ESurveyEntities _dbContext;
        #endregion

        #region Construct
        public GenericBaseRepository(ESurveyEntities context)
        {
            _dbContext = context;

        }
        #endregion

        #region Properties
        public ESurveyEntities Context
        {
            get { return _dbContext; }
            set { _dbContext = value; }
        }
        #endregion

        #region CRUD

        public void Insert(T item)
        {

            _dbContext.Set<T>().Add(item);

        }

        public virtual void Update(T item)
        {
            _dbContext.Entry(item).State =  EntityState.Modified;
        }

        

        public void Remove(T item)
        {
            _dbContext.Set<T>().Remove(item);
        }

        public void RemoveBy(Expression<Func<T, bool>> predicate)
        {
            var set = _dbContext.Set<T>().Where(predicate);
            _dbContext.Set<T>().RemoveRange(set);
        }

        
        public void RemoveRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().RemoveRange(items);
        }

        #endregion

        #region Selection Methods

        public IEnumerable<T> Fetch()
        {
            return _dbContext.Set<T>().ToList();
        }

        public IEnumerable<T> FetchBy(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).ToList();
        }

        public IEnumerable<T> PaggingFetch(int startIndex, int count)
        {
            return _dbContext.Set<T>().Skip(startIndex).Take(count).ToList();
        }

        public IEnumerable<T> PaggingFetchBy(Expression<Func<T, bool>> predicate, int startIndex, int count)
        {
            return _dbContext.Set<T>().Where(predicate).Skip(startIndex).Take(count).ToList();
        }


        


        public async Task<List<T>> FetchAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> FetchByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> PaggingFetchAsync(int startIndex, int count)
        {
            return await _dbContext.Set<T>().Skip(startIndex).Take(count).ToListAsync();
        }

        public async Task<List<T>> PaggingFetchByAsync(Expression<Func<T, bool>> predicate, int startIndex, int count)
        {
            return await _dbContext.Set<T>().Where(predicate).Skip(startIndex).Take(count).ToListAsync();
        }
        #endregion

        #region Get

        public int GetCount()
        {
            return _dbContext.Set<T>().Count();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }
        public T GetById(int id)
        {
            return id > 0 ? _dbContext.Set<T>().Find(id) : null;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return id > 0 ? await _dbContext.Set<T>().FindAsync(id) : null;
        }

        #endregion

        #region Implements of IDisposable

        //public void Dispose()
        //{
        //    if (_dbContext != null)
        //        _dbContext.Dispose();
        //}

        #endregion

        #region SaveRecords

        public void SaveContext()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveContextAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
