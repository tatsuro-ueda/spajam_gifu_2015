//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpajamAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Talk
    {
        public string UserID { get; set; }
        public string TalkID { get; set; }
        public string TalkTitle { get; set; }
        public long SortID { get; set; }
        public string Evaluation { get; set; }
        public Nullable<long> EvaluationDetail { get; set; }
    }
}
