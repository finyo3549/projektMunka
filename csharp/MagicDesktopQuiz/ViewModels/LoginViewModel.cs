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

            var user = new
            {
                email = Email,
                password = Password
            };

            var json = JsonConvert.SerializeObject(user); // Newtonsoft.Json használata a serializáláshoz
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("login", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<User>(responseContent); // Newtonsoft.Json használata a deserializáláshoz

                    // Sikeres bejelentkezés esetén további logika
                    _homeWindow = new HomeWindow(Email);
                    _homeWindow.ShowDialog();
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
