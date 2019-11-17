using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.Register
{
    public class EmailModel : LogViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string EmailCode { get; set; }

    }
}
