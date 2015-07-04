using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SpajamHonsen.Utilities
{
    /// <summary>
    /// AzureのストレージサービスのUtilityクラス
    /// </summary>
    /// <remarks>
    /// AzureのストレージサービスのUtilityクラス
    /// </remarks>
    public class AzureStorageUtil
    {
        #region Fields
        private CloudBlobClient blobClient;
        #endregion Fields

        #region Constractors
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AzureStorageUtil() 
        {
            var appSettings = ConfigurationManager.AppSettings;

            var accountKey = appSettings["CloudStorageAccount"];

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            blobClient = storageAccount.CreateCloudBlobClient();
        }
        #endregion Constractors

        #region Upload
        /// <summary>
        /// AzureBlobStrageにファイルをアップロードする
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="containerName">コンテナ名</param>
        /// <returns>Taskクラス</returns>
        public async Task UploadBlobStrage(Stream stream, string fileName, string containerName)
        {
            // コンテナを作成
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists();

            // Blobを作成
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(stream);
        }

        /// <summary>
        /// AzureBlobStrageにファイルをアップロードする
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="containerName">コンテナ名</param>
        /// <returns>Taskクラス</returns>
        public void UploadBlobStrage(string filePath, string fileName, string containerName)
        {
            // コンテナを作成
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists();

            // Blobを作成
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            blockBlob.UploadFromFile(filePath, FileMode.Open);
        }

        /// <summary>
        /// AzureBlobStrageにファイルをアップロードする
        /// </summary>
        /// <param name="byteArray">バイナリデータのbyte配列</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="containerName">コンテナ名</param>
        /// <returns>Taskクラス</returns>
        public async Task UploadBlobStrage(byte[] byteArray, string fileName, string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists();

            // Blobを作成
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // byte配列をMemoryStreamに変換
            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                // Blobにアップロードする
                await blockBlob.UploadFromStreamAsync(ms);
            }
        }

        #endregion Upload

        #region Download
        /// <summary>
        /// AzureBlobStrageから対象ファイルのURLを取得する
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="containerName">コンテナ名</param>
        /// <returns></returns>
        public string GetBlobStrageUrl(string fileName, string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            var blockBlob = container.GetBlockBlobReference(fileName);

            return blockBlob.Uri.ToString();
        }

        /// <summary>
        /// AzureBlobStrageから対象ファイルのBase64文字列を取得する
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="containerName">コンテナ名</param>
        /// <returns></returns>
        public async Task<string> GetBlobStrageBase64Async(string fileName, string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            var blockBlob = container.GetBlockBlobReference(fileName);

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Position = 0;
                await blockBlob.DownloadToStreamAsync(ms);
                var byteArray = ms.ToArray();
                return System.Convert.ToBase64String(byteArray); ;
            }
        }
        #endregion Download
    }
}