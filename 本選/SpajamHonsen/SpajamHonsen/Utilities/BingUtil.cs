using SpajamHonsen.AzureMarketplace;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace SpajamHonsen.Utilities
{
    /// <summary>
    /// BingoAPIのユーティリティークラス
    /// </summary>
    /// <remarks>
    /// BingoAPIのユーティリティークラス
    /// </remarks>
    public class BingUtil
    {
        #region Fields
        private string accountKey = "";
        #endregion Fields

        #region Consraters
        public BingUtil() 
        { 
            var appSettings = ConfigurationManager.AppSettings;
            accountKey = appSettings["AzureMarketPlaceAccountKey"];
        }
        #endregion Consraters

        #region Methods
        /// <summary>
        /// MicrosoftBingVoiceRecognitionAPIにリクエスト送信
        /// </summary>
        /// <param name="byteArray">音声ファイルのByte配列</param>
        /// <returns></returns>
        public static async Task<string> RequestMicrosoftBingVoiceRecognitionAPIAsync(byte[] byteArray)
        {
            var httpClient = new HttpClient();

            //content-type指定
            var mediaType = new MediaTypeWithQualityHeaderValue("audio/wav");
            var parame1 = new NameValueHeaderValue("samplerate", "16000");
            var parame2 = new NameValueHeaderValue("sourcerate", "8000");
            var parame3 = new NameValueHeaderValue("trustsourcerate", "false");
            mediaType.Parameters.Add(parame1);
            mediaType.Parameters.Add(parame2);
            mediaType.Parameters.Add(parame3);

            httpClient.DefaultRequestHeaders.Accept.Add(mediaType);

            var builder = new UriBuilder("https://speech.platform.bing.com/recognize");
            builder.Port = -1;

            // QueryStringの設定
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["Version"] = "3.0";
            query["requestid"] = Guid.NewGuid().ToString();
            query["appID"] = "D4D52672-91D7-4C74-8AD8-42B1D98141A5";
            query["format"] = "json";
            query["locale"] = "ja-JP";
            query["device.os"] = "Windows OS";
            query["scenarios"] = "ulm";
            query["instanceid"] = Guid.NewGuid().ToString(); ;

            builder.Query = query.ToString();

            string url = builder.ToString();
            var uri = new Uri(url);

            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                // TODO response403 何らかの認証が必要？
                var result = await httpClient.PostAsync(uri, param);

                return await result.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// MicrosoftTranslatorAPIにリクエスト送信
        /// </summary>
        /// <remarks>
        /// MicrosoftTranslatorAPIにリクエスト送信して翻訳結果を取得する
        /// </remarks>
        /// <param name="text">翻訳対象文字列</param>
        /// <param name="from">翻訳対象言語</param>
        /// <param name="to">翻訳結果言語</param>
        /// <returns>翻訳結果</returns>
        public static async Task<string> RequestMicrosoftTranslatorAPIAsync(string text, string from, string to)
        {
            string url = "http://api.microsofttranslator.com/v2/Http.svc/Translate?&text=" +
                System.Web.HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to + "&contentType=text%2fplain";

            HttpClient client = new HttpClient(new AccessTokenMessageHandler(new HttpClientHandler()));

            var result = await client.GetAsync(url);

            string translation;
            using (Stream stream = await result.Content.ReadAsStreamAsync())
            {
                System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                translation = (string)dcs.ReadObject(stream);
            }

            return translation;
        }

        /// <summary>
        /// BingSearchAPIにリクエスト送信
        /// </summary>
        /// <remarks>
        /// BingSearchAPIにリクエスト送信して検索結果を取得する
        /// </remarks>
        /// <param name="keyword">検索キーワード</param>
        /// <returns>検索結果</returns>
        public async Task<SpajamHonsen.Models.JsonResponse.BingSearchAPIResponseModel.feed> RequestBingSearchAPIAsync(string keyword)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["Query"] = "'" + HttpUtility.UrlEncode(keyword) + "'";
            queryString["Sources"] = "'web+image+video+news+spell'";

            HttpClientHandler handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential(accountKey, accountKey);

            string url = "https://api.datamarket.azure.com/Bing/Search/v1/Composite?" + queryString;

            HttpClient client = new HttpClient(handler);

            var resultStr = await client.GetStringAsync(url);

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SpajamHonsen.Models.JsonResponse.BingSearchAPIResponseModel.feed));
            XmlTextReader reader = new XmlTextReader(new StringReader(resultStr));
            var responseModel = (SpajamHonsen.Models.JsonResponse.BingSearchAPIResponseModel.feed)serializer.Deserialize(reader);
            return responseModel;
        }

        /// <summary>
        /// BingSynonymAPIにリクエスト送信
        /// </summary>
        /// <remarks>
        /// BingSynonymAPIにリクエスト送信して類義語を取得する
        /// </remarks>
        /// <param name="keyword">類義語取得キーワード</param>
        /// <returns>類義語のリスト</returns>
        public async Task<SpajamHonsen.Models.JsonResponse.BingSynonymAPIResponseModel.feed> RequestBingSynonymAPIAsync(string keyword)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["Query"] = "'" + HttpUtility.UrlEncode(keyword) + "'";

            string url = "https://api.datamarket.azure.com/Bing/Synonyms/v1/GetSynonyms?" + queryString;

            HttpClientHandler handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential(accountKey, accountKey);

            HttpClient client = new HttpClient(handler);

            var resultStr = await client.GetStringAsync(url);

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SpajamHonsen.Models.JsonResponse.BingSynonymAPIResponseModel.feed));
            XmlTextReader reader = new XmlTextReader(new StringReader(resultStr));
            var responseModel = (SpajamHonsen.Models.JsonResponse.BingSynonymAPIResponseModel.feed)serializer.Deserialize(reader);
            return responseModel;
        }

        /// <summary>
        /// BingVoiceOutputAPIにリクエスト送信
        /// </summary>
        /// <remarks>
        /// BingVoiceOutputAPIにリクエスト送信して音声を取得する
        /// </remarks>
        /// <param name="text">音声合成対象文字列</param>
        /// <returns>音声合成結果</returns>
        public async Task<string> RequestBingVoiceOutputAsync(string text)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["locale"] = "'en-US'";
            queryString["text"] = "'" + HttpUtility.UrlEncode(text) + "'";

            var param = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "locale", "'" + HttpUtility.UrlEncode("en-US") + "'"},
                { "text", "'" + HttpUtility.UrlEncode(text) + "'" },
            });

            string url = "https://api.datamarket.azure.com/data.ashx/Bing/speechoutput/v2/BingSpeechToText?" + queryString; ;

            HttpClientHandler handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential(accountKey, accountKey);

            HttpClient client = new HttpClient(handler);

            var result = await client.PostAsync(url, null);

            var resultStr = await result.Content.ReadAsStringAsync();

            return resultStr;
        }

        #endregion Methods
    }
}