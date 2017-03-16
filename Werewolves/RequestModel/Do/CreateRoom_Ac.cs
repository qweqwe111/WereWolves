using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Werewolves.RequestModel
{
    public class CreateRoom_Ac_Request
    {
        public string roles { set; get; }

        public int Userid { set; get; }

        public int Pnum { set; get; }

        public Wwa.Enum.Victory Victory { set; get; }
    }
    public class CreateRoom_Ac_Response
    {
        /// <summary>
        /// 座位分配 key:座位号 value:角色
        /// </summary>
        public Dictionary<int,string> positions { set; get; }
        /// <summary>
        /// 人数
        /// </summary>
        public int Pnum { set; get; }
        /// <summary>
        /// 胜利条件
        /// </summary>
        public Wwa.Enum.Victory Victory { set; get; }
        /// <summary>
        /// 房间号
        /// </summary>
        public int RoomID { set; get; }
    }
}