using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SpajamHonsen.Utilities
{
    /// <summary>
    /// BingoAPIのユーティリティークラス
    /// </summary>
    /// <remarks>
    /// BingoAPIのユーティリティークラス
    /// </remarks>
    public static class BingUtil
    {
        /// <summary>
        /// MicrosoftBingVoiceRecognitionAPIにリクエスト送信
        /// </summary>
        /// <param name="byteArray">音声ファイルのByte配列</param>
        /// <returns></returns>
        public static async Task<string> RequestMicrosoftBingVoiceRecognitionAPIAsync(byte[] byteArray)
        {
            var httpClient = new HttpClient();

            //content-type指定
            var mediaType = new MediaTypeWithQualityHeaderValue("audio/wav");
            var parame1 = new NameValueHeaderValue("samplerate", "16000");
            var parame2 = new NameValueHeaderValue("sourcerate", "8000");
            var parame3 = new NameValueHeaderValue("trustsourcerate", "false");
            mediaType.Parameters.Add(parame1);
            mediaType.Parameters.Add(parame2);
            mediaType.Parameters.Add(parame3);

            httpClient.DefaultRequestHeaders.Accept.Add(mediaType);

            var builder = new UriBuilder("https://speech.platform.bing.com/recognize");
            builder.Port = -1;

            // QueryStringの設定
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["Version"] = "3.0";
            query["requestid"] = Guid.NewGuid().ToString();
            query["appID"] = "D4D52672-91D7-4C74-8AD8-42B1D98141A5";
            query["format"] = "json";
            query["locale"] = "ja-JP";
            query["device.os"] = "Windows OS";
            query["scenarios"] = "ulm";
            query["instanceid"] = Guid.NewGuid().ToString(); ;

            builder.Query = query.ToString();

            string url = builder.ToString();
            var uri = new Uri(url);

            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                // TODO response403 何らかの認証が必要？
                var result = await httpClient.PostAsync(uri, param);

                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}