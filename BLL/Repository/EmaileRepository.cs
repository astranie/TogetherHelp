using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class EmaileRepository : SqlContext
    {

        private Email _email;
        public EmaileRepository()
        {
            _email = new Email();
        }


        public Email RegisterEmail(string address)
        {
            _email.EmailAddress = address;
            _email.ValidateCode = MakeCode();

            emailes.Add(_email);
            SaveChanges();
            return _email;
        }
        public Email HasExisted(string address)
        {

            return emailes.Where(e => e.EmailAddress == address).SingleOrDefault();
        }

        private String MakeCode()
        {
            string code = new Random().Next().ToString();
            return code;
        }

        public Email GetById(int id)
        {
            return emailes.Where(e => e.Id == id).SingleOrDefault();
        }

        public Email GetByAddress(string address)
        {
            return emailes.Where(e => e.EmailAddress == address).SingleOrDefault();
        }

        public Email Update()
        {
            SaveChanges();
            return _email;
        }

    }
}
