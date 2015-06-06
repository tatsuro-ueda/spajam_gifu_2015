using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models
{
    /// <summary>
    /// 音声解説登録のリクエストモデル
    /// </summary>
    public class AudioCommentariesRequestModel
    {
        /// <summary>
        /// 音声解説のタイトル
        /// </summary>
        public string AudioCommentaryTitle { get; set; }

        /// <summary>
        /// スポットマスターのキー
        /// </summary>
        public string SpotKey { get; set; }
        
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string RegisteredUserID { get; set; }
        
        /// <summary>
        /// 音声解説データ(Base64)
        /// </summary>
        public string AudioBase64 { get; set; }
    }
}