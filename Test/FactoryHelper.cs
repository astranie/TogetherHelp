using BLL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFactory
{
    internal class FactoryHelper
    {
        public static string Password = "7858";

        public static SqlContext Context = new SqlContext();


    }
}
