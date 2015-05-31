using Newtonsoft.Json;
using SpajamHonsen.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SpajamHonsen.Utilities
{
    /// <summary>
    /// GoogleAPIのユーティリティークラス
    /// </summary>
    /// <remarks>
    /// GoogleAPIのユーティリティークラス
    /// </remarks>
    public class GoogleUtil
    {
        /// <summary>
        /// GoogleSpeechAPIにリクエスト送信
        /// </summary>
        /// <param name="byteArray">音声ファイルのByte配列</param>
        /// <returns>音声解析結果文字列</returns>
        public static async Task<string> RequestGoogleSpeechAPIAsync(byte[] byteArray)
        {
            var httpClient = new HttpClient();
            var mediaType = new MediaTypeWithQualityHeaderValue("audio/x-flac");
            var parameter = new NameValueHeaderValue("rate", "16000");
            mediaType.Parameters.Add(parameter);

            var url = "https://www.google.com/speech-api/v2/recognize?output=json&lang=ja-jp&key=";
            var appSettings = ConfigurationManager.AppSettings;
            var apiKey = appSettings["GoogleSpeechAPIKey"];
            var uri = new Uri(url + apiKey);

            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                var result = await httpClient.PostAsync(uri, param);

                var responseFromServer = await result.Content.ReadAsStringAsync();
                var responceArray = responseFromServer.Split('\n');
                var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SpajamHonsen.Models.GoogleSpeechAPIResponseModel.Resuls>(responceArray[1]));

                return responseJson.result[0].alternative[0].transcript;
            }
        }
    }
}