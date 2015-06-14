using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonResponse
{
    /// <summary>
    /// FaceAPIVerificationのResponseModel
    /// </summary>
    public class FaceAPIVerificationResponseModel
    {
        public bool isIdentical { get; set; }
        public float confidence { get; set; }
    }
}