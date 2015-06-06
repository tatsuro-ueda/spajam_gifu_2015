using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SpajamHonsen.Models;

namespace SpajamHonsen.Utilities
{
    public class BaiduUtil
    {
        public static async Task<string> RequestBaiduTranslateAPIAsync(string text)
        {
            var httpClient = new HttpClient();

            var url = "https://www.googleapis.com/language/translate/v2?target=zh-CN&source=ja&key=AIzaSyAKrLfZ14Ktn4bqx_n8gnSV1aWMNLW9RBA";

            url += "&q=" + HttpUtility.UrlEncode(text);

            var result = await httpClient.GetAsync(new Uri(url));

            var responseFromServer = await result.Content.ReadAsStringAsync();

            return responseFromServer;   
        }


        public async Task<string> RequestVoiceTextAPI(string voiceText)
        {

            var url = "http://tsn.baidu.com/text2audio";
                
            var client = new HttpClient();

            var uri = new Uri(url);

            var param = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "tex", voiceText },
                    { "lan", "zh" },
                    { "cuid", "xxx" },
                    { "ctp", "1" },
                    { "tok", "24.63243d4ac69f030ff6a59ba77a9d34b5.2592000.1435312482.282335-6072649" },
                });

            var result = await client.PostAsync(uri, param);
            var stream = await result.Content.ReadAsStreamAsync();

            var fileName = Guid.NewGuid().ToString();
            var containerName = "voicetext";

            var azureStorageUtil = new AzureStorageUtil();
            await azureStorageUtil.UploadBlobStrage(stream, fileName, containerName);
            return fileName;
        }
    }
}