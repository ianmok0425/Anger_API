using Anger_Library;

namespace Anger_API.API.Models.Abstract
{
    public class ResultFactory : IResultFactory<AngerResult>
    {
        public AngerResult CreateResult(ReturnCode statusCode, APIReturnCode apiCode, ResponseBase d = null)
        {
            var returnMsg = new APIMessage();

            return new AngerResult()
            {
                HttpStatusCode = (int)statusCode,
                Code = (int)apiCode,
                Message = returnMsg.Message,
                MessageTC = returnMsg.MessageTC,
                MessageSC = returnMsg.MessageSC,
                Data = d
            };
        }
    }
}