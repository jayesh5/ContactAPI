using NLog;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
namespace Contacts.DataLayer.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected Logger Logger { get; private set; }

        private ContactsContext _entities;
        private readonly DbSet<T> _database;
        public Repository()
        {
            if (_entities == null)
                _entities = new ContactsContext();
            _database = _entities.Set<T>();
            Logger = LogManager.GetCurrentClassLogger();
        }
        public ContactsContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Insert(T entity)
        {
            try
            {
                _database.Add(entity);
                Save();
            }
            catch (DbEntityValidationException ex)
            {
                LogException(ex, entity, "Deleting");
            }
            catch (SqlException ex)
            {
                LogException(ex, entity, "Deleting");
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Exception when deleting entity {0}", entity.ToString());
            }
        }


        public virtual void Delete(T entity)
        {
            if (_entities.Entry(entity).State == EntityState.Detached)
            {
                _database.Attach(entity);
            }

            try
            {
                _database.Remove(entity);
                Save();
            }
            catch (DbEntityValidationException ex)
            {
                LogException(ex, entity, "Deleting");
            }
            catch (SqlException ex)
            {
                LogException(ex, entity, "Deleting");
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Exception when deleting entity {0}", entity.ToString());
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                _entities.Entry(entity).State = EntityState.Modified;
                Save();
            }
            catch (DbEntityValidationException ex)
            {
                LogException(ex, entity, "Deleting");
            }
            catch (SqlException ex)
            {
                LogException(ex, entity, "Deleting");
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Exception when deleting entity {0}", entity.ToString());
            }
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
        public virtual void Revert(T entity)
        {
            var state = _entities.Entry(entity).State;

            if (state != EntityState.Added)
            {
                _entities.Entry(entity).Reload();
            }
            _entities.Entry(entity).State = state;
        }
        protected void LogException(DbEntityValidationException ex, T entity, string operation)
        {
            foreach (var validationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Logger.Info("Property: {0}.{1} Reason: {2}", validationErrors.Entry.Entity, validationError.PropertyName, validationError.ErrorMessage);
                }
            }

            Logger.Warn(ex, "{0} entity {1} failed", operation, entity.ToString());
        }

        protected void LogException(SqlException ex, T entity, string operation)
        {
            foreach (SqlError error in ex.Errors)
            {
                if (!string.IsNullOrEmpty(error.Procedure))
                { // stored procedure error
                    Logger.Info("SQL SP Error {0} occurred in {1} on line {2}: {3}", error.Number, error.Procedure, error.LineNumber, error.Message);
                }
                else
                { // other error
                    Logger.Info("SQL Error {0} occurred: {1}", error.Number, error.Message);
                }
            }

            Logger.Warn(ex, "{0} entity {1} failed because of an SQL error: {2} (code {3}) on server {4}", operation, entity.ToString(), ex.Message, ex.Number, ex.Server);
        }

        public T Get(int id)
        {
            return _database.Find(id);
        }
    }
}