using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wwa.Entities;
namespace Wwa.App
{
    public class UsersApp
    {
        public int Exist(string openid)
        {
            using (MysqlDbContext mdb = new MysqlDbContext())
            {
                var user=mdb.Users.Where(t => t.Openid == openid).FirstOrDefault();
                if(user ==null)
                {
                    return 0;
                }
                return 1;
            }
      
        }
    }
}
