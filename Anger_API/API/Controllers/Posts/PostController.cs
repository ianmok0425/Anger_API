using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.Members;
using Anger_API.API.Models.Posts;

using Anger_API.Database.Members;
using Anger_API.Database.Admins;
using System.IO;

namespace Anger_API.API.Controllers.Posts
{
    [ApiKeyAuthorize]
    public class PostController : AngerApiController
    {
        public IMemberRepository MemberRepo { get; }
        public IAdminRepository AdminRepo { get; }
        public PostController(
            IMemberRepository memberRepo,
            IAdminRepository adminRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            MemberRepo = memberRepo ?? throw new ArgumentNullException(nameof(MemberRepo));
            AdminRepo = adminRepo ?? throw new ArgumentNullException(nameof(AdminRepo));
        }
        [HttpPost]
        [Route("api/post/upload")]
        public async Task<AngerResult> RegPreMember([FromBody] UploadPostRequest model)
        {
            if (model == null) throw new NullReferenceException();
            model.Validate();

            var admin = await AdminRepo.RetrieveByID<Database.Admins.Admin> (model.AdminID);
            if (admin == null) return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.AdminNotExist);

            // to be continue...
            var bytes = Convert.FromBase64String(model.CoverBase64);
            using (var imageFile = new FileStream(@"C:\Users\ianmok\Desktop\BackUp\123.png", FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
            /******************************************************************************/

            var rsp = new UploadPostResponse() { };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}