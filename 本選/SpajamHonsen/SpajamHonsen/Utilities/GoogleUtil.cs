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
        private static async Task<string> RequestGoogleSpeechAPI(byte[] byteArray)
        {
            var httpClient = new HttpClient();

            //content-type指定
            var mediaType = new MediaTypeWithQualityHeaderValue("audio/x-flac");
            // var parameter = new NameValueHeaderValue("rate", "16000");
            //var mediaType = new MediaTypeWithQualityHeaderValue("audio/x-wav");
            var parameter = new NameValueHeaderValue("rate", "44100");
            mediaType.Parameters.Add(parameter);
            // httpClient.DefaultRequestHeaders.Accept.Add(mediaType);

            var url = "https://www.google.com/speech-api/v2/recognize?output=json&lang=ja-jp&key=";
            var appSettings = ConfigurationManager.AppSettings;
            var apiKey = appSettings["GoogleSpeechAPIKey"];
            var uri = new Uri(url + apiKey);

            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                var result = await httpClient.PostAsync(uri, param);

                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}