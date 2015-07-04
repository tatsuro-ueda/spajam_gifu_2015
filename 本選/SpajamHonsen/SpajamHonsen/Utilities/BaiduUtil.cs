using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SpajamHonsen.Models;
using SpajamHonsen.Models.JsonResponse;

namespace SpajamHonsen.Utilities
{
    public class BaiduUtil
    {

        public static async Task<string> RequestBaiduSpeechAPIAsync(byte[] byteArray)
        {
            // 音声解説ファイルのレート変換
            var inputFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/" + Guid.NewGuid().ToString() + ".wav");

            // 一時的に音声ファイルを作成
            using (System.IO.FileStream inputFileStream =
                new System.IO.FileStream(
                    inputFilePath,
                    System.IO.FileMode.Create,
                    System.IO.FileAccess.Write))
            {
                // バイト型配列の内容をすべて書き込む
                inputFileStream.Write(byteArray, 0, byteArray.Length);
            }

            // 音声ファイルのレートを16000に変換
            var convertFilePath = FFmpegUtil.ConvertAudioRate2(inputFilePath, "8000");

            using (System.IO.FileStream outputFileStream =
                new System.IO.FileStream(
                    convertFilePath, FileMode.Open))
            {
                byte[] audioByteArray = new byte[outputFileStream.Length];
                var httpClient = new HttpClient();
                var mediaType = new MediaTypeWithQualityHeaderValue("audio/wav");
                var parameter = new NameValueHeaderValue("rate", "8000");
                mediaType.Parameters.Add(parameter);

                var url = "http://vop.baidu.com/server_api?lan=zh";
                url += "&cuid=u7CHooimP8rCsOlzNzW50C66";
                url += "&token=24.d7ca91217c8dcd828bd374b03d40b799.2592000.1438064825.282335-6310719";
                url += "&format=wav";
                url += "&channel=1";
                var uri = new Uri(url);

                using (MemoryStream ms = new MemoryStream(audioByteArray, 0, audioByteArray.Length))
                {
                    var param = new StreamContent(outputFileStream);
                    param.Headers.ContentType = mediaType;

                    var result = await httpClient.PostAsync(uri, param);

                    var responseFromServer = await result.Content.ReadAsStringAsync();
                    var responseJson =
                        await Task.Factory.StartNew(
                                () =>
                                    JsonConvert
                                        .DeserializeObject<BaiduSpeechAPIResponseModel>(responseFromServer));
                    var res = responseJson.result[0];
                    return res.Substring(0, res.Length - 1);
                }
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


        public static async Task<string> RequestVoiceTextAPI(string voiceText)
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