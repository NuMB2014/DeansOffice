using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using deans_office.Models;

namespace deans_office.ModelView
{
    class Login
    {        
        public static void TryAuthorize(string login, string password)
        {
            User user = new User();
            user.CheckData(login, password);
        }
    }
}
