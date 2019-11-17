﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class SuggestRepository:RepositoryBase<Suggest>
    {
     

        public Suggest Publish(Suggest suggest,int authorid)
        {
            #region 一种粗暴解决Context冲突的方法
            //UserRepository userRepository = new UserRepository();
            //userRepository.SqlContext = new SuggestRepository().SqlContext;
            #endregion

            //suggest.Author= context.users.Where(u => u.Id == authorid).SingleOrDefault();
            //suggests.Add(suggest);
            //SaveChanges();
            suggest.Author = new UserRepository().GetById(authorid.ToString());
            Save(suggest);
            return suggest;
        }

     
        public Suggest GetByName(string title)
        {
            return entities.Where(s => s.Title == title).SingleOrDefault();
        }
    }
}
