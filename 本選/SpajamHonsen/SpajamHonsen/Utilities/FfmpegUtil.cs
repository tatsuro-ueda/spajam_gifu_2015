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
    public class FfmpegUtil
    {
        /// <summary>
        /// 音声ファイルのレートを変換する
        /// </summary>
        /// <param name="inputFilePath">インプットファイルのパス</param>
        /// <returns>アウトプットファイルのパス</returns>
        public static string ConvertAudioRate(string filePath, string rate)
        {
            string result = string.Empty;
            string input = string.Empty;
            string output = string.Empty;
            try
            {
                string ffmpegFilePath = "~/ffmpeg/ffmpeg.exe";
                FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
                string filename = Path.GetFileNameWithoutExtension(fi.Name);
                string extension = Path.GetExtension(fi.Name);
                input = HttpContext.Current.Server.MapPath(filePath);
                output = HttpContext.Current.Server.MapPath("~/temp/" + filename);

                var processInfo = new ProcessStartInfo(HttpContext.Current.Server.MapPath(ffmpegFilePath), " -i \"" + input + "\" -ar " + rate + " \"" + output + "\"")
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
                    result = output;
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