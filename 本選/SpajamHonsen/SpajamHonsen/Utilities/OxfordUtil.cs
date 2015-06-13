using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
        #region VisionAPI
        // TODO Key
        public static string subscriptionKey = @"43d9ba4539fc424db63371ee47e2aa5d";

        public static string imageUrl = @"{'Url':'http://www.ytravelblog.com/wp-content/uploads/2010/10/Vastness-of-the-Salar-de-Uyuni1.jpg'}";
        public static string localpath = @"C:\Users\XXXX\Pictures\Vastness-of-the-Salar-de-Uyuni1.jpg";
        public static string thumbnailSavePath = @"C:\Vision\";
        static void Main(string[] args)
        {
            // Image Uri
            AnalyzeAnImage(imageUrl, null);
            GenerateThumbnail(imageUrl, null);
            OCRApi(imageUrl, null);
            Console.ReadLine();

            // local Image
            AnalyzeAnImage(null, localpath);
            GenerateThumbnail(null, localpath);
            OCRApi(null, localpath);
            Console.ReadLine();
        }

        public static void AnalyzeAnImage(string imageUrl, string localPath)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Specify values for optional parameters, as needed
            queryString["visualFeatures"] = "All";

            // Specify your subscription key
            queryString["subscription-key"] = subscriptionKey;

            // Specify values for path parameters (shown as {...})
            var uri = "https://api.projectoxford.ai/vision/v1/analyses?" + queryString;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";

            // Specify request body
            if (imageUrl != null)
            {
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

                Console.WriteLine(responseString);
            }

            if (localPath != null)
            {
                request.ContentType = "application/octet-stream";
                byte[] img = File.ReadAllBytes(localpath);
                request.ContentLength = img.Length;
                var responseString = "";
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(img, 0, img.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Console.WriteLine(responseString);
            }
        }

        public static void OCRApi(string imageUrl, string localPath)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Specify values for optional parameters, as needed
            queryString["language"] = "en";
            queryString["detectOrientation "] = "true";

            // Specify your subscription key
            queryString["subscription-key"] = subscriptionKey;

            // Specify values for path parameters (shown as {...})
            var uri = "https://api.projectoxford.ai/vision/v1/ocr?" + queryString;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";

            // Specify request body
            if (imageUrl != null)
            {
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

                Console.WriteLine(responseString);
            }

            if (localPath != null)
            {
                request.ContentType = "application/octet-stream";
                byte[] img = File.ReadAllBytes(localpath);
                request.ContentLength = img.Length;
                var responseString = "";
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(img, 0, img.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Console.WriteLine(responseString);
            }
        }

        public static void GenerateThumbnail(string imageUrl, string localPath)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Specify values for the following required parameters
            queryString["width"] = "50";
            queryString["height"] = "60";

            // Specify values for optional parameters, as needed
            queryString["smartCropping"] = "true";

            // Specify your subscription key
            queryString["subscription-key"] = subscriptionKey;

            // Specify values for path parameters (shown as {...})
            var uri = "https://api.projectoxford.ai/vision/v1/thumbnails?" + queryString;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";

            // Specify request body
            if (imageUrl != null)
            {
                byte[] byteData = Encoding.UTF8.GetBytes(imageUrl);
                request.ContentType = @"application/json";
                request.ContentLength = byteData.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, byteData.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                Image image = Image.FromStream(response.GetResponseStream());
                image.Save(thumbnailSavePath + Guid.NewGuid().ToString() + @".jpg");

                Console.WriteLine("Thumbnail Width: {0}", image.Width);
                Console.WriteLine("Thumbnail Height: {0}", image.Height);
                Console.WriteLine("Thumbnail path: {0}", thumbnailSavePath + Guid.NewGuid().ToString() + @".jpg");
            }

            if (localPath != null)
            {
                request.ContentType = "application/octet-stream";
                byte[] img = File.ReadAllBytes(localpath);
                request.ContentLength = img.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(img, 0, img.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                Image image = Image.FromStream(response.GetResponseStream());
                image.Save(thumbnailSavePath + Guid.NewGuid().ToString() + @".jpg");

                Console.WriteLine("Thumbnail Width: {0}", image.Width);
                Console.WriteLine("Thumbnail Height: {0}", image.Height);
                Console.WriteLine("Thumbnail path: {0}", thumbnailSavePath + Guid.NewGuid().ToString() + @".jpg");
            }
        }
        #endregion VisionAPI
    }
}