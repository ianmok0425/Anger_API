using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;

namespace Anger_API.Service.File
{
    public interface IFileService
    {
        string UploadImage(string imageBase64, DateTime postAt, long postID);
    }
}