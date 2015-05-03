using Microsoft.Win32;
using Newtonsoft.Json;
using SampleBase.Base;
using SpajamMadobenTestWebClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpajamMadobenTestWebClient
{
    /// <summary>
    /// ビューモデル
    /// </summary>
    public class TestWebClientViewModel : PropertyChangedNotifier
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestWebClientViewModel()
        {
            Initialize();
        }
        
        #region Fields
        /// <summary> 接続先一覧 </summary>
        ObservableCollection<ComboBoxModel> servers_ = new ObservableCollection<ComboBoxModel>();
        /// <summary> 選択中の接続先 </summary>
        ComboBoxModel server_ = new ComboBoxModel();
        /// <summary> URL</summary>
        string url_;
        /// <summary> メソッド一覧 </summary>
        ObservableCollection<ComboBoxModel> methods_ = new ObservableCollection<ComboBoxModel>();
        /// <summary> 選択中のメソッド </summary>
        ComboBoxModel method_ = new ComboBoxModel();
        /// <summary> コンテンツタイプ一覧 </summary>
        ObservableCollection<ComboBoxModel> contentTypes_ = new ObservableCollection<ComboBoxModel>();
        /// <summary> 選択中のコンテンツタイプ </summary>
        ComboBoxModel contentType_ = new ComboBoxModel();
        /// <summary> ファイルパス</summary>
        string filePath_;
        /// <summary> パラメーター</summary>
        string parameter_;
        #endregion Fields

        #region Commands
        /// <summary>
        /// ファイル選択ボタン押下時コマンド
        /// </summary>
        private RelayCommand fireFileSelect_;

        /// <summary>
        /// ファイル選択ボタン押下時コマンド
        /// </summary>
        public RelayCommand FireFileSelect
        {
            get
            {
                return this.fireFileSelect_;
            }
        }

        /// <summary>
        /// 送信ボタン押下時コマンド
        /// </summary>
        private RelayCommand fireSend_;

        /// <summary>
        /// 送信ボタン押下時コマンド
        /// </summary>
        public RelayCommand FireSend
        {
            get
            {
                return this.fireSend_;
            }
        }
        #endregion Commands

        #region Properties
        /// <summary>
        /// 接続先情報一覧
        /// </summary>
        public ObservableCollection<ComboBoxModel> Servers
        {
            get
            {
                return servers_;
            }
        }

        /// <summary>
        /// 選択中の接続先
        /// </summary>
        public ComboBoxModel Server
        {
            get
            {
                return server_;
            }
            set
            {
                server_ = value;
                this.OnPropertyChagned("Server");
                if (this.server_.ComboBoxItem == "テスト")
                {
                    this.URL = "http://localhost:24133/api/Service";
                    return;
                }

                this.URL = "http://spajammadobenwebapi.azurewebsites.net/api/Service";
            }
        }

        /// <summary>
        /// URL
        /// </summary>
        public string URL 
        {
            get
            {
                return url_;
            }
            set
            {
                url_ = value;
                this.OnPropertyChagned("URL");
            }
        }
        /// <summary>
        /// メソッド一覧
        /// </summary>
        public ObservableCollection<ComboBoxModel> Methods
        {
            get
            {
                return methods_;
            }
        }

        /// <summary>
        /// 選択中のメソッド
        /// </summary>
        public ComboBoxModel Method
        {
            get
            {
                return method_;
            }
            set
            {
                method_ = value;
                this.OnPropertyChagned("Method");
            }
        }
        /// <summary>
        /// コンテンツタイプ一覧
        /// </summary>
        public ObservableCollection<ComboBoxModel> ContentTypes
        {
            get
            {
                return contentTypes_;
            }
        }

        /// <summary>
        /// 選択中のコンテンツタイプ
        /// </summary>
        public ComboBoxModel ContentType
        {
            get
            {
                return contentType_;
            }
            set
            {
                contentType_ = value;
                this.OnPropertyChagned("ContentType");
            }
        }
        /// <summary>
        /// ファイルパス
        /// </summary>
        public string FilePath
        {
            get
            {
                return filePath_;
            }
            set
            {
                filePath_ = value;
                this.OnPropertyChagned("FilePath");
            }
        }

        /// <summary>
        /// パラメーター
        /// </summary>
        public string Parameter
        {
            get
            {
                return parameter_;
            }
            set
            {
                parameter_ = value;
                this.OnPropertyChagned("Parameter");
            }
        }
        
        #endregion Properties

        #region Methods
        /// <summary>
        /// 初期表示処理を行います。
        /// </summary>
        private void Initialize()
        {
            // ファイル選択ボタンのコマンド設定
            this.fireFileSelect_ = new RelayCommand(OnFireFileSelect);
            // 送信ボタンのコマンド設定
            this.fireSend_ = new RelayCommand(OnFireSendAsync);
            // 接続先設定
            this.Servers.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "テスト"
                }
            );
            this.Servers.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "本番"
                }
            );
            this.Server = Servers.Where(server => server.ComboBoxItem == "テスト").First();
            // メソッド設定
            this.Methods.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "GET"
                }
            );
            this.Methods.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "POST"
                }
            );
            this.Methods.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "PUT"
                }
            );
            this.Methods.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "DELETE"
                }
            );
            this.Method = Methods.Where(method => method.ComboBoxItem == "POST").First();
            // コンテンツタイプ設定
            this.ContentTypes.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "text/plain"
                }
            );
            this.ContentTypes.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "application/json"
                }
            );
            this.ContentTypes.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = "application/x-www-form-urlencoded"
                }
            );
            this.ContentType = ContentTypes.Where(contentType => contentType.ComboBoxItem == "text/plain").First();

        }
        
        /// <summary>
        /// ファイル選択ボタン押下時の処理
        /// </summary>
        private void OnFireFileSelect()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.DefaultExt = "*.*";
            if (ofd.ShowDialog() == true)
            {
                this.FilePath = ofd.FileName;
            }
        }

        /// <summary>
        /// 送信ボタン押下時の処理
        /// </summary>
        private async void OnFireSendAsync()
        {
            try
            {
                // アプリで使用するための HttpClient インスタンスを作成 
                var httpClient = new HttpClient();

                // POST 送信先の Uri
                var uri = new Uri(this.URL);

                HttpContent param = new StringContent(this.Parameter, System.Text.Encoding.UTF8, "text/plain");

                switch (this.Method.ComboBoxItem)
                {
                    case "GET":
                        await httpClient.GetAsync(uri);
                        break;
                    case "POST":
                        if (!string.IsNullOrWhiteSpace(this.FilePath))
                        {
                            var filePath = this.FilePath;

                            using (var stream = System.IO.File.OpenRead(filePath))
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
            catch (Exception)
            {
                //握りつぶす
            }
        }
        #endregion Methods
    }
}
