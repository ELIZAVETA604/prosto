using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", "Ваш_API_ключ");

                var requestData = new
                {
                    username,
                    password
                };

                try
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync("https://kinopoiskapiunofficial.tech/api/v2.2/auth/login", requestData);

                    if (response.IsSuccessStatusCode)
                    {
                        // Вход успешен, выполните необходимые действия
                        // Например, перейдите на главное окно
                        // this.Close(); // Если вы хотите закрыть окно авторизации после успешного входа
                    }
                    else
                    {
                        ErrorMessage.Text = "Неправильное имя пользователя или пароль.";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                }
            }
        }
    }
}
