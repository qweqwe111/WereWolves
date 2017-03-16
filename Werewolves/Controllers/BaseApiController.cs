using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Werewolves.Results;
using Wwa.Entities.Enum;
namespace Werewolves.Controllers
{
    public class BaseApiController : ApiController
    {

        //protected ApiErrorResult Error(string errorMessage)
        //{
        //    return new ApiErrorResult(this.Request, errorMessage);
        //}

        //protected ApiErrorResult Error(Exception exception)
        //{
        //    return new ApiErrorResult(this.Request, exception);
        //}

        //protected ApiErrorResult Error(ApiError errorBody)
        //{
        //    return new ApiErrorResult(this.Request, errorBody);
        //}

        protected IHttpActionResult Fail(string message, int code)
        {
            return Ok(new FailResult(code, message));
        }

        protected IHttpActionResult Fail(Currency errorCode)
        {
            return Ok(new FailResult((int)errorCode, errorCode.ToString()));
        }

        //protected IHttpActionResult ModelIf<T>(T model, Func<T, IHttpActionResult> acter)
        //    where T : class
        //{
        //    if (default(T) == model || !this.ModelState.IsValid)
        //    {
        //        return Fail(ErrorEnum.参数传递错误);
        //    }
        //    return acter.Invoke(model);
        //}
    }
}