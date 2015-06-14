using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonResponse
{
    /// <summary>
    /// FaceAPIGroupingのResponseModel
    /// </summary>

    public class FaceAPIGroupingResponseModel
    {
        public string[][] groups { get; set; }
        public string[] messyGroup { get; set; }
    }
}