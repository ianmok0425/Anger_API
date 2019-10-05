using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.Settings;

namespace Anger_API.API.Controllers.Settings
{
    using Database.Settings;
    [ApiKeyAuthorize]
    public class SettingController : AngerApiController
    {
        public ISettingRepository SettingRepo { get; }
        public SettingController(
           ISettingRepository settingRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            SettingRepo = settingRepo ?? throw new ArgumentNullException(nameof(SettingRepo));
        }
        [Route("api/favpost/get")]
        [HttpGet]
        public async Task<AngerResult> GetFavPost([FromUri] GetSettingRequest model)
        {
            var setting = await SettingRepo.Retrieve();
            var rsp = new GetSettingResponse() { Setting = setting };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}