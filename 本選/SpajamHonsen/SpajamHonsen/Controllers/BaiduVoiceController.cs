using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SpajamHonsen.Models;
using SpajamHonsen.Utilities;

namespace SpajamHonsen.Controllers
{
    public class BaiduVoiceController : ApiController
    {
        /// <summary>
        /// PostVoiceTextAPIAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> PostVoiceTextAPIAsync(TestRequestModel request)
        {
            var fileID = await BaiduUtil.RequestVoiceTextAPI(request.Base64String);
            var azureStorageUtil = new AzureStorageUtil();
            return azureStorageUtil.GetBlobStrageUrl(fileID, "voicetext");
        }
    }
}
