using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonRequest
{
    /// <summary>
    /// デバイストークンをバインドさせるためのクラス
    /// </summary>
    public class DeviceTokenRegistRequest
    {
        public string DeviceToken { get; set; }
    }
}