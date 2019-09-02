using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

using OfficeOpenXml;

using Anger_API.API.Controllers.Abstract;
using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.Admins;

using Anger_API.Service.Admin.RunningText;
using Anger_API.Database.Admins;

namespace Anger_API.API.Controllers.Admin
{
    public class AdminController : AngerApiController
    {
        public IAdminRepository AdminRepo { get; }
        public IRunningTextService RunningTextService { get; }
        public AdminController(
            IAdminRepository adminRepo,
            IRunningTextService runningTextService,
            IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            AdminRepo = adminRepo ?? throw new ArgumentNullException(nameof(AdminRepo));
            RunningTextService = runningTextService ?? throw new ArgumentNullException(nameof(RunningTextService));
        }
        [HttpGet]
        [Route("api/admin/getRunningText")]
        public async Task<HttpResponseMessage> GetRunningText([FromUri] GetRunningTextRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();

            bool verifyAdmin = await AdminRepo.VerfiyAdmin(model.Account, model.Password);
            if (!verifyAdmin) throw new Exception("Admin Account or Password incorrect.");

            DateTime? createdOn = string.IsNullOrWhiteSpace(model.CreatedOn) ? null : (DateTime?)DateTime.Parse(model.CreatedOn);

            ExcelPackage excel = await RunningTextService.GenerateList(model.ActionType, createdOn);

            string today = DateTime.Now.ToString("yyyyMMdd");

            MediaTypeHeaderValue mediaType =
                MediaTypeHeaderValue.Parse("application/octet-stream");
            MemoryStream memoryStream = new MemoryStream(excel.GetAsByteArray());
            HttpResponseMessage response =
                    response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(memoryStream);
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("fileName") { FileName = "RunningText" + today + ".xlsx" };
            Request.RegisterForDispose(response);
            return response;
        }
    }
}