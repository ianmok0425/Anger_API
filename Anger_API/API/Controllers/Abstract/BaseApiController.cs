using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using Anger_Library;

namespace Anger_API.API.Controllers.Abstract
{
    public abstract class BaseApiController<T> : ApiController where T : APIResult, new()
    {
    }
}