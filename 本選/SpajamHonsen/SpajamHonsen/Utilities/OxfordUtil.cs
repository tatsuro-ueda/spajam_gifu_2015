using Newtonsoft.Json;
using SpajamHonsen.Models.JsonResponse;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SpajamHonsen.Utilities
{
    /// <summary>
    /// ProjectOxfordAPIのユーティリティークラス
    /// </summary>
    /// <remarks>
    /// ProjectOxfordAPIのユーティリティークラス
    /// </remarks>
    public class OxfordUtil
    {
        #region Consraters
        public OxfordUtil() 
        { 
            var appSettings = ConfigurationManager.AppSettings;
            subscriptionKey = appSettings["VisionAPIKey"];
        }
        #endregion Consraters

        #region Fields
        private string subscriptionKey = "";
        #endregion Fields

        #region VisionAPI
        // public static string imageUrl = @"{'Url':'https://spajamhonsenstorage.blob.core.windows.net/visions/visionsample.jpg'}";
        private string imageUrl = @"{'Url':'https://spajamhonsenstorage.blob.core.windows.net/visions/ocrsample.jpg'}";

        /// <summary>
        /// 画像解析
        /// </summary>
        /// <returns></returns>
        public async Task<VisionAPIAnalyzeanImageResponseModel> AnalyzeAnImageAsync()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["visualFeatures"] = "All";
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/vision/v1/analyses?" + queryString;

            HttpClient httpClient = new HttpClient();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            byte[] byteArray = Encoding.UTF8.GetBytes(imageUrl);

            var responseString = string.Empty;
            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                var result = await httpClient.PostAsync(uri, param);

                var responseStream = await result.Content.ReadAsStreamAsync();
                responseString = new StreamReader(responseStream).ReadToEnd();
            }

            var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<VisionAPIAnalyzeanImageResponseModel>(responseString));
            return responseJson;
        }

        /// <summary>
        /// OCR(画像の文字認識)
        /// </summary>
        /// <returns></returns>
        public async Task<VisionAPIOCRResponseModel> OCRApiAsync()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["language"] = "en";
            queryString["detectOrientation "] = "true";

            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/vision/v1/ocr?" + queryString;

            HttpClient httpClient = new HttpClient();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            byte[] byteArray = Encoding.UTF8.GetBytes(imageUrl);

            var responseString = string.Empty;
            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                var result = await httpClient.PostAsync(uri, param);

                var responseStream = await result.Content.ReadAsStreamAsync();
                responseString = new StreamReader(responseStream).ReadToEnd();
            }

            var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<VisionAPIOCRResponseModel>(responseString));
            return responseJson;
        }

        /// <summary>
        /// サムネイルの作成
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public async Task<string> GenerateThumbnailAsync()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["width"] = "50";
            queryString["height"] = "60";
            queryString["smartCropping"] = "true";
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/vision/v1/thumbnails?" + queryString;

            HttpClient httpClient = new HttpClient();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            byte[] byteArray = Encoding.UTF8.GetBytes(imageUrl);

            Stream responseStream = null;
            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                var result = await httpClient.PostAsync(uri, param);

                responseStream = await result.Content.ReadAsStreamAsync();
            }
            
            // 作成したサムネイル画像をAzureにアップロードする
            var azureStorageUtil = new AzureStorageUtil();
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            await azureStorageUtil.UploadBlobStrage(responseStream, fileName, "visions");
            return fileName;
        }
        #endregion VisionAPI
    }
}