using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Wwa.Entities;
using Library.Randoms;
using Library;
using System.Web.Http;
using System.Web.Http.Results;
using Werewolves.RequestModel;
using Wwa.DTO;
using System.Text;
namespace Werewolves.Controllers
{
    public class ValuesController : ApiController
    {
        /// <summary>
        /// 三级分类加载
        /// </summary>
        /// <returns></returns>
        public string mysql()
        {
            var ajaxlist = new List<mm>();
            string con = ConfigurationManager.ConnectionStrings["mysqlconnection1"].ToString();
            using (MySqlConnection c = new MySqlConnection(con))
            {
                string sql = "select Id,Name,Path from lt_mall_categories";
                c.Open();
                using (MySqlCommand com = new MySqlCommand(sql, c))
                {
                    using (var dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var m = new mm()
                                {
                                    id = Convert.ToInt32(dr[0]),
                                    name = dr[1].ToString(),
                                    path = dr[2].ToString()
                                };
                                ajaxlist.Add(m);
                            }
                        }
                    }
                }
            }
            var ViewJson = new List<CategoryJsonModel>();
            foreach (var cate in ajaxlist)
            {
                var patharray = cate.path.Split('|');
                var first = patharray.Length > 0 ? Convert.ToInt32(patharray[0]) : 0;
                var two = patharray.Length > 1 ? Convert.ToInt32(patharray[1]) : 0;
                var three = patharray.Length > 2 ? Convert.ToInt32(patharray[2]) : 0;
                var cateone = ajaxlist.Where(t => t.id == first).FirstOrDefault();
                var categoryfirst = new CategoryJsonModel()
                {
                    Id = cateone.id,
                    Name = cateone.name,
                    SubCategory = new List<SecondLevelCategory>()
                };
                if (two == 0)
                {
                    ViewJson.Add(categoryfirst);
                    continue;
                }
                var catetwo = ajaxlist.Where(t => t.id == two).FirstOrDefault();
                var categorytwo = new SecondLevelCategory()
                {
                    Id = catetwo.id,
                    Name = catetwo.name,
                    SubCategory = new List<ThirdLevelCategoty>()
                };
                if (three == 0)
                {
                    var onefloor = ViewJson.FirstOrDefault(t => t.Id == cateone.id);
                    onefloor.SubCategory.Add(categorytwo);
                    continue;
                }
                var catethree = ajaxlist.Where(t => t.id == three).FirstOrDefault();
                var categorythree = new ThirdLevelCategoty()
                {
                    Id = catethree.id,
                    Name = catethree.name
                };
                categorytwo.SubCategory.Add(categorythree);
                var onefloors = ViewJson.FirstOrDefault(t => t.Id == cateone.id);
                var twofloors = onefloors.SubCategory.FirstOrDefault(t => t.Id == categorytwo.Id);
                twofloors.SubCategory.Add(categorythree);
            }
            return JsonConvert.SerializeObject(ViewJson);
        }


        public mm getcate(int id)
        {
            mm mod = null;
            string con = ConfigurationManager.ConnectionStrings["mysqlconnection1"].ToString();
            using (MySqlConnection c = new MySqlConnection(con))
            {
                string sql = "select Id,Name,Path from lt_mall_categories where Id=" + id + "";
                c.Open();
                using (MySqlCommand com = new MySqlCommand(sql, c))
                {
                    using (var dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                mod = new mm()
                                {
                                    id = Convert.ToInt32(dr[0]),
                                    name = dr[1].ToString(),
                                    path = dr[2].ToString()
                                };
                            }
                        }
                    }
                }
            }
            return mod;
        }
    }
    public class CategoryJsonModel
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public List<SecondLevelCategory> SubCategory
        {
            get;
            set;
        }

        public CategoryJsonModel()
        {
        }
    }
    public class SecondLevelCategory
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public List<ThirdLevelCategoty> SubCategory
        {
            get;
            set;
        }

        public SecondLevelCategory()
        {
        }
    }

    public class ThirdLevelCategoty
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ThirdLevelCategoty()
        {
        }
    }
    public class mm
    {
        public int id { set; get; }
        public string name { set; get; }
        public string path { set; get; }

    }
}
