using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Anger_API.Library;

namespace Anger_API.Service.File
{
    public class FileService : IFileService
    {
        public string UploadImage(string imageBase64, DateTime postAt, long postID)
        {
            var imageByte = Convert.FromBase64String(imageBase64);

            var mediaFilePath = Anger.GetConfig().MediaFilePath;
            var mediaFileUrl = Anger.GetConfig().MediaFileUrl;

            var subpath = "\\" + postAt.ToString("yyyyMMdd") + "\\" + postID;
            var filePath = mediaFilePath + subpath;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            var fileName = "cover.jpeg";

            using (var imageFile = new FileStream(filePath + "\\" + fileName, FileMode.Create))
            {
                imageFile.Write(imageByte, 0, imageByte.Length);
                imageFile.Flush();
            }

            var fileUrl = mediaFileUrl + "/" + postAt.ToString("yyyyMMdd") + "/" + postID + "/" + fileName;

            return fileUrl;
        }
    }
}