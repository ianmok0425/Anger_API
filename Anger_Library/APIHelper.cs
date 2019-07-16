using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Anger_Library
{
    public abstract class APIResult
    {
        [JsonIgnore]
        public int HttpStatusCode;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Code;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message;

        public string DateTime = System.DateTime.Now.ToString("yyyyMMddHHmmss");

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ResponseBase Data;

        public APIResult() { }
    }
    public abstract class ResponseBase
    {
    }

    public class APIException : Exception
    {
        public int Code = (int)HttpStatusCode.InternalServerError;

        public APIException() : base() { }
        public APIException(int code, string msg) : base(msg)
        {
            Code = code;
        }
        public APIException(string msg) : base(msg) { }

        public static void ExRequiredAtLeastOne(string display, params string[] values)
        {
            foreach (string v in values)
            {
                if (!string.IsNullOrWhiteSpace(v))
                    return;
            }

            throw new APIException(display);
        }

        public static void ExValidParams(object obj, string display)
        {
            if (obj == null) ExInvalidParams(display);
        }
        public static void ExInvalidParams(string display)
        {
            throw new APIException("Invalid " + display + " parameter.");
        }
        public static void ExRequired(string value, string display)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new APIRequired(display);
        }


        /// <summary>
        /// Validate string value and parse to DateTime object in UTC
        /// </summary>
        /// <param name="value"></param>
        /// <param name="display"></param>
        /// <param name="dateValue"></param>
        /// <param name="format"></param>
        /// <param name="timeZone"></param>
        public static void ExValidDecimal(string value, string display, ref decimal decValue)
        {
            bool result = decimal.TryParse(value, out decValue);
            if (!result)
                throw new APIInvalidFormat(display, value);
        }
        public static void ExValidInt(string value, string display, ref int intValue)
        {
            bool result = int.TryParse(value, out intValue);
            if (!result)
                throw new APIInvalidFormat(display, value);
        }
        public static void ExValidDouble(string value, string display, ref double dblValue)
        {
            bool result = double.TryParse(value, out dblValue);
            if (!result)
                throw new APIInvalidFormat(display, value);
        }

        public static void ExValidOption<TEnum>(int value, string name, out TEnum option) where TEnum : struct
        {
            ExValidOption(value.ToString(), name, out option);
        }
        public static void ExValidOption<TEnum>(string value, string name, out TEnum option) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("Must be an enumerated type.");
            if (!Enum.TryParse(value, out option)) throw new APIInvalidValue(value, name);
            if (!Enum.IsDefined(typeof(TEnum), option)) throw new APIInvalidValue(value, name);
        }
        public static void ExValidOption<TEnum>(int value, string name, out TEnum? option) where TEnum : struct
        {
            ExValidOption(value.ToString(), name, out option);
        }
        public static void ExValidOption<TEnum>(string value, string name, out TEnum? option) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("Must be an enumerated type.");
            if (!Enum.TryParse(value, out TEnum tmpOption)) throw new APIInvalidValue(value, name);
            else option = tmpOption;
            if (!Enum.IsDefined(typeof(TEnum), option)) throw new APIInvalidValue(value, name);
        }

        public static void ExValidGuid(string value, string display, ref Guid guidValue)
        {
            bool result = Guid.TryParse(value, out guidValue);
            if (!result)
                throw new APIInvalidFormat(display, value);
        }
        public static void ExValidBool(string value, string display, ref bool boolValue)
        {
            bool result = bool.TryParse(value, out boolValue);
            if (!result)
                throw new APIInvalidFormat(display, value);
        }

        public static void ExRequiredInt(string value, string display, ref int intValue)
        {
            ExRequired(value, display);

            ExValidInt(value, display, ref intValue);
        }
        public static void ExRequiredDecimal(string value, string display, ref decimal decValue)
        {
            ExRequired(value, display);

            ExValidDecimal(value, display, ref decValue);
        }
        public static void ExRequiredDouble(string value, string display, ref double dblValue)
        {
            ExRequired(value, display);

            ExValidDouble(value, display, ref dblValue);
        }
        public static void ExRequiredGuid(string value, string display, ref Guid guidValue)
        {
            ExRequired(value, display);

            ExValidGuid(value, display, ref guidValue);
        }
        public static void ExRequiredBool(string value, string display, ref bool boolValue)
        {
            ExRequired(value, display);

            ExValidBool(value, display, ref boolValue);
        }
    }
    public class APIInvalidMethod : APIException
    {
        public APIInvalidMethod(string msg)
            : base(string.Format("Invalid API Method - {0}.", msg))
        { }
    }
    public class APIInvalidKey : APIException
    {
        public override string Message
        {
            get
            {
                return "Invalid Key for API";
            }
        }
    }

    public class APIRequired : APIException
    {
        public APIRequired(string display)
            : base(display + " is required.")
        { }
    }

    public class APIInvalidFormat : APIException
    {
        public APIInvalidFormat(string display, string value)
            : base(string.Format("Invalid format of {0} with value {1}", display, value))
        { }
    }
    public class APIInvalidValue : APIException
    {
        public APIInvalidValue(string display, string value)
            : base(string.Format("Invalid value {0} for {1}", value, display))
        { }
    }
    public class APIStopped : APIException
    {
        public APIStopped() : base("API has been stopped service.") { }
    }

}
