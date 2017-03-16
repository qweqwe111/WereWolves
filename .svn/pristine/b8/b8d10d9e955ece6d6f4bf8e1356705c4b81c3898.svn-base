using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wwa.Entities.Enum;
namespace Werewolves.Results
{
    public abstract class BaseResult
    {
        public string Message { get; set; }

        public int Code { set; get; }
    }

    public class FailResult : BaseResult
    {
        public FailResult(int code, string message)
        {
            //code =0 就为请求正确,否则为错误码
            if (code > 0)
            {
                this.Code = code;
                this.Message = message;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }

    public class SuccessResult : BaseResult
    {
        public new int Code { get { return (int)Currency.数据请求成功; } }

        public new string Message { get { return Currency.数据请求成功.ToString(); } }
    }
}