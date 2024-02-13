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
using System.Windows.Shapes;

namespace SisanjeHrDesktopApp.SharedWindows
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
        }

        public enum User
        {
            Admin, Owner, Worker
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            SetupNewWindowCloseThis(User.Admin, "Welcome Admin", "Login", Visibility.Hidden, 1);
        }

        private void BtnOwner_Click(object sender, RoutedEventArgs e)
        {
            SetupNewWindowCloseThis(User.Owner, "Welcome Owner", "Login or Register", Visibility.Visible, 2);
        }

        private void BtnWorker_Click(object sender, RoutedEventArgs e)
        {
            SetupNewWindowCloseThis(User.Worker, "Welcome Worker", "Login", Visibility.Hidden, 1);
        }

        private void SetupNewWindowCloseThis(User user, string textBlockWelcomeText, string textBlockLoginRegisterText, Visibility visibility, int column)
        {
            Login loginWindow = new Login(user);
            loginWindow.TextBlockWelcome.Text = textBlockWelcomeText;
            loginWindow.TextBlockLoginRegister.Text = textBlockLoginRegisterText;
            Grid.SetColumn(loginWindow.BtnLogin, column);
            loginWindow.BtnRegister.Visibility = visibility;
            loginWindow.Show();

            Close();
        }

        private void BtnExitMain_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
