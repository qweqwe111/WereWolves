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
    /// <summary>
    /// 狼人杀Api
    /// </summary>
    public class DoController : ApiController
    {
        /// <summary>
        /// 获取角色的key和Value
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetRoles()
        {
            List<RolesKV> kv = new List<RolesKV>();
            foreach (var role in Enum.GetValues(typeof(Wwa.Enum.wolverole)))
            {
                RolesKV rkv = new RolesKV()
                {
                    Values = role.ToString(),
                    Key = (int)role
                };
                kv.Add(rkv);
            }
            return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = JsonConvert.SerializeObject(kv) });
        }

        /// <summary>
        /// 再来一局
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult GameAgain([FromBody]ChangeSetting_Request RequestMod)
        {
            try
            {
                if (RequestMod == null) throw new ArgumentException("非法访问");
                if (RequestMod.RoomId < 1) throw new ArgumentException("非法访问");
                var roles = RequestMod.roles.Split(',');
                foreach (var role in roles)
                {
                    var Irole = Convert.ToInt32(role);
                    var rolename = Enum.GetName(typeof(Wwa.Enum.wolverole), Irole);
                    if (string.IsNullOrEmpty(rolename))
                    {
                        throw new ArgumentException("无此角色");
                    }
                }
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    var room = db.Rooms.FirstOrDefault(t => t.RoomId == RequestMod.RoomId && t.IsQuit == false);
                    if (room == null) throw new ArgumentException("无此房间");
                    room.PersonsNum = RequestMod.Pnum;
                    var Vic = Enum.GetName(typeof(Wwa.Enum.Victory), RequestMod.Victory);
                    if (string.IsNullOrEmpty(Vic))
                    {
                        room.VictoryCondition = Wwa.Enum.Victory.屠边;
                    }
                    else
                    {
                        room.VictoryCondition = (Wwa.Enum.Victory)Enum.Parse(typeof(Wwa.Enum.Victory), Vic);
                    }
                    var rolearr = roles.OrderBy(t => Guid.NewGuid()).ToList();
                    var dic = new Dictionary<int, string>();
                    for (int iss = 1; iss <= rolearr.Count(); iss++)
                    {
                        var vl = Convert.ToInt32(rolearr[iss - 1]);
                        var name = Enum.Parse(typeof(Wwa.Enum.wolverole), vl.ToString());
                        RoomRole rr = new RoomRole()
                        {
                            LocationId = iss,
                            RoleEnum = (Wwa.Enum.wolverole)name,
                            Rid = RequestMod.RoomId
                        };
                        dic.Add(iss, name.ToString());
                        db.RoomRoles.Add(rr);
                    }
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateRoom_Ac_Response returndata = new CreateRoom_Ac_Response()
                        {
                            RoomID = RequestMod.RoomId,
                            Pnum = RequestMod.Pnum,
                            Victory = room.VictoryCondition
                        };
                        returndata.positions = dic;
                        return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = returndata });
                    }
                    else
                    {
                        return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "房间更新失败" });
                    }
                }

            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + "ChangeSetting房间更新失败(数据库问题):" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "房间更新失败,请联系管理员" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }
        /// <summary>
        /// 游戏结束一局后修改房间配置
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult ChangeSetting([FromBody]ChangeSetting_Request RequestMod)
        {
            try
            {
                if (RequestMod == null) throw new ArgumentException("非法访问");
                if (RequestMod.Userid < 1) throw new ArgumentException("无此用户");
                if (RequestMod.roles.Split(',').Length != RequestMod.Pnum) throw new ArgumentException("角色与人数不对等");
                var roles = RequestMod.roles.Split(',');
                foreach (var role in roles)
                {
                    var Irole = Convert.ToInt32(role);
                    var rolename = Enum.GetName(typeof(Wwa.Enum.wolverole), Irole);
                    if (string.IsNullOrEmpty(rolename))
                    {
                        throw new ArgumentException("无此角色");
                    }
                }
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    var room = db.Rooms.FirstOrDefault(t => t.RoomId == RequestMod.RoomId && t.IsQuit == false);
                    if (room == null) throw new ArgumentException("无此房间");
                    room.PersonsNum = RequestMod.Pnum;
                    var Vic = Enum.GetName(typeof(Wwa.Enum.Victory), RequestMod.Victory);
                    if (string.IsNullOrEmpty(Vic))
                    {
                        room.VictoryCondition = Wwa.Enum.Victory.屠边;
                    }
                    else
                    {
                        room.VictoryCondition = (Wwa.Enum.Victory)Enum.Parse(typeof(Wwa.Enum.Victory), Vic);
                    }
                    var rolearr = roles.OrderBy(t => Guid.NewGuid()).ToList();
                    var dic = new Dictionary<int, string>();
                    for (int iss = 1; iss <= rolearr.Count(); iss++)
                    {
                        var vl = Convert.ToInt32(rolearr[iss - 1]);
                        var name = Enum.Parse(typeof(Wwa.Enum.wolverole), vl.ToString());
                        RoomRole rr = new RoomRole()
                        {
                            LocationId = iss,
                            RoleEnum = (Wwa.Enum.wolverole)name,
                            Rid = RequestMod.RoomId
                        };
                        dic.Add(iss, name.ToString());
                        db.RoomRoles.Add(rr);
                    }
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateRoom_Ac_Response returndata = new CreateRoom_Ac_Response()
                        {
                            RoomID = RequestMod.RoomId,
                            Pnum = RequestMod.Pnum,
                            Victory = room.VictoryCondition
                        };
                        returndata.positions = dic;
                        return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = returndata });
                    }
                    else
                    {
                        return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "房间更新失败" });
                    }
                }

            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + "ChangeSetting房间更新失败(数据库问题):" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "房间更新失败,请联系管理员" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }
        /// <summary>
        /// 创建房间
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateRoom([FromBody]CreateRoom_Ac_Request RequestMod)
        {
            try
            {
                if (RequestMod == null) throw new ArgumentException("非法访问");
                if (RequestMod.Userid < 1) throw new ArgumentException("无此用户");
                if (RequestMod.roles.Split(',').Length != RequestMod.Pnum) throw new ArgumentException("角色与人数不对等");
                var roles = RequestMod.roles.Split(',');
                foreach (var role in roles)
                {
                    var Irole = Convert.ToInt32(role);
                    var rolename = Enum.GetName(typeof(Wwa.Enum.wolverole), Irole);
                    if (string.IsNullOrEmpty(rolename))
                    {
                        throw new ArgumentException("无此角色");
                    }
                }
                int rid = Number.GetByPower();
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    Rooms r = new Rooms
                    {
                        ID = 1,
                        CreateUserId = RequestMod.Userid,
                        IsQuit = false,
                        PersonsNum = RequestMod.Pnum,
                        LogId = 1,
                        RoomId = rid,
                        Createtime = DateTime.Now
                    };
                    var Vic = Enum.GetName(typeof(Wwa.Enum.Victory), RequestMod.Victory);
                    if (string.IsNullOrEmpty(Vic))
                    {
                        r.VictoryCondition = Wwa.Enum.Victory.屠边;
                    }
                    else
                    {
                        r.VictoryCondition = (Wwa.Enum.Victory)Enum.Parse(typeof(Wwa.Enum.Victory), Vic);
                    }
                    var rolearr = roles.OrderBy(t => Guid.NewGuid()).ToList();
                    var dic = new Dictionary<int, string>();
                    for (int iss = 1; iss <= rolearr.Count(); iss++)
                    {
                        var vl = Convert.ToInt32(rolearr[iss - 1]);
                        var name = Enum.Parse(typeof(Wwa.Enum.wolverole), vl.ToString());
                        RoomRole rr = new RoomRole()
                        {
                            LocationId = iss,
                            RoleEnum = (Wwa.Enum.wolverole)name,
                            Rid = rid
                        };
                        dic.Add(iss, name.ToString());
                        db.RoomRoles.Add(rr);
                    }
                    db.Rooms.Add(r);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        CreateRoom_Ac_Response returndata = new CreateRoom_Ac_Response()
                        {
                            RoomID = rid,
                            Pnum = RequestMod.Pnum,
                            Victory = r.VictoryCondition
                        };
                        returndata.positions = dic;
                        return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = returndata });
                    }
                    else
                    {
                        return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "房间创建失败" });
                    }
                }

            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + ":房间创建失败(数据库问题):" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "房间创建失败,请联系管理员" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }
        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult GoInRoom([FromBody]GoInRoom_Ac_Request RequestMod)
        {
            try
            {
                if (RequestMod.RoomId < 1 || RequestMod.LocationId < 1) throw new ArgumentException("非法访问");
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    var room = db.Rooms.Where(t => t.RoomId == RequestMod.RoomId & t.IsQuit == false).FirstOrDefault();
                    var roomroles = db.RoomRoles.Where(t => t.Rid == RequestMod.RoomId && t.LocationId == RequestMod.LocationId).FirstOrDefault();
                    if (room == null || roomroles == null) throw new ArgumentException("房间不存在");
                    GoInRoom_Ac_Response ResponseMod = new GoInRoom_Ac_Response()
                    {
                        RoomID = room.RoomId,
                        Pnum = room.PersonsNum,
                        Victory = room.VictoryCondition,
                        Role = roomroles.RoleEnum.ToString(),
                        Location = roomroles.LocationId
                    };
                    return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = ResponseMod });
                }
            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + ":房间进入失败(数据库问题):" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "房间进入失败,请联系管理员" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }
        /// <summary>
        /// 游戏智能判断是否结束
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult IsEnd([FromBody]IsEnd_Ac_Request RequestMod)
        {
            try
            {
                if (RequestMod == null) throw new ArgumentException("非法访问");
                int persons = RequestMod.Persons;
                int wolves = RequestMod.Wolves;
                int gods = RequestMod.Gods;
                if (wolves == 0) { return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = GoodEnd() }); }
                if (gods == 0 || persons == 0) { return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() }); }
                if (RequestMod.Night)
                {
                    if (wolves < persons + gods)
                    {
                        if (persons + gods - wolves > 1) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                        if (!RequestMod.WolfKing) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                        if (!RequestMod.WolfPower) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                        if (RequestMod.Hunter || RequestMod.WitchDrugs > 0 || RequestMod.Guard) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                        return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                    }
                    else if (wolves == persons + gods)
                    {
                        if (RequestMod.WolfPower)
                        {
                            //狼警没女巫毒死  好人输
                            return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                        }
                        else
                        {
                            if (persons + gods == 2)
                            {
                                return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                            }
                            if (RequestMod.WitchDrugs == 2) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                            if (RequestMod.Guard) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                            return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                        }
                    }
                    else
                    {
                        return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                    }
                }
                else
                {
                    if (wolves < persons + gods)
                    {
                        if (persons + gods - wolves > 1) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                        if (!RequestMod.WolfKing) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                        if (!RequestMod.WolfPower) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                        if (RequestMod.Hunter || RequestMod.WitchDrugs > 0 || RequestMod.Guard) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                        return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                    }
                    else if (wolves == persons + gods)
                    {
                        if (RequestMod.WolfPower)
                        {
                            //狼警没女巫毒死  好人输
                            return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                        }
                        else
                        {
                            if (persons + gods == 2)
                            {
                                if (RequestMod.Guard) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                                if (!RequestMod.BadDrug) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                                return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                            }
                            if (RequestMod.WitchDrugs == 2 || RequestMod.WitchDrugs == 1) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                            if (RequestMod.Guard) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                            if (RequestMod.Hunter && gods >= 2) return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
                            return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                        }
                    }
                    else
                    {
                        return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = WolfEnd() });
                    }
                }
                return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = NotEnd() });
            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + ":QuitRoom无删除记录:" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "记录删除失败,请重试" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }
        /// <summary>
        /// 退出房间
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult QuitRoom([FromBody]QuitRoom_Ac_Request RequestMod)
        {
            try
            {
                if (RequestMod.Roomid < 1) throw new ArgumentException("非法访问");
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    var room = db.Rooms.Where(t => t.RoomId == RequestMod.Roomid & t.IsQuit == false).FirstOrDefault();
                    var roomroles = db.RoomRoles.Where(t => t.Rid == RequestMod.Roomid);
                    var logs = db.WerewlovesLogs.Where(t => t.RoomId == RequestMod.Roomid);
                    if (room == null || roomroles == null) throw new ArgumentException("房间不存在");
                    room.IsQuit = true;
                    db.RoomRoles.RemoveRange(roomroles);
                    db.WerewlovesLogs.RemoveRange(logs);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        return Ok(new AjaxResult() { state = ResultType.success.ToString()});
                    }
                    else
                    {
                        LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + ":QuitRoom无删除记录");
                        return Ok(new AjaxResult() { state = ResultType.success.ToString() });
                    }
                }
            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + ":QuitRoom无删除记录:" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "记录删除失败,请重试" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }

        /// <summary>
        /// 一局游戏结束后
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult GameOver([FromBody]EndAction_Ac_Request RequestMod)
        {
            try
            {
                if (RequestMod.Roomid < 1) throw new ArgumentException("非法访问");
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    var room = db.Rooms.Where(t => t.RoomId == RequestMod.Roomid & t.IsQuit == false).FirstOrDefault();
                    var roomroles = db.RoomRoles.Where(t => t.Rid == RequestMod.Roomid);
                    if (room == null || roomroles == null) throw new ArgumentException("房间不存在");
                    db.RoomRoles.RemoveRange(roomroles);
                    int i = db.SaveChanges();
                    GoInRoom_Ac_Response ResponseMod = new GoInRoom_Ac_Response()
                    {
                        RoomID = room.RoomId,
                        Pnum = room.PersonsNum
                    };
                    return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = ResponseMod });
                }
            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + ":记录删除失败:" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "记录删除失败,请重试" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult AddLog([FromBody]Log_Ac_Request RequestMod)
        {
            try
            {
                if (RequestMod.RoomId < 1 || RequestMod.LocationId < 1) throw new ArgumentException("非法访问");
                if (string.IsNullOrEmpty(RequestMod.Content)) throw new ArgumentException("内容不可为空");
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    WerewlovesLog log = new WerewlovesLog()
                    {
                        CreateTime = DateTime.Now,
                        Content = RequestMod.Content,
                        RoomId = RequestMod.RoomId
                    };
                    db.WerewlovesLogs.Add(log);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        return Ok(new AjaxResult() { state = ResultType.success.ToString() });
                    }
                    else
                    {
                        return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "记录失败" });
                    }
                }
            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + ":Log记录失败(sql):" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "记录失败,请重试" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }
        /// <summary>
        /// 读取全部日志
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult GameLogs([FromBody]GameLogs_Ac_Request RequestMod)
        {
            try
            {
                if (RequestMod.RoomId < 1) throw new ArgumentException("非法访问");
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    GameLogs_Ac_Response res = new GameLogs_Ac_Response();
                    res.logs = db.WerewlovesLogs.Where(t => t.RoomId == RequestMod.RoomId).OrderBy(t => t.CreateTime).ToList();
                    if (res.logs != null)
                    {
                        return Ok(new AjaxResult() { state = ResultType.success.ToString(), data = res });
                    }
                    else
                    {
                        return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "无日志记录" });
                    }
                }
            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + "GameLogs记录失败(sql):" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "无日志记录" });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="RequestMod"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult RemoveLogs([FromBody]GameLogs_Ac_Request RequestMod)
        {
            try
            {
                if (RequestMod.RoomId < 1) throw new ArgumentException("非法访问");
                using (MysqlDbContext db = new MysqlDbContext())
                {
                    var ReturnData = db.WerewlovesLogs.Where(t => t.RoomId == RequestMod.RoomId).ToList();
                    db.WerewlovesLogs.RemoveRange(ReturnData);
                    db.SaveChanges();
                    return Ok(new AjaxResult() { state = ResultType.success.ToString() });
                }
            }
            catch (MySqlException e)
            {
                LogFactory.GetLogger("").Error(Environment.NewLine + DateTime.Now + "RemoveLogs删除失败(sql):" + e.Message);
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (ArgumentException e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                return Ok(new AjaxResult() { state = ResultType.error.ToString(), message = "非法操作:" + e.Message });
            }
        }
        public static IsEnd_Ac_Response NotEnd()
        {
            IsEnd_Ac_Response mod = new IsEnd_Ac_Response()
            {
                IsEnd = false,
                WinRole = 0
            };
            return mod;
        }
        public static IsEnd_Ac_Response WolfEnd()
        {
            IsEnd_Ac_Response mod = new IsEnd_Ac_Response()
            {
                IsEnd = true,
                WinRole = 1
            };
            return mod;
        }
        public static IsEnd_Ac_Response GoodEnd()
        {
            IsEnd_Ac_Response mod = new IsEnd_Ac_Response()
            {
                IsEnd = true,
                WinRole = 2
            };
            return mod;
        }
    }

}