using System;

namespace BLL
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string  PassWord { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;

        public User IsInvited { get; set; }

        public Email Email { get; set; }
    }
}
