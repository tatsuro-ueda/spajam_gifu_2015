using SpajamHonsen.Models.JsonResponse;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace SpajamHonsen.Utilities
{
    /// <summary>
    /// YahooAPIのユーティリティークラス
    /// </summary>
    /// <remarks>
    /// YahooAPIのユーティリティークラス
    /// </remarks>
    public class YahooUtil
    {
        #region Fields
        private string appID = "";
        #endregion Fields

        #region Consraters
        public YahooUtil() 
        { 
            var appSettings = ConfigurationManager.AppSettings;
            appID = appSettings["YahooAppID"];
        }
        #endregion Consraters

        /// <summary>
        /// YahooQuestionSearchAPIにリクエスト送信
        /// </summary>
        /// <param name="keyword">検索キーワード</param>
        /// <returns>レスポンスモデル</returns>
        public async Task<SpajamHonsen.Models.JsonResponse.YahooQuestionSearchAPIResponseModel.ResultSet> RequestYahooQuestionSearchAPIAsync(string keyword)
        {

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["appid"] = appID;
            queryString["query"] = HttpUtility.UrlEncode(keyword);

            var url = "http://chiebukuro.yahooapis.jp/Chiebukuro/V1/questionSearch?" + queryString;

            var httpClient = new HttpClient();

            var result = await httpClient.GetAsync(url);

            var responseString = await result.Content.ReadAsStringAsync();

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SpajamHonsen.Models.JsonResponse.YahooQuestionSearchAPIResponseModel.ResultSet));
            XmlTextReader reader = new XmlTextReader(new StringReader(responseString));
            var responseModel = (SpajamHonsen.Models.JsonResponse.YahooQuestionSearchAPIResponseModel.ResultSet)serializer.Deserialize(reader);
            return responseModel;
        }

    }
}