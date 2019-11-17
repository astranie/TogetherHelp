using System;

namespace BLL
{
    public class Suggest : Entity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public User Author { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}