using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class RepositoryBase<T> where T : Entity
    {
        private DbContext sqlContext;
        public RepositoryBase(DbContext context)
        {
            sqlContext = context;

            #region 显式打开事务

            //if (sqlContext.Database.CurrentTransaction != null)
            //{
            //    sqlContext.Database.BeginTransaction();
            //}

            #endregion




            entities = sqlContext.Set<T>();
            //切记  如果DbSet属性不在DBContext里设置，就需要把它显式的进行Set
        }
        public DbSet<T> entities { get; set; }

        public T Save(T entity)
        {
            entities.Add(entity);
            sqlContext.SaveChanges();
            return entity;
        }

        public void Delete(string id, T entity)
        {
            int Id = Convert.ToInt32(id);
            entity = GetById(Id).SingleOrDefault();
            if (entity != null)
            {
                entities.Remove(entity);
                sqlContext.SaveChanges();
            }
            else
            {
                //不存在需要提示
            }

        }

        public IQueryable<T> GetById(int id)
        {
            return entities.Where(entities => entities.Id == id);
        }

        public void Update()
        {
            sqlContext.SaveChanges();
        }

        //对IList对象进行分页操作，比如文章等
        public IQueryable<T> Paged(IQueryable<T> targets, int pageindex, int count)
        {
            return targets.Skip((pageindex - 1) * count).Take(count);
        }


    }
}
