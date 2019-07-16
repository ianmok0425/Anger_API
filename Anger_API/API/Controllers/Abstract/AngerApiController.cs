using Anger_API.API.Models.Abstract;

namespace Anger_API.API.Controllers.Abstract
{
    public class AngerApiController : BaseApiController<AngerResult>
    {
        protected IResultFactory<AngerResult> ResultFactory;

        public AngerApiController(IResultFactory<AngerResult> resultFactory)
        {
            ResultFactory = resultFactory;
        }
    }
}