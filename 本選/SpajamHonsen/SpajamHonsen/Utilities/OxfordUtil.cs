﻿using Newtonsoft.Json;
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
        #region Fields
        private string subscriptionKey = "";
        #endregion Fields

        #region Consraters
        public OxfordUtil(OxfordAPIType type) 
        { 
            var appSettings = ConfigurationManager.AppSettings;
            if (type == OxfordAPIType.Vision)
            {
                subscriptionKey = appSettings["VisionAPIKey"];
            }
            else if (type == OxfordAPIType.Faces)
            {
                subscriptionKey = appSettings["FacesAPIKey"];
            }
        }
        #endregion Consraters

        public enum OxfordAPIType
        {
            Vision = 1,
            Faces = 2,
        }
        #region VisionAPI
        /// <summary>
        /// VisionAPIによる画像解析を行う
        /// </summary>
        /// <param name="imageUrl">解析対象の画像URL</param>
        /// <returns>VisionAPI(画像解析)のResponceクラス</returns>
        public async Task<VisionAPIAnalyzeanImageResponseModel> AnalyzeAnImageAsync(string imageUrl)
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
        /// VisionAPIによるOCR(画像の文字認識)を行う
        /// </summary>
        /// <param name="imageUrl">解析画像のURL</param>
        /// <param name="language">言語(jp/cn/en)</param>
        /// <param name="detectOrientation">画像のむき true/false</param>
        /// <returns>VisionAPI(OCR)のResponceクラス</returns>
        public async Task<VisionAPIOCRResponseModel> OCRApiAsync(string imageUrl, string language, bool detectOrientation)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["language"] = language;
            queryString["detectOrientation "] = detectOrientation.ToString();
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
        /// VisionAPIによるサムネイルの作成を行う
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Azureにアップロードしたサムネイルのファイル名</returns>
        public async Task<string> GenerateThumbnailAsync(string imageUrl, double width, double height)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["width"] = width.ToString();
            queryString["height"] = height.ToString();
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

        #region FacesAPI
        /// <summary>
        /// FacesAPI(顔認識)の実行
        /// </summary>
        /// <returns></returns>
        public async Task<string> DetectionAsync()
        {
            /*
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.projectoxford.ai/face/v0/detections?subscription-key=" + subscriptionKey);
            request.Method = "POST";
            request.ContentType = "application/octet-stream";

            string imageUrl = "https://spajamhonsenstorage.blob.core.windows.net/visions/visionsample.jpg"; 
            byte[] byteArray = Encoding.UTF8.GetBytes(imageUrl);
            Stream responseStream = null;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    responseStream = response.GetResponseStream();
                }
            }  
            */

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["analyzesFaceLandmarks"] = "true";
            queryString["analyzesAge"] = "true";
            queryString["analyzesGender"] = "true";
            queryString["analyzesHeadPose"] = "true";
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/face/v0/detections?" + queryString;

            HttpClient httpClient = new HttpClient();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            string imageUrl = @"{'Url':'https://spajamhonsenstorage.blob.core.windows.net/visions/visionsample.jpg'}";
            byte[] byteArray = Encoding.UTF8.GetBytes(imageUrl);

            string responseString = string.Empty;
            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                var result = await httpClient.PostAsync(uri, param);

                responseString = await result.Content.ReadAsStringAsync();
            }
            return responseString;
        }
        #endregion FacesAPI
    }
}