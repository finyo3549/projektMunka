using MagicQuizDesktop.Models;
using MagicQuizDesktop.View.Windows;
using Newtonsoft.Json; // Newtonsoft.Json importálása
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MagicQuizDesktop.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static readonly HttpClient client = new HttpClient();
        private ObservableCollection<User> users;
        private HomeWindow _homeWindow;
        private string authToken; // A token tárolására szolgáló változó

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }


        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            client.BaseAddress = new Uri("http://127.0.0.1:8000/api/");
            LoginCommand = new RelayCommand(async _ => await Login());
        }

        private bool ValidateLoginInput()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Az email cím megadása kötelező.");
                return false;
            }
            // Egy egyszerű email cím validálás
            if (!Email.Contains("@") || !Email.Contains("."))
            {
                MessageBox.Show("Érvénytelen email cím.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("A jelszó megadása kötelező.");
                return false;
            }
            if (Password.Length < 8)
            {
                MessageBox.Show("A jelszónak legalább 8 karakter hosszúnak kell lennie.");
                return false;
            }

            return true;
        }

        private async Task Login()
        {
            if (!ValidateLoginInput())
            {
                return;
            }

            var userLogin = new
            {
                email = Email,
                password = Password
            };

            var jsonLogin = JsonConvert.SerializeObject(userLogin);
            var contentLogin = new StringContent(jsonLogin, Encoding.UTF8, "application/json");

            try
            {
                // Bejelentkezési kérés elküldése
                var loginResponse = await client.PostAsync("login", contentLogin);
                if (loginResponse.IsSuccessStatusCode)
                {
                    var loginContent = await loginResponse.Content.ReadAsStringAsync();
                    var loginResult = JsonConvert.DeserializeObject<dynamic>(loginContent); // Dinamikus típus a flexibilitás érdekében

                    authToken = loginResult.token; // A token eltárolása
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken); // Token hozzáadása a kérések fejlécéhez

                    var userId = loginResult.user_id; // Feltételezzük, hogy a válasz tartalmazza a userId-t

                    // Lekérdezzük a felhasználó adatait a /api/user/{id} végponton
                    var userResponse = await client.GetAsync($"users/{userId}");
                    if (userResponse.IsSuccessStatusCode)
                    {
                        var userContent = await userResponse.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<User>(userContent); // A User osztály tartalmazza a felhasználó adatait

                        // Sikeres adatlekérdezés esetén további logika
                        _homeWindow = new HomeWindow(user);
                        _homeWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("A felhasználói adatok lekérdezése sikertelen. Próbáld újra!");
                    }
                }
                else
                {
                    MessageBox.Show("Bejelentkezés sikertelen. Próbáld újra!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }

    }
}
