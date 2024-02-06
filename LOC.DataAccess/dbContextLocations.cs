using LiteDB;
using LOC.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace LOC.DataAccess
{
    public class dbContextLocations
    {
        private readonly LiteDatabase _db;
        public dbContextLocations()
        {
            try
            {
                var db = new LiteDatabase(@"Locations.db");
                if (db != null)
                    _db = db;
            }
            catch (Exception ex)
            {
                throw new Exception("Can find or create LiteDb database.", ex);
            }
        }

        public IEnumerable<T> FindAll<T>() where T : EntityBase, new()
        {
            return _db.GetCollection<T>(typeof(T).Name)
                .Find(x => !x.IsDeleted);
        }

        public T FindOne<T>(Guid gid) where T : EntityBase, new()
        {
            return _db.GetCollection<T>(typeof(T).Name)
                .FindOne(x => x.GID == gid && !x.IsDeleted) ?? new T();
        }

        public IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> condition) where T : EntityBase, new()
        {
            return _db.GetCollection<T>(typeof(T).Name).Find(condition).Where(x => !x.IsDeleted);
        }

        public int Insert<T>(T forecast) where T : EntityBase, new()
        {
            return _db.GetCollection<T>(typeof(T).Name)
                .Insert(forecast);
        }

        public bool Update<T>(T forecast) where T : EntityBase, new()
        {
            return _db.GetCollection<T>(typeof(T).Name)
                .Update(forecast);
        }
    }
}
