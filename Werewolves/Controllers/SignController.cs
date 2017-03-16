using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wwa.App;
using Wwa.Entities;

namespace Werewolves.Controllers
{
    public class SignController : ApiController
    {
        public bool sign(string openid)
        {
            UsersApp userapp = new UsersApp();
            if (userapp.Exist(openid)>0)
            {

                return true;
            }
            else
            {

                return true;
            }

        }
         
    }
}