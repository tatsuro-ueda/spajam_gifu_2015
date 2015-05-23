using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamAPI.Models
{
    /// <summary>
    /// 音声+データ返却用のモデル
    /// </summary>
    public class DownLoadResponseModel
    {
        /// <summary>
        /// AudioCommentaryクラス
        /// </summary>
        public AudioCommentary AudioCommentary { get; set; }

        /// <summary>
        /// 音声ファイル(base64)
        /// </summary>
        public string Base64Audio { get; set; }
    }
}