using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wwa.Enum;

namespace Library.Randoms
{
    public class Distribute
    {
        //狼人=1,
        //平民=2,
        //预言家=3,
        //女巫=4,
        //猎人=5,
        //白痴=6,
        //守卫=7,
        //白狼王=8
        public static Dictionary<int, string> DistributeRole(string[] roles,int Pnum,int RoomId)
        {
           
            var rolearr = roles.OrderBy(t=>Guid.NewGuid()).ToList();
            var dic = new Dictionary<int, string>();
            for(int i=1;i<=rolearr.Count();i++)
            {
                var vl = Convert.ToInt32(rolearr[i - 1]);
                var name = Enum.GetName(typeof(wolverole),vl);
                dic.Add(i, name);
            }
            return dic;
            //foreach(var role in rolearr)
            //{
            //    var name = Enum.GetName(typeof(wolverole), role);
            //    dic.Add()
            //}
        }
    }

    public class DistributeRoleMod
    {
        public Dictionary<int, string> Dic { set; get; }

        public string Str { set; get; }
    }
}
