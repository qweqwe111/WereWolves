using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Wwa.App
{
    public  class DapperHelper
    {

        #region 构造函数

        private static DapperHelper _instance;
        public static DapperHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DapperHelper();
            }
            return _instance;
        }

        #endregion

        #region Context
        public MySqlConnection Context()
        {
            string con = ConfigurationManager.ConnectionStrings["mysqlconnection"].ToString();
            return new MySqlConnection(con);
        }
        #endregion


        public int Add(string sql)
        {
            try
            { 
            using (MySqlConnection con = DapperHelper.GetInstance().Context())
            {
                return con.Execute(sql);
            }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
