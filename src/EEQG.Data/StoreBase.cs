using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EEQG.Data
{
    public class StoreBase<T, Tkey> where T : class
    {
        private DbSet<T> dbset;
        private DbContext db;
        public StoreBase()
        {

        }
        public StoreBase(DbContext _db)
        {
            db = _db;
            dbset = db.Set<T>();

        }
        public virtual T Find(Tkey id)
        {
            return dbset.Find(id);
        }
        public virtual void Add(T t)
        {
            dbset.Add(t);
            Save();
        }
        public virtual void AddRange(IEnumerable<T> t)
        {
            dbset.AddRange(t);
            Save();
        }
        public virtual DbSet<T> DbSet
        {
            get
            {
                return dbset;
            }

        }
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> pr = null)
        {
            if (pr == null)
            {
                return dbset;
            }
            return dbset.Where(pr);
        }
        public virtual int PageCount(int pCount = 50)
        {
            return (int)Math.Ceiling(dbset.Count() * 1.0 / pCount);
        }
        public virtual PageModel<T> Page(IQueryable<T> query, int index = 0, int pSize = 20)
        {
            PageModel<T> model = new PageModel<T>();
            model.ItemTotalCount = query.Count();
            model.PageIndex = index;
            model.PageSize = pSize;
            model.PageCount = (int)Math.Ceiling(model.ItemTotalCount * 1.0 / pSize);

            if (index == 0)
            {
                model.Items = query.Take(pSize).ToList();
            }
            else
            {
                model.Items = query.Skip(index * pSize).Take(pSize).ToList();
            }

            return model;
        }
        public virtual void Remove(T o)
        {
            dbset.Remove(o);
            Save();
        }
        public virtual void RemoveAll()
        {
            foreach (var item in dbset)
            {
                dbset.Remove(item);
            }
            Save();
        }
        public virtual void Update(T o)
        {
            db.Entry(o).State = EntityState.Modified;
            Save();
        }
        public virtual void Save()
        {
            db.SaveChanges();
        }
    }
}
