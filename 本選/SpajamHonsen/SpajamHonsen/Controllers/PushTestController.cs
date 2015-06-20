using PushSharp;
using PushSharp.Apple;
using PushSharp.Core;
using SpajamHonsen.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace SpajamHonsen.Controllers
{
    public class PushTestController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        //  api/pushTest

        /// <summary>
        /// 通知を行います。
        /// </summary>
        /// <param name="message">通知するメッセージ</param>
        [HttpPost]
        public void Notify([FromBody]string message)
        {
            var push = new PushBroker();

            //Push通知の各イベントを設定（なくてもOK）
            push.OnNotificationSent += NotificationSent;
            push.OnChannelException += ChannelException;
            push.OnServiceException += ServiceException;
            push.OnNotificationFailed += NotificationFailed;
            push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            push.OnChannelCreated += ChannelCreated;
            push.OnChannelDestroyed += ChannelDestroyed;

            //DBに登録されているデバイストークンをすべて取得
            List<byte[]> allDeviceTokens =
                db.DeviceToken.Select(d => d.DeviceToken1).ToList();

            foreach (byte[] token in allDeviceTokens)
            {
                //デバイストークンを16進数文字列に変換
                Encoding utf8Enc = Encoding.GetEncoding("utf-8");
                string deviceToken = utf8Enc.GetString(token);

                // TODO 証明書周り
                var appleCert = File.ReadAllBytes(@"C:\Users\miso_soup3\Desktop\push\my_apns_dev_cert.p12");
                push.RegisterAppleService(new PushSharp.Apple.ApplePushChannelSettings(appleCert, "ここには証明書のpasswordを"));
                push.QueueNotification(new AppleNotification()
                    .ForDeviceToken(deviceToken)
                    .WithAlert(message));
                //.WithBadge(7));　←ちなみにバッジ通知の場合はこのように。
            }
        }

        /// <summary>
        /// バイト配列から16進数の文字列を生成します。
        /// </summary>
        /// <param name="bytes">バイト配列</param>
        /// <returns>16進数文字列</returns>
        private static string BytesToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
        {
            //Currently this event will only ever happen for Android GCM
            Debug.WriteLine("Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " + newSubscriptionId + " -> " + notification);
        }

        static void NotificationSent(object sender, INotification notification)
        {
            Debug.WriteLine("Sent: " + sender + " -> " + notification);
        }

        static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
        {
            Debug.WriteLine("Failure: " + sender + " -> " + notificationFailureException.Message + " -> " + notification);
        }

        static void ChannelException(object sender, IPushChannel channel, Exception exception)
        {
            Debug.WriteLine("Channel Exception: " + sender + " -> " + exception);
        }

        static void ServiceException(object sender, Exception exception)
        {
            Debug.WriteLine("Channel Exception: " + sender + " -> " + exception);
        }

        static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
        {
            Debug.WriteLine("Device Subscription Expired: " + sender + " -> " + expiredDeviceSubscriptionId);
        }

        static void ChannelDestroyed(object sender)
        {
            Debug.WriteLine("Channel Destroyed for: " + sender);
        }

        static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            Debug.WriteLine("Channel Created for: " + sender);
        }
    }
}
