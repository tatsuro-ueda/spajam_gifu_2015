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
        private SpajamHonsen.AccessToken.BingAPIToken.AdmAccessToken admToken;
        private SpajamHonsen.AccessToken.BingAPIToken.AdmAuthentication admAuth;

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
            admAuth = new SpajamHonsen.AccessToken.BingAPIToken.AdmAuthentication(clientId, clientSecret);
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

}
