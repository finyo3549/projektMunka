using MagicQuizDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MagicQuizDesktop.View.Pages
{
    /// <summary>
    /// Interaction logic for ProfilPage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage(User user)
        {
            InitializeComponent();
            UserId.Text = Convert.ToString(user.Id);
            UserName.Text = Convert.ToString(user.Name);
            UserEmail.Text = Convert.ToString(user.Email);
        }
    }
}
