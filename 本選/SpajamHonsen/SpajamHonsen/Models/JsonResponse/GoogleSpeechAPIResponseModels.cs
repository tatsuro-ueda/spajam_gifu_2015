﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models
{
    public class GoogleSpeechAPIResponseModel
    {
        public class Resuls
        {
            public Result[] result { get; set; }
            public int result_index { get; set; }
        }

        public class Result
        {
            public Alternative[] alternative { get; set; }
            public bool final { get; set; }
        }

        public class Alternative
        {
            public string transcript { get; set; }
            public float confidence { get; set; }
        }
    }
}