using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonRequest
{
    /// <summary>
    /// HVCログを登録する際のRequestモデル
    /// </summary>
    public class HVCLogPostRequest
    {
            public string SpotID { get; set; }
            public string Language { get; set; }
            public int Expression { get; set; }
            public int Age { get; set; }
            public string Sex { get; set; }
    }
}