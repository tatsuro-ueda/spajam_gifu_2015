using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpajamMadobenTestWebClient
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 送信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                // アプリで使用するための HttpClient インスタンスを作成 
                var httpClient = new HttpClient();

                // POST 送信先の Uri
                var uri = new Uri(this.URL.Text);

                HttpContent param = new StringContent(this.Param.Text, System.Text.Encoding.UTF8, "text/plain");

                switch (this.Method.Text)
                {
                    case "GET":
                        await httpClient.GetAsync(uri);
                        break;
                    case "POST":
                        if(!string.IsNullOrWhiteSpace(this.File.Text))
                        {
                            var filePath = this.File.Text;
                            
                            using(var stream = System.IO.File.OpenRead(filePath))
                            {
                                var byteArray = new byte[stream.Length];

                                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                                stream.Read(byteArray, 0, (int)stream.Length);
                                var fileContent = new ByteArrayContent(byteArray);
                                fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/x-flac");
                                multiContent.Add(fileContent, JsonConvert.SerializeObject("buffer"));
                                param = multiContent;
                            }
                        }

                        await httpClient.PostAsync(uri, param);
                        
                        break;
                    case "PUT":
                        await httpClient.PutAsync(uri, param);
                        break;
                    case "DELETE":
                        await httpClient.DeleteAsync(uri);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                //握りつぶす
            }
        }

        /// <summary>
        /// ファイル選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.DefaultExt = "*.*";
            if (ofd.ShowDialog() == true)
            {
                this.File.Text = ofd.FileName;
            }
        }

        /// <summary>
        /// 環境選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Server_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Server.Text == "テスト")
            {
                this.URL.Text = "http://spajammadobenwebapi.azurewebsites.net/api/Service";
                return;
            }

            this.URL.Text = "http://localhost:24133/api/Service";
        }
    }
}
