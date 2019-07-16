using Anger_Library;

namespace Anger_API.API.Models.Abstract
{
    public interface IResultFactory<T>
    {
        T CreateResult(ReturnCode statusCode, APIReturnCode apiCode, ResponseBase d = null);
    }
}