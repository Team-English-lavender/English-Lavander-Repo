using Chat.Client.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Chat.Client.Controllers
{
    [RoutePrefix("api/Files")]
    public class FilesController : BaseController
    {
        [HttpPost, Route("UploadFile")]
        public IHttpActionResult UploadFile(string groupId = "")
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["uploadFile"];

                if (httpPostedFile != null)
                {
                    string filename = httpPostedFile.FileName;
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        fileData = binaryReader.ReadBytes(HttpContext.Current.Request.Files[0].ContentLength);
                    }

                    filename = groupId + DateTime.Now.ToString("o") + filename;
                    DropboxProvider.UploadFile(filename, fileData);
                    string url = DropboxProvider.GetDownloadUrl(filename);
                }
            }

            return Ok();
        }
    }
}
