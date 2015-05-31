using SpajamHonsen.Models;
using SpajamHonsen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SpajamHonsen.Controllers
{
     /// <summary>
     /// WEBAPIテスト用のコントローラー
     /// </summary>
    public class APITestController : ApiController
    {
        
        // GET: api/Talks
        /// <summary>
        /// APIテスト用メソッド
        /// </summary>
        /// <returns></returns>
        public async Task<string> PostAPITestAsync(TestRequestModel request)
        {
            byte[] byteArray = System.Convert.FromBase64String(request.Base64String);


            // TODO Bing音声解析 return await BingUtil.RequestMicrosoftBingVoiceRecognitionAPIAsync(byteArray);
            return await GoogleUtil.RequestGoogleSpeechAPIAsync(byteArray);
        }
    }
}