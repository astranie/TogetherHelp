using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class SuggestRepository : SqlContext
    {


        public Suggest Publish(Suggest suggest,int authorid)
        {
            suggest.Author= users.Where(u => u.Id == authorid).SingleOrDefault();
            suggests.Add(suggest);
            SaveChanges();
            return suggest;
        }

        public Suggest GetById(int id)
        {
            return suggests.Where(s => s.Id == id).SingleOrDefault();
        }
        public Suggest GetByName(string title)
        {
            return suggests.Where(s => s.Title == title).SingleOrDefault();
        }
    }
}
