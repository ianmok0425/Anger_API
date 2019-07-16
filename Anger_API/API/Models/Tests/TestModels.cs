using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anger_Library;

namespace Anger_API.API.Models.Tests
{
    public class TestRequest
    {
        public int Id { get; set; }
    }
    public class TestResponse : ResponseBase
    {
        public string Message { get; set; }
    }
}