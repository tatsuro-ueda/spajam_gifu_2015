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

        public static async Task<string> RequestBaiduSpeechAPIAsync(byte[] byteArray)
        {
            var httpClient = new HttpClient();
            var mediaType = new MediaTypeWithQualityHeaderValue("audio/x-flac");
            var parameter = new NameValueHeaderValue("rate", "16000");
            mediaType.Parameters.Add(parameter);

            var url = "http://vop.baidu.com/server_api?lan=zh";
            url += "&cuid=u7CHooimP8rCsOlzNzW50C66";
            url += "&token=24.d7ca91217c8dcd828bd374b03d40b799.2592000.1438064825.282335-6310719";
            url += "&format=wav";
            // url += "&rate=8000";
            url += "&channel=1";
            var uri = new Uri(url);

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