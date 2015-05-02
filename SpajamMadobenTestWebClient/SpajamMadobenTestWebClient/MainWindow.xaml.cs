using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // アプリで使用するための HttpClient インスタンスを作成 
                var httpClient = new HttpClient();

                // POST 送信先の Uri
                var uri = new Uri(this.URL.Text);

                var param = new StringContent(this.Param.Text, System.Text.Encoding.UTF8, "text/plain");

                switch (this.Method.Text)
                {
                    case "GET":
                        httpClient.GetAsync(uri);
                        break;
                    case "POST":
                        httpClient.PostAsync(uri, param);
                        break;
                    case "PUT":
                        httpClient.PutAsync(uri, param);
                        break;
                    case "DELETE":
                        httpClient.DeleteAsync(uri);
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
    }
}
