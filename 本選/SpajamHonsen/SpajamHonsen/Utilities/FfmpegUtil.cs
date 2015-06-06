using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                string ffmpegFilePath = "~/ffmpeg/ffmpeg.exe";
                // FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
                // string filename = Path.GetFileNameWithoutExtension(fi.Name);
                // string extension = Path.GetExtension(fi.Name);
                // input = HttpContext.Current.Server.MapPath(filePath);
                outputFilePath = HttpContext.Current.Server.MapPath("~/Temp/Audios/" + Guid.NewGuid().ToString());

                var processInfo = new ProcessStartInfo(HttpContext.Current.Server.MapPath(ffmpegFilePath), " -i \"" + inputFilePath + "\" -ar " + rate + " \"" + outputFilePath + "\"")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                try
                {
                    Process process = System.Diagnostics.Process.Start(processInfo);
                    // result = process.StandardError.ReadToEnd();
                    result = outputFilePath;
                    process.WaitForExit();
                    process.Close();
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

            return result;
        }
    }
}