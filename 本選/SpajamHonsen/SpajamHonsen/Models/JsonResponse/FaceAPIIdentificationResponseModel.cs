using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonResponse
{
    /// <summary>
    /// FaceAPIIdentificationのResponseModel
    /// </summary>
    public class FaceAPIIdentificationResponseModel
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string faceId { get; set; }
        public Candidate[] candidates { get; set; }
    }

    public class Candidate
    {
        public string personId { get; set; }
        public string name { get; set; }
        public float confidence { get; set; }
    }
}