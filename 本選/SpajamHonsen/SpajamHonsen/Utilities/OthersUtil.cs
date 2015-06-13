using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SpajamHonsen.Utilities
{
    /// <summary>
    /// Spajam予選で使用した音声関連WEBAPIのユーティリティークラス
    /// </summary>
    /// <remarks>
    /// その他のWEBAPIのユーティリティークラス
    /// </remarks>
    public class OthersUtil
    {
        /// <summary>
        /// VoiceTextAPIにリクエスト送信
        /// </summary>
        /// <param name="voiceText">音声合成するテキスト</param>
        /// <param name="speaker">スピーカー</param>
        /// <returns>ファイルIDのstring</returns>
        public async Task<string> RequestVoiceTextAPI(string voiceText,string speaker)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var username = appSettings["VoiceTextAPIUser"];
            var url = "https://api.voicetext.jp/v1/tts";

            var authHeader = new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, ""))));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = authHeader;

            var uri = new Uri(url);

            var param = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "text", voiceText },
                    { "speaker", speaker },
                });

            var result = await client.PostAsync(uri, param);
            var stream = await result.Content.ReadAsStreamAsync();

            var fileName = Guid.NewGuid().ToString();
            var containerName = "voices";

            var azureStorageUtil = new AzureStorageUtil();
            await azureStorageUtil.UploadBlobStrage(stream, fileName, containerName);
            return fileName;
        }
    }
}