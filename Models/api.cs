using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace deans_office.Models
{
    class api
    {
        string apiURL = "";
        string apiPort = "";
        bool SSL = false;
        List<string> metods = new List<string>() { 
            "server",//Нумерация с 0. Работает как массив
            "user/login", 
            "", 
            "" 
        };

        public api()
        {
            apiURL = "127.0.0.1";//заполнить из файла
            apiPort = "8080";
        }
        public string Request(int metodid, string json)
        {
            if (apiURL.Length > 0)
            {
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                WebRequest request = WebRequest.Create((SSL ? "https" : "http") + "://" + apiURL + (String.IsNullOrEmpty(apiPort) ? "" : (":" + apiPort)) + "/" + metods[metodid]
                        + "/");//
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "POST";
                using (Stream streamWriter = request.GetRequestStream())
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    streamWriter.Write(byteArray);
                    streamWriter.Flush();
                }
                try
                {
                    WebResponse response = request.GetResponse();
                    string temp;
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            temp = reader.ReadToEnd().ToString();
                        }
                    }
                    response.Close();
                    return temp.Replace("\"", string.Empty);
                }
                catch (WebException e)
                {
                    return null;
                }
            }
            else
                return null;
        }
    }
}
