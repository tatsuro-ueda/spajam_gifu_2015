using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SpajamMadobenWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SpajamMadobenWebAPI.Controllers
{
    /// <summary>
    /// Spajamで使用するAPI
    /// </summary>
    public class SpajamMadobenController : ApiController
    {
        private SpajamMadobenDBEntities2 db = new SpajamMadobenDBEntities2();


        // POST: api/Talks
        /// <summary>
        /// json形式でデータを受け取り、DB登録、BLOBストレージアップロード、GoogleSpeechAPIで音声解析を行う
        /// </summary>
        /// <param name="talkModel">Request用jsonモデル</param>
        /// <returns></returns>
        [ResponseType(typeof(TalkModel))]
        public async Task<string> PostTalk(TalkModel talkModel)
        {
            string responseFromServer = string.Empty;

            if (!ModelState.IsValid)
            {
                // return BadRequest(ModelState);
            }

            db.Talk.Add(talkModel.Talk);

            try
            {
                // DB登録
                await db.SaveChangesAsync();

                // リクエスト内容を取得
                var base64 = talkModel.Base64Audio;

                // AzureBLOBStrageに保存
                var appSettings = ConfigurationManager.AppSettings;
                CloudStorageAccount storageAccount = 
                    CloudStorageAccount.Parse(appSettings["CloudStorageAccount"]);
                byte[] byteArray = System.Convert.FromBase64String(base64);
                var fileName = Guid.NewGuid().ToString();
                await UploadBlobStrage(storageAccount, byteArray, fileName);

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

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (DbUpdateException)
            {
                if (TalkExists(talkModel.Talk.UserID))
                {
                    // return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return responseFromServer;
        }

        /// <summary>
        /// AzureBlobStrageにファイルをアップロードする
        /// </summary>
        /// <param name="storageAccount"></param>
        /// <param name="byteArray"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static async Task UploadBlobStrage(CloudStorageAccount storageAccount, byte[] byteArray, string fileName)
        {
            
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
        }

        private bool TalkExists(string id)
        {
            return db.Talk.Count(e => e.UserID == id) > 0;
        }
    }
}
