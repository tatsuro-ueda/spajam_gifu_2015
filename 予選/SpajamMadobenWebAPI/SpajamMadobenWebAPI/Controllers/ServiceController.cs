using SpajamMadobenWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SpajamMadobenWebAPI.Controllers
{
    /// <summary>
    /// Azureを利用したWEBAPIのサンプル
    /// </summary>
    public class ServiceController : ApiController
    {
        /// <summary>
        /// サーバー上の音声ファイルをAzureのBLOBストレージにアップロードする
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns></returns>
        public HttpResponseMessage GetAudio(String fileName) 
        {
            // アカウントを取得
            var appSettings = ConfigurationManager.AppSettings;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(appSettings["CloudStorageAccount"]);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            
            // コンテナを作成
            CloudBlobContainer container = blobClient.GetContainerReference("audios");

            container.CreateIfNotExists();

            // Blobを作成
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("test" + fileName + ".flac");

            // Blobにアップロードする
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + fileName + ".flac");

            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                blockBlob.UploadFromStream(fileStream);
            } 

            return null;
        }

        // POST: api/Service
        /// <summary>
        /// base64文字列で受け取った音声ファイルをBLOBストレージに保存し、GoogleSpeechAPIで解析する
        /// </summary>
        /// <returns></returns>
        public async Task<string> PostBase64AudioAsync()
        {
            // リクエスト内容を取得
            var base64 = await Request.Content.ReadAsStringAsync();
            
            //バイト型配列に戻す
            byte[] byteArray = System.Convert.FromBase64String(base64);

            // AzureBLOBStrageに保存
            var fileName = Guid.NewGuid().ToString();

            // アカウントを取得
            var appSettings = ConfigurationManager.AppSettings;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(appSettings["CloudStorageAccount"]);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // コンテナを作成
            CloudBlobContainer container = blobClient.GetContainerReference("audios");

            container.CreateIfNotExists();

            // Blobを作成
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName + ".flac");

            // byte配列をMemoryStreamに変換
            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                // Blobにアップロードする
                await blockBlob.UploadFromStreamAsync(ms);
            }

            // GoogleSpeechAPIに送信
            var key = appSettings["GoogleSpeechAPIKey"];
            var url = "https://www.google.com/speech-api/v2/recognize?output=json&lang=ja-jp&key=";
            var postUrl = url + key;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(postUrl);
            var sampleRate = 16000; 
            
            request.Method = "POST";
            request.ContentType = "audio/x-flac; rate=" + sampleRate.ToString();
            request.ContentLength = byteArray.Length;
            
            Stream sendStream = request.GetRequestStream();
            sendStream.Write(byteArray, 0, byteArray.Length);
            sendStream.Close();

            string responseFromServer;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        // GET: api/GoogleSpeechTexts/flacName
        /// <summary>
        /// サーバー上の音声ファイルをGoogleSpeechAPIで解析する
        /// </summary>
        /// <param name="flacName">ファイル名</param>
        /// <returns></returns>
        public string GetGoogleSpeechText(String flacName)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var key = appSettings["GoogleSpeechAPIKey"];
            var url = "https://www.google.com/speech-api/v2/recognize?output=json&lang=ja-jp&key=";
            var postUrl = url + key;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(postUrl);

            request.Method = "POST";
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + flacName + ".flac");
            byte[] byteArray = File.ReadAllBytes(filePath);
            var sampleRate = 16000;
            request.ContentType = "audio/x-flac; rate=" + sampleRate.ToString();
            request.ContentLength = byteArray.Length;

            Stream sendStream = request.GetRequestStream();
            sendStream.Write(byteArray, 0, byteArray.Length);
            sendStream.Close();

            string responseFromServer;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
