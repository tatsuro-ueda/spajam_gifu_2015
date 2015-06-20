using SpajamHonsen.Models;
using SpajamHonsen.Models.JsonRequest;
using SpajamHonsen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SpajamHonsen.Controllers
{
     /// <summary>
     /// WEBAPIテスト用のコントローラー
     /// </summary>
    public class APITestController : ApiController
    {
        #region GET: api/APITest
        /* 完了 MicrosoftTranslatorAPI(翻訳)
        /// <summary>
        /// リクエストテキストをMicrosoftTranslatorAPIで翻訳して返す
        /// </summary>
        /// <param name="text">翻訳対象テキスト</param>
        /// <param name="from">翻訳元言語</param>
        /// <param name="to">翻訳結果言語</param>
        /// <returns>翻訳結果</returns>
        public async Task<string> GetMicrosoftTranslatorAPIAsync(string text, string from, string to)
        {
            var result = await BingUtil.RequestMicrosoftTranslatorAPIAsync(text, from, to);
            return result;
        }
        */

        // 未完了　BingSynonimsAPI(類義語)
        /// <summary>
        /// BingSynonimsAPIでリクエストテキストの類義語を返す
        /// </summary>
        /// <param name="synonim">類義語取得対象キーワード</param>
        /// <returns>翻訳結果</returns>
        public async Task<string> GetBingSynonimsAPIAsync(string keyword)
        {
            var bingUtil = new BingUtil();
            var result = await bingUtil.RequestBingSynonymAPIAsync(keyword);
            return result;
        }

        /* 完了　OxfordVisoinAPI(画像解析、文字認識、サムネイル作成)
        /// <summary>
        /// OxfordVisoinAPIのテスト
        /// </summary>
        /// <returns>翻訳結果</returns>
        public async Task<string> GetOxfordVisoinAPIAsync()
        {
            string analyzeImageUrl = @"{'Url':'https://spajamhonsenstorage.blob.core.windows.net/visions/visionsample.jpg'}";
            string ocrImageUrl = @"{'Url':'https://spajamhonsenstorage.blob.core.windows.net/visions/ocrsample.jpg'}";
            
            /*
            // 画像ファイルのアップロード
            byte[] byteArray = System.Convert.FromBase64String(request.AudioBase64);
            var azureStorageUtil = new AzureStorageUtil();
            var fileName = Guid.NewGuid().ToString();
            await azureStorageUtil.UploadBlobStrage(byteArray, fileName, "visions");

            // 画像のURLを取得
            var imageUrl = azureStorageUtil.GetBlobStrageUrl(fileName, "visions");
            */

            /*
              var oxfordUtil = new OxfordUtil(OxfordAPIType.Vision);
              // var result = await oxfordUtil.AnalyzeAnImageAsync(analyzeImageUrl);
              // var result = await oxfordUtil.OCRApiAsync(ocrImageUrl, "en", true);
              var result = await oxfordUtil.GenerateThumbnailAsync(ocrImageUrl, 100, 100);
            
              return result.ToString();
         }
         */

        /* 完了　OxfordFaceAPI(顔認識)
        /// <summary>
        /// OxfordFaceAPIのテスト
        /// </summary>
        /// <returns>翻訳結果</returns>
        public async Task<string> GetOxfordFaceAPIAsync()
        {
            string familyImageUrl = @"{'Url':'https://spajamhonsenstorage.blob.core.windows.net/visions/visionsample.jpg'}";
            
            var oxfordUtil = new OxfordUtil(SpajamHonsen.Utilities.OxfordUtil.OxfordAPIType.Faces);
            // var result = await oxfordUtil.DetectionAsync(familyImageUrl, true, true, true, true);
            // var result = await oxfordUtil.VerificationAsync("f0773255-b1ba-4bfd-b9a1-3463a2abeca8", "2c7c7fdf-601f-4532-ad56-806b8694988a");
            
            /*
            // リクエストの作成
            var request = new FaceAPIFindSimilarFacesRequestModel();
            request.faceId = "f0773255-b1ba-4bfd-b9a1-3463a2abeca8";
            request.faceIds = new String[] { "f0773255-b1ba-4bfd-b9a1-3463a2abeca8", "2c7c7fdf-601f-4532-ad56-806b8694988a", "f0773255-b1ba-4bfd-b9a1-3463a2abeca8", "2c7c7fdf-601f-4532-ad56-806b8694988a" };

            var result = await oxfordUtil.FindSimilarFacesAsync(request);
            */
            /*
            // リクエストの作成
            var request = new FaceAPIGroupingRequestModel();
            request.faceIds = new String[] { "f0773255-b1ba-4bfd-b9a1-3463a2abeca8", "2c7c7fdf-601f-4532-ad56-806b8694988a", "f0773255-b1ba-4bfd-b9a1-3463a2abeca8", "2c7c7fdf-601f-4532-ad56-806b8694988a" };
            
            var result = await oxfordUtil.GroupingAsync(request);
            */

            /*
            // リクエストの作成
            var request = new FaceAPIIdentificationRequestModel();
            request.faceIds = new String[] { "f0773255-b1ba-4bfd-b9a1-3463a2abeca8", "2c7c7fdf-601f-4532-ad56-806b8694988a", "f0773255-b1ba-4bfd-b9a1-3463a2abeca8", "2c7c7fdf-601f-4532-ad56-806b8694988a" };
            request.personGroupId = "f0773255-b1ba-4bfd-b9a1-3463a2abeca8";
            request.maxNumOfCandidatesReturned = 5;

            var result = await oxfordUtil.IdentificationAsync(request);
            
            return result.ToString();
         }
         */

        /* 完了　YahooQuestionSearchAPI(Yahoo知恵袋　質問検索API)
        /// <summary>
        /// YahooQuestionSearchAPIでYahoo知恵袋の検索結果を返す
        /// </summary>
        /// <param name="keyword">検索キーワード</param>
        /// <returns>検索結果</returns>
        public async Task<string> GetYahooQuestionSearchAPIAsync(string keyword)
        {
            var yahooUtil = new YahooUtil();
            var result = await yahooUtil.RequestYahooQuestionSearchAPIAsync(keyword);
            return result.ToString();
        }
        */

        /* 完了　BingSearchAPI(Bing検索API)
        /// <summary>
        /// BingSearchAPIでBingの検索結果を返す
        /// </summary>
        /// <param name="keyword">検索キーワード</param>
        /// <returns>検索結果</returns>
        public async Task<string> GetBingSearchAPIAsync(string keyword)
        {
            var bingUtil = new BingUtil();
            var result = await bingUtil.RequestBingSearchAPIAsync(keyword);
            return result.ToString();
        }
        */
        
        #endregion GET: api/APITest

        #region POST: api/APITest
        /* 完了 FFmpeg(レート変換)
        /// <summary>
        /// Base64形式で音声ファイル(content-type:x-wav rete:44100)をPOSTして
        /// 音声ファイル(content-type:x-flac rete:16000)に変換してファイルパスを返す
        /// </summary>
        /// <param name="request">音声ファイルのBase64文字列</param>
        /// <returns>変換後のファイルのパス</returns>
        public async Task<string> PostConvertAudioRateAsync(TestRequestModel request)
        {
            byte[] byteArray = System.Convert.FromBase64String(request.Base64String);

            var filePath = HttpContext.Current.Server.MapPath("~/ffmpeg/" + Guid.NewGuid().ToString());
            //ファイルを作成して書き込む
            using (System.IO.FileStream fs = 
                new System.IO.FileStream(
                    filePath,
                    System.IO.FileMode.Create,
                    System.IO.FileAccess.Write))
           {
                //バイト型配列の内容をすべて書き込む
                fs.Write(byteArray, 0, byteArray.Length);
           }

            var result = FFmpegUtil.ConvertAudioRate(filePath, "16000");

            return result;
        }
        */

        /* 完了 FFmpeg(形式変換)
        /// <summary>
        /// Base64形式で音声ファイル(content-type:x-wav rete:44100)をPOSTして
        /// MP3形式に変換してファイルパスを返す
        /// </summary>
        /// <param name="request">音声ファイルのBase64文字列</param>
        /// <returns>変換後のファイルのパス</returns>
        public async Task<string> PostConvertAudioFormatAsync(TestRequestModel request)
        {
            byte[] byteArray = System.Convert.FromBase64String(request.Base64String);

            var filePath = HttpContext.Current.Server.MapPath("~/ffmpeg/" + Guid.NewGuid().ToString());
            //ファイルを作成して書き込む
            using (System.IO.FileStream fs = 
                new System.IO.FileStream(
                    filePath,
                    System.IO.FileMode.Create,
                    System.IO.FileAccess.Write))
           {
                //バイト型配列の内容をすべて書き込む
                fs.Write(byteArray, 0, byteArray.Length);
           }

            var result = FFmpegUtil.ConvertAudioFormat(filePath, "mp3");

            return result;
        }
        */

        /* 完了 GoogleSpeechAPI(音声解析)
        /// <summary>
        /// Base64形式で音声ファイル(content-type:x-flac rete:16000)をPOSTしてGoogleSpeechAPIで音声を解析して返す
        /// </summary>
        /// <param name="request">テスト用リクエストモデル</param>
        /// <returns>音声解析結果テキスト(1番目)</returns>
        public async Task<string> PostGoogleSpeechAPIAsync(TestRequestModel request)
        {
            byte[] byteArray = System.Convert.FromBase64String(request.Base64String);
            return await GoogleUtil.RequestGoogleSpeechAPIAsync(byteArray);
        }
        */

        /* 完了 VoiceTextAPI(音声合成(日本語のみ))
        /// <summary>
        /// リクエストのテキストをVoiceTextAPIで音声合成してAzureにアップする
        /// その後アップしたURLを返却する
        /// </summary>
        /// <param name="request">音声にしたいテキスト</param>
        /// <returns>アップロードした音声ファイルのURL</returns>
        public async Task<string> PostVoiceTextAPIAsync(TestRequestModel request)
        {
            var othersUtil = new OthersUtil();
            var fileID = await othersUtil.RequestVoiceTextAPI(request.Base64String, "show");
            var azureStorageUtil = new AzureStorageUtil();
            return azureStorageUtil.GetBlobStrageUrl(fileID, "voices");
        }
        */

        /* 完了 GetGoogleJapaneseAPI(ひらがな→漢字変換)
        /// <summary>
        /// リクエストのテキストをGoogle日本語入力APIでひらがなを漢字変換して返す
        /// </summary>
        /// <param name="text">ひらがなテキスト</param>
        /// <returns>漢字変換後のテキスト</returns>
        public async Task<string> GetGoogleJapaneseAPIAsync(string text)
        {
            return await GoogleUtil.RequestGoogleJapaneseAPI(text);
        }
        */

        /* TODO 未完了　BingVoiceRecognitionAPI(音声解析)
        /// <summary>
        /// Base64形式で音声ファイル(content-type:x-flac rete:16000)をPOSTしてBingVoiceRecognitionAPIで音声を解析して返す
        /// </summary>
        /// <param name="request"></param>
        /// <returns>音声解析結果テキスト</returns>
        public async Task<string> PostMicrosoftBingVoiceRecognitionAPIAsync(TestRequestModel request)
        {
            byte[] byteArray = System.Convert.FromBase64String(request.Base64String);
            return await BingUtil.RequestMicrosoftBingVoiceRecognitionAPIAsync(byteArray);
        }
        */
        #endregion POST: api/APITest
    }
}