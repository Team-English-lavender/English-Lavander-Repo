using DropNet;
using DropNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Chat.Client.Providers
{
    public class DropboxProvider
    {
        private const string ApiKey = "7n30c34c0yhtg86";
        private const string ApiSecret = "1h21faykdj3spqf";

        private static DropNetClient client = new DropNetClient(ApiKey, ApiSecret, "8p74Yi1kaCsAAAAAAAAALItOq4iHqNkREQBikE_r-azu9UPzu6Hlbku62XD0f3SV");

        public static void UploadFile(string filename, byte[] fileBytes)
        {
            var metadata = client.UploadFile("/", filename, fileBytes);
        }

        public static string GetDownloadUrl(string filename)
        {
            var share = client.GetShare("/" + filename);
            return share.Url;
        }
    }
}