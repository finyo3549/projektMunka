using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Models
{
    internal class User : INotifyPropertyChanged
    {
        /// <summary>
        /// Esemény, ami akkor váltódik ki, amikor egy tulajdonság értéke megváltozik. Az INotifyPropertyChanged által követelt.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Értesíti a figyelőket egy tulajdonság értékének változásáról.
        /// </summary>
        /// <param name="propertyName">A megváltozott tulajdonság neve.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Meghívja a PropertyChanged eseményt; a '?' biztosítja, hogy csak akkor hívódjon meg, ha vannak előfizetők.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Az Id tulajdonság privát mezője. Háttértár a Id tulajdonsághoz.
        private long id;
        [JsonProperty("id")]
        public long Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    // Értesít minden kötést, hogy az Id tulajdonság értéke megváltozott.
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        // Hasonló struktúra érvényes az UserName, Email, Password tulajdonságokra is:
        // Mindegyiknek van egy privát mezője, egy nyilvános gettere, és egy settere, amely értesítést küld, ha az érték megváltozik.

        private string userName;
        [JsonProperty("name")]
        public string UserName
        {
            get => userName;
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
        [JsonProperty("email")]
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
        [JsonProperty("password")]
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

    }
}
