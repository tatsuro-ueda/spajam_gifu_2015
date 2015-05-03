using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamMadobenWebAPI.Models
{
    /// <summary>
    /// 音声登録用のモデル
    /// </summary>
    public class TalkModel
    {
        /// <summary>
        /// トーククラス
        /// </summary>
        public Talk Talk { get; set; }

        /// <summary>
        /// 音声ファイル(base64)
        /// </summary>
        public string Base64Audio { get; set; }
    }
}