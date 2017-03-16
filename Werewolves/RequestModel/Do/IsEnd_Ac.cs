using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Werewolves.RequestModel
{
    public class IsEnd_Ac_Request
    {
        public bool Night { set; get; }
        public bool Guard { set; get; }
        public int WitchDrugs { set; get; }
        public bool WolfPower { set; get; }
        public bool WolfKing { set; get; }
        public bool BadDrug { set; get; }
        public bool Hunter { set; get; }

        public int Gods { set; get; }
        public int Persons { set; get; }
        public int Wolves { set; get; }
    }

    public class IsEnd_Ac_Response
    {
        public bool IsEnd { set; get; }
        public int WinRole { set; get; }
    }
}