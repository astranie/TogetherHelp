using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class EmaileRepository : RepositoryBase<Email>
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

            Save(_email);
            return _email;
        }
        public Email HasExisted(string address)
        {

            return entities.Where(e => e.EmailAddress == address).SingleOrDefault();
        }

        private String MakeCode()
        {
            string code = new Random().Next().ToString();
            return code;
        }



        public Email GetByAddress(string address)
        {
            return entities.Where(e => e.EmailAddress == address).SingleOrDefault();
        }
    }
}
