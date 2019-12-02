using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.LogOn
{
    public class IntroductionModel : LogViewModel
    {
        public byte[] Header { get; set; }
        public int RegitseredTime { get; set; }

        public int Money { get; set; }

    }
}
