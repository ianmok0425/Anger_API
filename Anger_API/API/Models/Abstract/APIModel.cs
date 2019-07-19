using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

using Newtonsoft.Json;

using Anger_Library;

namespace Anger_API.API.Models.Abstract
{
    public class AngerResult : APIResult
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Code = -1;
        public string MessageTC;
        public string MessageSC;

        public AngerResult() { }
        public AngerResult(ReturnCode statusCode, string msg, ResponseBase d = null)
        {
            HttpStatusCode = (int)statusCode;
            Code = -1;
            Message = "Exception Error: " + msg;
            MessageTC = "Exception Error: " + msg;
            MessageSC = "Exception Error: " + msg;
            Data = d;
        }

        public static AngerResult SystemError(string msg)
        {
            return new AngerResult()
            {
                HttpStatusCode = (int)ReturnCode.Error500,
                Code = -1,
                Message = msg,
                MessageTC = msg,
                MessageSC = msg
            };
        }
    }
    public class APIMessage
    {
        public string Message;
        public string MessageTC;
        public string MessageSC;

        public APIMessage()
        {
            Message = "Success";
            MessageTC = "成功";
            MessageSC = "成功";
        }
    }

    public enum ReturnCode
    {
        OK200 = HttpStatusCode.OK,
        Created201 = HttpStatusCode.Created,
        Accepted202 = HttpStatusCode.Accepted,
        NoContent204 = HttpStatusCode.NoContent,
        Unauth401 = HttpStatusCode.Unauthorized,
        Forbidden403 = HttpStatusCode.Forbidden,
        NotFound404 = HttpStatusCode.NotFound,
        Error500 = HttpStatusCode.InternalServerError,

        //  Custom Code on the below
        UnauthNeedPromote601 = 601,
        UnauthExpired602 = 602,

        ResourceExpired701 = 701
    }

    public enum APIReturnCode
    {
        Success = 0,
    }
}