using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonRequest
{
    /// <summary>
    /// ツイート内容の登録
    /// </summary>
    public class TweetPostRequest
    {
        /// <summary>
        /// 音声ファイルのbase64文字列
        /// </summary>
        public string base64Audio { get; set; }
        
        /// <summary>
        /// HVCログの登録
        /// </summary>
        public HVCLogPostRequest hVCLogPostRequest { get; set; }
    }
}