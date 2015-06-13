using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        public string AnalyzeAnImage()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["visualFeatures"] = "All";
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/vision/v1/analyses?" + queryString;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";

            byte[] byteData = Encoding.UTF8.GetBytes(imageUrl);
            request.ContentType = @"application/json";
            request.ContentLength = byteData.Length;
            var responseString = "";
            using (var stream = request.GetRequestStream())
            {
                stream.Write(byteData, 0, byteData.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        /// <summary>
        /// OCR(画像の文字認識)
        /// </summary>
        /// <returns></returns>
        public string OCRApi()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["language"] = "en";
            queryString["detectOrientation "] = "true";

            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/vision/v1/ocr?" + queryString;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";

            byte[] byteData = Encoding.UTF8.GetBytes(imageUrl);
            request.ContentType = @"application/json";
            request.ContentLength = byteData.Length;
            var responseString = "";
            using (var stream = request.GetRequestStream())
            {
                stream.Write(byteData, 0, byteData.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";

            byte[] byteData = Encoding.UTF8.GetBytes(imageUrl);
            request.ContentType = @"application/json";
            request.ContentLength = byteData.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(byteData, 0, byteData.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var azureStorageUtil = new AzureStorageUtil();
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            await azureStorageUtil.UploadBlobStrage(response.GetResponseStream(), fileName, "visions");
            return fileName;
        }
        #endregion VisionAPI
    }
}