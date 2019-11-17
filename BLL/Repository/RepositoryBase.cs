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
            entities = sqlContext.Set<T>();//切记  如果DbSet属性不在DBContext里设置，就需要把它显式的进行Set
        }
        public DbSet<T> entities { get; set; }

        public T Save(T entity)
        {
            entities.Add(entity);
            sqlContext.SaveChanges();
            return entity;
        }

        public T GetById(int id)
        {
            return entities.Where(entities => entities.Id == id).SingleOrDefault();
        }

        public void Update()
        {
            sqlContext.SaveChanges();
        }

    }
}
