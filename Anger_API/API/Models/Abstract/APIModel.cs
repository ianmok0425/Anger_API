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

        public APIMessage(APIReturnCode code)
        {
            switch (code)
            {
                case APIReturnCode.Success:
                    Message = "Success";
                    MessageTC = "成功";
                    MessageSC = "成功";
                    break;

                // Reg PreMember
                case APIReturnCode.EmailExist:
                    Message = "Email exist";
                    MessageTC = "電郵已被註冊";
                    MessageSC = "电邮已被注册";
                    break;
                case APIReturnCode.MobileExist:
                    Message = "Mobile exist";
                    MessageTC = "手機已被註冊";
                    MessageSC = "手机已被注册";
                    break;
                case APIReturnCode.AccountExist:
                    Message = "Account exist";
                    MessageTC = "帳號已被註冊";
                    MessageSC = "帐号已被注册";
                    break;
                case APIReturnCode.PreMemberNotExist:
                    Message = "Premember not exist";
                    MessageTC = "帳號不存在";
                    MessageSC = "帐号不存在";
                    break;
                case APIReturnCode.PreMemberHasBeenVerified:
                    Message = "PreMember has been verified";
                    MessageTC = "帳號已驗證";
                    MessageSC = "帐号已验证";
                    break;  
                case APIReturnCode.InvalidVerifyCode:
                    Message = "Invalid Verfiy Code";
                    MessageTC = "驗證碼不正確";
                    MessageSC = "验证码不正确";
                    break;
                case APIReturnCode.InvalidAccount:
                    Message = "Invalid Account";
                    MessageTC = "帳號錯誤";
                    MessageSC = "帐号错误";
                    break;
                case APIReturnCode.InvalidPassword:
                    Message = "Invalid Password";
                    MessageTC = "密碼錯誤";
                    MessageSC = "密码错误";
                    break;
                case APIReturnCode.MemberNotExist:
                    Message = "Member Not Exist";
                    MessageTC = "會員不存在";
                    MessageSC = "会员不存在";
                    break;
                case APIReturnCode.AdminNotExist:
                    Message = "Admin Not Exist";
                    MessageTC = "管理員不存在";
                    MessageSC = "管理员不存在";
                    break;
            }
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

        // Reg PreMember
        EmailExist = 1,
        MobileExist = 2,
        AccountExist = 3,
        PreMemberNotExist = 4,
        PreMemberHasBeenVerified = 5,
        InvalidVerifyCode = 6,

        // Member
        InvalidAccount = 7,
        InvalidPassword = 8,
        MemberNotExist = 9,

        // Admin
        AdminNotExist = 10
    }
}