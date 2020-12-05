using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;
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
                    switch (e.Status)
                    {
                        case WebExceptionStatus.ConnectFailure:
                            MessageBox.Show("Невозможно подключится с серверу.\nОбратитесь к администратору для решения данной проблемы.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.ConnectionClosed:
                            MessageBox.Show("Соединение с сервером было преждевременно закрыто.\nОбратитесь к администратору для решения данной проблемы.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.KeepAliveFailure:
                            MessageBox.Show("Сервер закрыл подключение, установленное с заданным заголовком проверки активности.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.NameResolutionFailure:
                            MessageBox.Show("Службе имен не удалось разрешить имя узла.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.ProtocolError:
                            MessageBox.Show("От сервера получен полный ответ, указывающий на ошибку на уровне протокола.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.ReceiveFailure:
                            MessageBox.Show("От удаленного сервера не получен полный ответ.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.RequestCanceled:
                            MessageBox.Show("Запрос отменен.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.SecureChannelFailure:
                            MessageBox.Show("Произошла ошибка в защищенном канале связи.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.SendFailure:
                            MessageBox.Show("Не удалось отправить полный запрос на удаленный сервер.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.ServerProtocolViolation:
                            MessageBox.Show("Ответ от сервера не является допустимым ответом HTTP.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;                        
                        case WebExceptionStatus.Timeout:
                            MessageBox.Show("В течение заданного для запроса времени ожидания не был получен ответ.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.TrustFailure:
                            MessageBox.Show("Не удалось проверить сертификат сервера.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.MessageLengthLimitExceeded:
                            MessageBox.Show("Получено сообщение, которое превышает ограничение, заданное при отправке запроса или получении ответа от сервера.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.Pending:
                            MessageBox.Show("Ожидается выполнение внутреннего асинхронного запроса.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.PipelineFailure:
                            MessageBox.Show("Это значение поддерживает платформу .NET Framework и не должно использоваться непосредственно из вашего кода.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case WebExceptionStatus.ProxyNameResolutionFailure:
                            MessageBox.Show("Службе разрешения имен не удалось разрешить имя узла прокси-сервера.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        default:
                            MessageBox.Show("Произошла непредвиденная ошибка.\nОбратитесь к администратору для решения данной проблемы.", "Ошибка соединения",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }                    
                    return null;
                }
            }
            else
                return null;
        }
    }
}