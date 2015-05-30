using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SpajamAPI.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace SpajamAPI.Controllers
{
    public class DownLoadController : ApiController
    {
        // private const string SPEECH_TEXT = "関ヶ原の戦いは知っとるじゃろ？お昼を過ぎてから、東軍の家康が総がかりで西軍を押し始め、西軍の総大将石田三成は逃走したんじゃ。ところが途中で捕まってしまって、三条河原でさらし首になったのじゃが、その捕まった場所がこの坂なんじゃ。それ以来、毎年9月15日になるとむせび泣きが聞こえるようになってしまってのお。";
        private const string SPEECH_TEXT = "あーあーまいくのてすとちゅうー";

        private SpajamMadobenDBEntities db = new SpajamMadobenDBEntities();

        // GET: api/DownLoad/5
        public async Task<DownLoadResponseModel> GetAudioCommentary(string id)
        {
            AudioCommentary audioCommentary = db.AudioCommentary.Where(master => master.SpotKey == id).First();
            if (audioCommentary == null)
            {
                return null;
            }

            var appSettings = ConfigurationManager.AppSettings;
            var accountKey = appSettings["CloudStorageAccount"];

            var url = DownloadBlobStrage(accountKey, audioCommentary.FileID);

            var response = new DownLoadResponseModel() 
            {
                AudioCommentary = audioCommentary,
                Base64Audio = url,
            };

            return response;
        }

        /// <summary>
        /// AzureBlobStrageからファイルをダウンロードする
        /// </summary>
        /// <param name="accountKey">AzureStorageのアカウントキー</param>
        /// <param name="fileName">ファイル名</param>
        /// <returns></returns>
        private static string DownloadBlobStrage(string accountKey, string fileName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // コンテナを作成
            CloudBlobContainer container = blobClient.GetContainerReference("audios");

            var blockBlob = container.GetBlockBlobReference(fileName + ".wav");

            var url = blockBlob.Uri.ToString();

            return url;
            /*
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Position = 0;
                await blockBlob.DownloadToStreamAsync(ms);
                var byteArray = ms.ToArray();
                return System.Convert.ToBase64String(byteArray); ;
            }
            */
        }
    }
}