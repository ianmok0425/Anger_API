using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;

namespace Anger_API.API.Models.Tests
{
    public class TestRequest : APIRequest
    {
        public string ID { get; set; }
        public int IDVal;
        public override void Validate()
        {
            base.Validate();
            APIException.ExRequiredInt(ID, nameof(ID), ref IDVal);
        }
    }
    public class TestResponse : ResponseBase
    {
        public string Message { get; set; }
    }
}