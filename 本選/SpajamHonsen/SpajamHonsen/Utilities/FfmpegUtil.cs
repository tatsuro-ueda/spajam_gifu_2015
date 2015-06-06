using System;
using System.Diagnostics;
using System.Web;

namespace SpajamHonsen.Utilities
{
    /// <summary>
    /// FFmpegのユーティリティークラス
    /// </summary>
    /// <remarks>
    /// FFmpegのユーティリティークラス
    /// </remarks>
    public static class FFmpegUtil
    {
        /// <summary>
        /// 音声ファイルのレートを変換する
        /// </summary>
        /// <param name="inputFilePath">インプットファイルのパス</param>
        /// <returns>アウトプットファイルのパス</returns>
        public static string ConvertAudioRate(string inputFilePath, string rate)
        {
            string result = string.Empty;
            string outputFilePath = string.Empty;
            try
            {
                string ffmpegFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/ffmpeg.exe");
                outputFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/" + Guid.NewGuid().ToString() + ".wav");

                var processInfo = new ProcessStartInfo(ffmpegFilePath, " -i " + inputFilePath + " -ar " + rate + " " + outputFilePath)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                try
                {
                    Process process = System.Diagnostics.Process.Start(processInfo);
                    result = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    process.Close(); 
                    result = outputFilePath;
                }
                catch (Exception)
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return outputFilePath;
        }
    }
}