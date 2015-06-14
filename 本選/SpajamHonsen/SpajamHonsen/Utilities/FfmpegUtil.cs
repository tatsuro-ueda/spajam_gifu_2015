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
        /// <param name="rate">音声ファイルの変換後のレート</param>
        /// <returns>アウトプットファイルのパス</returns>
        public static string ConvertAudioRate(string inputFilePath, string rate)
        {
            string result = string.Empty;
            string outputFilePath = string.Empty;
            try
            {
                string ffmpegFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/ffmpeg.exe");
                outputFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/" + Guid.NewGuid().ToString() + ".flac");

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

        /// <summary>
        /// 音声ファイルの形式を変換する
        /// </summary>
        /// <param name="inputFilePath">インプットファイルのパス</param>
        /// <param name="format">音声ファイルの変換後の形式</param>
        /// <returns>アウトプットファイルのパス</returns>
        public static string ConvertAudioFormat(string inputFilePath, string format)
        {
            string result = string.Empty;
            string outputFilePath = string.Empty;
            try
            {
                string ffmpegFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/ffmpeg.exe");
                outputFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/" + Guid.NewGuid().ToString() + "." + format);

                var processInfo = new ProcessStartInfo(ffmpegFilePath, " -i " + inputFilePath + " -ab 128 " + outputFilePath)
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