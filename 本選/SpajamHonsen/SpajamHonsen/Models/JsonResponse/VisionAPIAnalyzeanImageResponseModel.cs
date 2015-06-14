using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonResponse
{
    /// <summary>
    /// VisionAPI(画像解析)のJsonレスポンスモデル
    /// </summary>
    public class VisionAPIAnalyzeanImageResponseModel
    {
        public Category[] categories { get; set; }
        public Adult adult { get; set; }
        public string requestId { get; set; }
        public Metadata metadata { get; set; }
        public Face[] faces { get; set; }
        public Color color { get; set; }
        public Imagetype imageType { get; set; }
    }

    public class Adult
    {
        public bool isAdultContent { get; set; }
        public bool isRacyContent { get; set; }
        public float adultScore { get; set; }
        public float racyScore { get; set; }
    }

    public class Metadata
    {
        public int width { get; set; }
        public int height { get; set; }
        public string format { get; set; }
    }

    public class Color
    {
        public string dominantColorForeground { get; set; }
        public string dominantColorBackground { get; set; }
        public string[] dominantColors { get; set; }
        public string accentColor { get; set; }
        public bool isBWImg { get; set; }
    }

    public class Imagetype
    {
        public int clipArtType { get; set; }
        public int lineDrawingType { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public float score { get; set; }
    }

    public class Face
    {
        public int age { get; set; }
        public string gender { get; set; }
        public Facerectangle faceRectangle { get; set; }
    }
}