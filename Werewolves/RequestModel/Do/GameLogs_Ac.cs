using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wwa.Entities;
namespace Werewolves.RequestModel
{
    public class GameLogs_Ac_Request
    {
        public int RoomId { set; get; }
    }
    public class GameLogs_Ac_Response
    {
        public List<WerewlovesLog> logs { set; get; }
    }
}