using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wwa.Entities
{
    public class Rooms
    {
        
        [Key]
        public int ID { set; get; }

        public int RoomId { set; get; }

        public int CreateUserId { set; get; }

        public int PersonsNum { set; get; }

        public Wwa.Enum.Victory VictoryCondition { set; get; }
    
        public int LogId { set; get; }
     
        public bool IsQuit { set; get; }

        public DateTime Createtime { set; get; }
      
    }
}
