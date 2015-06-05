using System;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Media;
using System.Diagnostics;

namespace BingTranslrate
{
    class CBingTranslate
    {
        private string m_headerValue;
        private DateTime m_InitDate;
        private AdmAccessToken admToken;
        private AdmAuthentication admAuth;

        public bool Init(string clientId, string clientSecret)
        {
            if (m_InitDate != null)
            {
                TimeSpan ts = DateTime.Now - m_InitDate;
                Debug.WriteLine("Init : TotalSeconds :" + ts.TotalSeconds.ToString());
                if (ts.TotalSeconds < 600) return true;
            }

            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            admAuth = new AdmAuthentication(clientId, clientSecret);
            try
            {
                admToken = admAuth.GetAccessToken();
                // Create a header with the access_token property of the returned token
                m_headerValue = "Bearer " + admToken.access_token;
            }

            catch (WebException e)
            {
                ProcessWebException(e);
                return false;
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Init : Error " + ex.Message);
                return false;
            }

            Debug.WriteLine("Init : access_token :" + admToken.access_token);
            Debug.WriteLine("Init : expires_in   :" + admToken.expires_in);
            Debug.WriteLine("Init : scope        :" + admToken.scope);
            Debug.WriteLine("Init : token_type   :" + admToken.token_type);

            m_InitDate = DateTime.Now;

            return true;

        }

        public string TranslateMethod(string input, string from, string to)
        {
            Debug.WriteLine("TranslateMethod >>");

            string translation = "";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?&text=" + System.Web.HttpUtility.UrlEncode(input) + "&from=" + from + "&to=" + to + "&contentType=text%2fplain";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", m_headerValue);
            //httpWebRequest.KeepAlive = false;
            WebResponse response = null;

            Debug.WriteLine("TranslateMethod : header = " + m_headerValue);
            Debug.WriteLine("TranslateMethod : uri = " + uri);

            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    translation = (string)dcs.ReadObject(stream);
                    stream.Close();
                }
            }

            catch (WebException e)
            {
                ProcessWebException(e);
                return "";
            }

            catch (Exception ex)
            {
                Debug.WriteLine("TranslateMethod : Error " + ex.Message);
                return "";
            }

            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }

            Debug.WriteLine("TranslateMethod <<");

            return translation;
        }

        private void ProcessWebException(WebException e)
        {
            //Console.WriteLine("{0}", e.ToString());
            Debug.WriteLine("ProcessWebException : Error " + e.Message);
            // Obtain detailed error information
            string strResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)e.Response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.ASCII))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            //Console.WriteLine("Http status code={0}, error message={1}", e.Status, strResponse);
            Debug.WriteLine("ProcessWebException : Http status code={0}, error message={1}", e.Status, strResponse);
        }
    }

    [DataContract]
    public class AdmAccessToken
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public string expires_in { get; set; }
        [DataMember]
        public string scope { get; set; }
    }

    public class AdmAuthentication
    {
        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private string clientId;
        private string cientSecret;
        private string request;

        public AdmAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.cientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
        }

        public AdmAccessToken GetAccessToken()
        {
            return HttpPost(DatamarketAccessUri, this.request);
        }

        private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;
            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
                outputStream.Close();
            }
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                //Get deserialized object from JSON stream
                AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                webResponse.Close();
                return token;
            }
        }
    }
}
