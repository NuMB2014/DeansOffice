using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using deans_office.ModelView;

namespace deans_office
{ 
    public partial class login : Window
    {       
        public login()
        {
            
            InitializeComponent();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text.Length == 0)
            {
                errormessage.Text = "Введите логин";
                textBoxLogin.Focus();
            }
            else if (passwordBox1.Password.Length == 0)
            {
                errormessage.Text = "Введите пароль";               
                passwordBox1.Focus();
            }
            else
            {
                string login = textBoxLogin.Text;
                string password = passwordBox1.Password;                
                ModelView.Login.TryAuthorize(login, password);
                if (String.IsNullOrEmpty(Models.User.GetToken()))
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {     
                    index index_Window = new index();
                    this.Close();
                    index_Window.Show();
                }
            }
        }
    }
}
