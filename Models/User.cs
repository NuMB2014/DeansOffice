using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace deans_office.Models
{
    class User
    {
        private string Login;
        private static string token;
        public static int role { get; set; }//0 - adm // 1 - operator // 2 - student
        public User()
        {
            Login = "";
            token = "";
            role = 2;
        }
        public void SetLogin(string login) { Login = login; }
        public void CheckData(string login, string password)
        {
            api api = new api();
            SHA256 sha256Hash = SHA256.Create();
            string hashkey = BitConverter.ToString(sha256Hash.ComputeHash(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)))).Replace("-", string.Empty);
            Dictionary<string, string> JsonObject = new Dictionary<string, string>();
            JsonObject.Add("login", login);
            JsonObject.Add("hash", hashkey);
            string json = JsonConvert.SerializeObject(JsonObject, Formatting.Indented);
            string token = api.Request(1, json);
            SetToken(token);
            if (String.IsNullOrEmpty(token))
            {
                SetLogin(login);
            }
        }

        private void SetToken(string Token) { token = Token; }
        public static string GetToken() { return token; }
        private void SetRole(int idrole) { role = idrole; }
        public static int GetRole() { return role; }
    }
}
