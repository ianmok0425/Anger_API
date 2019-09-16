using System;
using System.Threading.Tasks;
using System.Web.Http;

using Anger_API.API.Attributes;
using Anger_API.API.Controllers.Abstract;

using Anger_API.API.Models.Abstract;
using Anger_API.API.Models.Posts;

using Anger_API.Database.Members;
using Anger_API.Database.Admins;
using Anger_API.Database.Posts;
using Anger_API.Service.File;

namespace Anger_API.API.Controllers.Posts
{
    [ApiKeyAuthorize]
    public class PostController : AngerApiController
    {
        public IFileService FileService { get; }
        public IPostRepository PostRepo { get; }
        public IMemberRepository MemberRepo { get; }
        public IAdminRepository AdminRepo { get; }
        public PostController(
            IFileService fileService,
            IPostRepository postRepo,
            IMemberRepository memberRepo,
            IAdminRepository adminRepo,
           IResultFactory<AngerResult> resultFactory) : base(resultFactory)
        {
            FileService = fileService ?? throw new ArgumentNullException(nameof(FileService));
            PostRepo = postRepo ?? throw new ArgumentNullException(nameof(PostRepo));
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

            var member = await MemberRepo.RetrieveByID<Member>(model.MemberID);
            if(member == null) return ResultFactory.CreateResult(ReturnCode.Error500, APIReturnCode.MemberNotExist);

            var utcNow = DateTime.UtcNow;

            var post = new Post()
            {
                MemberID = model.MemberID,
                Subject = model.Subject,
                CoverUrl = null,
                Content = model.Content,
                ViewCount = 0,
                PostAt = model.PostAt,
                RejectReason = model.RejectReason,
                ApprovedBy = model.AdminID,
                ApprovedAt = utcNow,
                EmailNotice = false,
                UpdatedAt = utcNow
            };
            var postID = await PostRepo.CreateAsync(post);

            string coverUrl = FileService.UploadImage(model.CoverBase64, model.PostAt, Convert.ToInt64(postID));
            post.CoverUrl = coverUrl;
            await PostRepo.Update(Convert.ToInt64(postID), post);

            var rsp = new UploadPostResponse() { CoverUrl = coverUrl };
            return ResultFactory.CreateResult(ReturnCode.Created201, APIReturnCode.Success, rsp);
        }
    }
}