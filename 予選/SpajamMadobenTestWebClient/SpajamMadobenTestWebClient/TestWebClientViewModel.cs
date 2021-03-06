﻿using Microsoft.Win32;
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
using System.Windows;

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

        #region Const
        private const string SERVER_TEST = "テスト";
        private const string SERVER_HONBAN = "本番";

        private const string METHOD_GET = "GET";
        private const string METHOD_POST = "POST";
        private const string METHOD_PUT = "PUT";
        private const string METHOD_DELETE = "DELETE";

        private const string CONTENT_TYPE_TEXT = "text/plain";
        private const string CONTENT_TYPE_JSON = "application/json";
        private const string CONTENT_TYPE_AUDIO = "audio/x-flac";
        private const string CONTENT_TYPE_OTHER = "application/x-www-form-urlencoded";
        #endregion Const

        #region Fields
        /// <summary> 接続先一覧 </summary>
        ObservableCollection<ComboBoxModel> servers_ = new ObservableCollection<ComboBoxModel>();
        /// <summary> 選択中の接続先 </summary>
        ComboBoxModel server_ = new ComboBoxModel();
        /// <summary> URL</summary>
        string url_ = string.Empty;
        /// <summary> メソッド一覧 </summary>
        ObservableCollection<ComboBoxModel> methods_ = new ObservableCollection<ComboBoxModel>();
        /// <summary> 選択中のメソッド </summary>
        ComboBoxModel method_ = new ComboBoxModel();
        /// <summary> コンテンツタイプ一覧 </summary>
        ObservableCollection<ComboBoxModel> contentTypes_ = new ObservableCollection<ComboBoxModel>();
        /// <summary> 選択中のコンテンツタイプ </summary>
        ComboBoxModel contentType_ = new ComboBoxModel();
        /// <summary> ファイルパス</summary>
        string filePath_ = string.Empty;
        /// <summary> パラメーター</summary>
        string parameter_ = string.Empty;
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
                if (this.server_.ComboBoxItem == SERVER_TEST)
                {
                    this.URL = "http://localhost:3220/api/AudioCommentaries";
                    return;
                }

                this.URL = "http://spajamapi.azurewebsites.net/api/AudioCommentaries";
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
                    ComboBoxItem = SERVER_TEST
                }
            );
            this.Servers.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = SERVER_HONBAN
                }
            );
            this.Server = Servers.Where(server => server.ComboBoxItem == SERVER_TEST).First();
            // メソッド設定
            this.Methods.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = METHOD_GET
                }
            );
            this.Methods.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = METHOD_POST
                }
            );
            this.Methods.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = METHOD_PUT
                }
            );
            this.Methods.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = METHOD_DELETE
                }
            );
            this.Method = Methods.Where(method => method.ComboBoxItem == METHOD_POST).First();
            // コンテンツタイプ設定
            this.ContentTypes.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = CONTENT_TYPE_TEXT
                }
            );
            this.ContentTypes.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = CONTENT_TYPE_JSON
                }
            );
            this.ContentTypes.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = CONTENT_TYPE_AUDIO
                }
            );
            this.ContentTypes.Add(
                new ComboBoxModel()
                {
                    ComboBoxItem = CONTENT_TYPE_OTHER
                }
            );
            this.ContentType = ContentTypes.Where(contentType => contentType.ComboBoxItem == CONTENT_TYPE_TEXT).First();

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

                // HttpContent param = new StringContent(this.Parameter, System.Text.Encoding.UTF8, CONTENT_TYPE_TEXT);
                HttpContent param = new StringContent(this.Parameter, System.Text.Encoding.UTF8, CONTENT_TYPE_JSON);

                switch (this.Method.ComboBoxItem)
                {
                    case METHOD_GET:
                        await httpClient.GetAsync(uri);
                        break;
                    case METHOD_POST:
                        if (!string.IsNullOrWhiteSpace(this.FilePath))
                        {
                            var filePath = this.FilePath;

                            using (var stream = System.IO.File.OpenRead(filePath))
                            {
                                var byteArray = new byte[stream.Length];

                                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                                stream.Read(byteArray, 0, (int)stream.Length);
                                var fileContent = new ByteArrayContent(byteArray);
                                fileContent.Headers.ContentType = new MediaTypeHeaderValue(CONTENT_TYPE_AUDIO);
                                multiContent.Add(fileContent, JsonConvert.SerializeObject("buffer"));
                                param = multiContent;
                            }
                        }

                        var result = await httpClient.PostAsync(uri, param);

                        var resultString = await result.Content.ReadAsStringAsync();

                        MessageBox.Show(resultString);

                        break;
                    case METHOD_PUT:
                        await httpClient.PutAsync(uri, param);
                        break;
                    case METHOD_DELETE:
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
