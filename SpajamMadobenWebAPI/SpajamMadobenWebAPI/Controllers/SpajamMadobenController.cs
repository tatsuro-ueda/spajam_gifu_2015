using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SpajamMadobenWebAPI.Models;
using System;
using System.Collections.Generic;
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
    public class SpajamMadobenController : ApiController
    {
        private SpajamMadobenDBEntities2 db = new SpajamMadobenDBEntities2();

        // POST: api/Talks
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

                //バイト型配列に戻す
                byte[] byteArray = System.Convert.FromBase64String(base64);

                // AzureBLOBStrageに保存
                var fileName = Guid.NewGuid().ToString();

                // アカウントを取得
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    @"DefaultEndpointsProtocol=https;AccountName=spajammadobenstrage;AccountKey=007q7do8gs4w3BFp3vWIGLO7XXqJKquhKaqZ9vWuAUZzawL/teMWwyNgCgTLf5X9oGVZVVpu0VXe/WbN19wgvQ==");

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
                var key = "AIzaSyBlwhF2pGCf472kxOMCGk1-4ODWtInjjGk";
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

        private bool TalkExists(string id)
        {
            return db.Talk.Count(e => e.UserID == id) > 0;
        }
    }
}
