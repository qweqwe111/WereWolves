using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Security
{
    public class KeyCollection
    {
        #region Cookie
        public static string WebUserLoginError(string username)
        {
            username = "";
            return string.Format("Cookie-WebUser-Login-{0}", username);
        }
        #endregion
        #region Cache

        #endregion
        #region Session

        #endregion
    }
}
