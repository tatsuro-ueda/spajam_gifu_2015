namespace SpajamHonsen.Models.JsonResponse
{
    public class BaiduSpeechAPIResponseModel
    {
        public string corpus_no { get; set; }
        public string err_msg { get; set; }
        public int err_no { get; set; }
        public string[] result { get; set; }
        public string sn { get; set; }
    }
}