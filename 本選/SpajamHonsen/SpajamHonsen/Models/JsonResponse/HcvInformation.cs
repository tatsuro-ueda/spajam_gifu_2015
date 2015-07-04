using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonResponse
{
    public class HcvInformation
    {
        public string LogID { get; set; }
        public string SpotID { get; set; }
        public string Language { get; set; }
        public int Expression { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public System.DateTime CreateDateTime { get; set; }
    }
}