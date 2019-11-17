using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace UI.Models
{
    public class LogViewModel
    {
        public int CurrentUserId { get; set; }
        public string CurrentMD5Password { get; set; }

        public string CurrentUsername { get; set; }
    }
}
