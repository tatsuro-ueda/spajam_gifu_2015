using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonRequest
{
    /// <summary>
    /// FaceAPIFindSimilarFacesのRequestModel
    /// </summary>
    public class FaceAPIFindSimilarFacesRequestModel
    {
        public string faceId { get; set; }
        public string[] faceIds { get; set; }
    }
}