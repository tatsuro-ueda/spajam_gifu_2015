using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonRequest
{
    /// <summary>
    /// FaceAPIIdentificationのRequestModel
    /// </summary>
    public class FaceAPIIdentificationRequestModel
    {
        public string[] faceIds { get; set; }
        public string personGroupId { get; set; }
        public int maxNumOfCandidatesReturned { get; set; }
    }
}