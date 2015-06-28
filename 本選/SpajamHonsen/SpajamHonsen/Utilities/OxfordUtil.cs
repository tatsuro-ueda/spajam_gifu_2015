using Newtonsoft.Json;
using SpajamHonsen.Models.JsonRequest;
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
        
        #region Enums
        public enum OxfordAPIType
        {
            Vision = 1,
            Faces = 2,
        }
        #endregion Enums

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
        /// <param name="imageUrl">解析画像のURL</param>
        /// <param name="width">生成するサムネイルの幅</param>
        /// <param name="height">生成するサムネイルの高さ</param>
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

        #region FaceAPI
        /// <summary>
        /// FaceAPI(顔認識)の実行
        /// </summary>
        /// <param name="imageUrl">解析画像のURL</param>
        /// <param name="analyzesFaceLandmarks">顔認識</param>
        /// <param name="analyzesAge">年齢</param>
        /// <param name="analyzesGender">性別</param>
        /// <param name="analyzesHeadPose">顔の向き</param>
        /// <returns></returns>
        public async Task<FaceAPIDetectionResponseModel[]> DetectionAsync(string imageUrl, bool analyzesFaceLandmarks, bool analyzesAge, bool analyzesGender, bool analyzesHeadPose)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["analyzesFaceLandmarks"] = analyzesFaceLandmarks.ToString();
            queryString["analyzesAge"] = analyzesAge.ToString();
            queryString["analyzesGender"] = analyzesGender.ToString();
            queryString["analyzesHeadPose"] = analyzesHeadPose.ToString();
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/face/v0/detections?" + queryString;

            HttpClient httpClient = new HttpClient();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            byte[] byteArray = Encoding.UTF8.GetBytes(imageUrl);

            string responseString = string.Empty;
            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                var result = await httpClient.PostAsync(uri, param);

                responseString = await result.Content.ReadAsStringAsync();
            }
            var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<FaceAPIDetectionResponseModel[]>(responseString));
            return responseJson;
        }

        /// <summary>
        /// FaceAPIVerification(検証(同一人物かの確認))の実行
        /// </summary>
        /// <param name="faceId1">比較対象の顔ID1</param>
        /// <param name="faceId2">比較対象の顔ID2</param>
        /// <returns>レスポンスモデル</returns>
        public async Task<FaceAPIVerificationResponseModel> VerificationAsync(string faceId1, string faceId2)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/face/v0/verifications?" + queryString;

            HttpClient httpClient = new HttpClient();

            var request = "{\"faceId1\":\"" + faceId1 + "\","  + "\"faceId2\":\"" + faceId2 + "\"}";

            HttpContent param = new StringContent(request, System.Text.Encoding.UTF8, "application/json");
                  
            var response = await httpClient.PostAsync(uri, param);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<FaceAPIVerificationResponseModel>(responseString));
            return responseJson;
        }

        /// <summary>
        /// FaceAPIFindSimilarFaces
        /// 同様の顔を探す　（候補者のリストから似ている人を検出）
        /// の実行
        /// </summary>
        /// <param name="request">リクエストモデル</param>
        /// <returns>レスポンスモデル</returns>
        public async Task<FaceAPIFindSimilarFacesResponseModel[]> FindSimilarFacesAsync(FaceAPIFindSimilarFacesRequestModel request)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/face/v0/findsimilars?" + queryString;

            HttpClient httpClient = new HttpClient();

            var requestString = await JsonConvert.SerializeObjectAsync(request);

            HttpContent param = new StringContent(requestString, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, param);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<FaceAPIFindSimilarFacesResponseModel[]>(responseString));
            return responseJson;
        }

        /// <summary>
        /// FaceAPIGrouping
        /// グループ化　（所属グループを返す）
        /// の実行
        /// </summary>
        /// <param name="request">リクエストモデル</param>
        /// <returns>レスポンスモデル</returns>
        public async Task<FaceAPIGroupingResponseModel> GroupingAsync(FaceAPIGroupingRequestModel request)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/face/v0/groupings?" + queryString;

            HttpClient httpClient = new HttpClient();

            var requestString = await JsonConvert.SerializeObjectAsync(request);

            HttpContent param = new StringContent(requestString, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, param);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<FaceAPIGroupingResponseModel>(responseString));
            return responseJson;
        }

        /// <summary>
        /// FaceAPIIdentification
        /// 識別　(グループから似ている人を検出)
        /// の実行
        /// </summary>
        /// <param name="request">リクエストモデル</param>
        /// <returns>レスポンスモデル</returns>
        public async Task<FaceAPIIdentificationResponseModel> IdentificationAsync(FaceAPIIdentificationRequestModel request)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["subscription-key"] = subscriptionKey;

            var uri = "https://api.projectoxford.ai/face/v0/identifications?" + queryString;

            HttpClient httpClient = new HttpClient();

            var requestString = await JsonConvert.SerializeObjectAsync(request);

            HttpContent param = new StringContent(requestString, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, param);

            var responseString = await response.Content.ReadAsStringAsync();

            var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<FaceAPIIdentificationResponseModel>(responseString));
            return responseJson;
        }
        #endregion FaceAPI

        private void CustomImage() 
        {
            /*
            int i = 0;
            float sum = 0;
            float[] angle = new float[5];

            // 550×500ピクセルのキャンバスを生成したうえで、
            // 背景色を白でクリア
            Bitmap bImage =
              new Bitmap(550, 500, PixelFormat.Format64bppPArgb);
            Graphics graph = Graphics.FromImage(bImage);
            graph.Clear(Color.White);

            // 画像生成に使用するブラシ（SolidBrushコントロール）を定義
            SolidBrush[] brush = new SolidBrush[5];
            brush[0] = new SolidBrush(Color.Lime);
            brush[1] = new SolidBrush(Color.Blue);
            brush[2] = new SolidBrush(Color.DarkGreen);
            brush[3] = new SolidBrush(Color.Red);
            brush[4] = new SolidBrush(Color.Silver);

            // リクエストURLからベース名のみを取得し、
            // 「<ベース名>.dat」をデータ・ファイルとして読み込む
            String basePath =
              Path.GetFileNameWithoutExtension(context.Request.FilePath);
            StreamReader sReader = new StreamReader(
              context.Server.MapPath(basePath + ".dat"),
              Encoding.GetEncoding("Shift_JIS"));

            // 読み込んだデータから数値合計（sum）と
            //個々の値（angle）をセット
            // また、「■ <項目名>」の形式で凡例を出力
            while (sReader.Peek() > -1)
            {
                String[] line = sReader.ReadLine().Split('\t');
                sum += Single.Parse(line[1]);
                angle[i] = Single.Parse(line[1]);
                graph.DrawString("■ " + line[0],
                  new Font("HG丸ｺﾞｼｯｸM-PRO", 16),
                  brush[i], new PointF(380, 20 + i * 25));
                i++;
            }
            sReader.Close();

            // 個々の値（angle）と合計値（sum）から円グラフの角度を計算
            for (i = 0; i < angle.Length; i++)
            {
                angle[i] = (int)(angle[i] * 360 / sum);
            }
            float start = -90;
            // FillPieメソッドで個々のデータに基づき、円弧を描画
            for (i = 0; i < angle.Length; i++)
            {
                graph.FillPie(brush[i], 10, 10, 400, 400, start, angle[i]);
                start += angle[i];
            }
            // コンテンツ・タイプを設定したうえで生成された画像を出力
            context.Response.ContentType = "image/jpg";
            bImage.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        
           */
        }
    }
}