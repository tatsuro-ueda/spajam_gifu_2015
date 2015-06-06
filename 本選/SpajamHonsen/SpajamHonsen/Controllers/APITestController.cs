using BingTranslrate;
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
        #region GET: api/APITest
        /// <summary>
        /// リクエストのテキストをMicrosoftTranslatorAPIで翻訳して返す
        /// </summary>
        /// <param name="text">翻訳対象テキスト</param>
        /// <returns></returns>
        public async Task<string> GetGoogleJapaneseAPIAsync(string text)
        {
            var bingUtil = new BingUtil();

            var cbingTranslate = new CBingTranslate();
            cbingTranslate.Init("thirauti", "Hs9iRQTNGRpko9cMhU1sdpPyuKrrXD5u3oAOmPtoJAg=");
            var result = cbingTranslate.TranslateMethod("ありがとう", "ja", "en");
            return result;
            // return await bingUtil.RequestMicrosoftTranslatorAPITranslateAsync("", "");
        }

        #endregion GET: api/APITest

        #region POST: api/APITest
        // GoogleSpeechAPI(音声解析)
        /// <summary>
        /// Base64形式で音声ファイル(content-type:x-wav rete:44100)をPOSTして
        /// 音声ファイル(content-type:x-flac rete:16000)に変換して
        /// GoogleSpeechAPIで音声を解析して返す
        /// </summary>
        /// <param name="request"></param>
        /// <returns>音声解析結果テキスト(1番目)</returns>
        public async Task<string> PostGoogleSpeechAPIAsync(TestRequestModel request)
        {
            byte[] byteArray = System.Convert.FromBase64String(request.Base64String);

            var filePath = HttpContext.Current.Server.MapPath("~/Temp/Audios/" + Guid.NewGuid().ToString());
            //ファイルを作成して書き込む
            using (System.IO.FileStream fs = 
                new System.IO.FileStream(
                    filePath,
                    System.IO.FileMode.Create,
                    System.IO.FileAccess.Write))
           {
                //バイト型配列の内容をすべて書き込む
                fs.Write(byteArray, 0, byteArray.Length);
           }

            var result = FFmpegUtil.ConvertAudioRate(filePath, "16000");


            return result;
            // return await GoogleUtil.RequestGoogleSpeechAPIAsync(byteArray);
        }

        /* TODO 未完了　BingVoiceRecognitionAPI(音声解析)
        /// <summary>
        /// Base64形式で音声ファイル(content-type:x-flac rete:16000)をPOSTしてBingVoiceRecognitionAPIで音声を解析して返す
        /// </summary>
        /// <param name="request"></param>
        /// <returns>音声解析結果テキスト</returns>
        public async Task<string> PostMicrosoftBingVoiceRecognitionAPIAsync(TestRequestModel request)
        {
            byte[] byteArray = System.Convert.FromBase64String(request.Base64String);
            return await BingUtil.RequestMicrosoftBingVoiceRecognitionAPIAsync(byteArray);
        }
        */

        /* 完了 GoogleSpeechAPI(音声解析)
        /// <summary>
        /// Base64形式で音声ファイル(content-type:x-flac rete:16000)をPOSTしてGoogleSpeechAPIで音声を解析して返す
        /// </summary>
        /// <param name="request"></param>
        /// <returns>音声解析結果テキスト(1番目)</returns>
        public async Task<string> PostGoogleSpeechAPIAsync(TestRequestModel request)
        {
            byte[] byteArray = System.Convert.FromBase64String(request.Base64String);
            return await GoogleUtil.RequestGoogleSpeechAPIAsync(byteArray);
        }
        */

        /* 完了 Google音声解析
        /// <summary>
        /// リクエストのテキストをVoiceTextAPIで音声合成してAzureにアップする
        /// その後アップしたURLを返却する
        /// </summary>
        /// <param name="request">音声にしたいテキスト</param>
        /// <returns></returns>
        public async Task<string> PostVoiceTextAPIAsync(TestRequestModel request)
        {
            var othersUtil = new OthersUtil();
            var fileID = await othersUtil.RequestVoiceTextAPI(request.Base64String, "hikari");
            var azureStorageUtil = new AzureStorageUtil();
            return azureStorageUtil.GetBlobStrageUrl(fileID, "voicetext");
        }
        */


        /* 完了 GetGoogleJapaneseAPI(ひらがな→漢字変換)
        /// <summary>
        /// リクエストのテキストをGoogle日本語入力APIでひらがなを漢字変換して返す
        /// その後アップしたURLを返却する
        /// </summary>
        /// <param name="text">ひらがなテキスト</param>
        /// <returns></returns>
        public async Task<string> GetGoogleJapaneseAPIAsync(string text)
        {
            return await GoogleUtil.RequestGoogleJapaneseAPI(text);
        }
        */
        #endregion POST: api/APITest
    }
}