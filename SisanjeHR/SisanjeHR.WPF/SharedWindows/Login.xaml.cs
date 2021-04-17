using SisanjeHrDesktopApp.AdminWindows;
using ApiClient.Api;
using SisanjeHrDesktopApp.OwnerWindows;
using SisanjeHrDesktopApp.OwnerWindows.AddNewWindows;
using SisanjeHrDesktopApp.SharedWindows.SharedCustom;
using Utilities.ViewModels.AdminViewModels;
using Utilities.ViewModels.OwnerViewModels;
using System.Threading.Tasks;
using System.Windows;
using static SisanjeHrDesktopApp.SharedWindows.Start;
using System.Windows.Controls;
using System;
using Utilities.ViewModels.WorkerViewModels;
using SisanjeHrDesktopApp.WorkerWindows;

namespace SisanjeHrDesktopApp.SharedWindows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly LoadingScreen ls;

        private readonly User user;

        private readonly AdminApiClient adminApi;
        private readonly OwnerApiClient ownerApi;
        private readonly WorkerApiClient workerApi;

        public Login(User user)
        {
            InitializeComponent();

            ls = new LoadingScreen();

            adminApi = new AdminApiClient();
            ownerApi = new OwnerApiClient();
            workerApi = new WorkerApiClient();

            this.user = user;
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            await LoginUser();
        }

        private async Task LoginUser()
        {
            if (ValidateInputs())
            {
                Close();
                ls.LblLoading.Text = "Logging in";
                ls.Show();

                switch (user)
                {
                    case User.Admin:
                        await LoginAdmin();
                        break;
                    case User.Owner:
                        await LoginOwner();
                        break;
                    case User.Worker:
                        await LoginWorker();
                        break;
                }

                ls.Close();
                return;
            }
            MessageBox.Show("All input fields are required!");
        }

        private bool ValidateInputs()
        {
            if (TbUsername.Text != string.Empty &&
                TbPassword.Password != string.Empty)
            {
                return true;
            }
            return false;
        }

        private async Task LoginAdmin()
        {
            AdminLoginVM adminLoginVM = new AdminLoginVM()
            {
                Username = TbUsername.Text,
                Password = TbPassword.Password
            };

            bool access = await adminApi.AuthenticateAdmin(adminLoginVM);

            if (access)
            {
                AdminMainMenu adminMainMenu = new AdminMainMenu();
                await adminMainMenu.SetAdminLoggedIn(adminLoginVM);
                adminMainMenu.Show();
            }
            else
            {
                MessageBox.Show("Unsuccessfull login!");

                Login loginWindow = new Login(user);
                loginWindow.TextBlockWelcome.Text = "Welcome Admin";
                loginWindow.TextBlockLoginRegister.Text = "Login";
                loginWindow.Show();
            }
        }

        private async Task LoginOwner()
        {
            OwnerLoginVM ownerLoginVM = new OwnerLoginVM()
            {
                Username = TbUsername.Text,
                Password = TbPassword.Password
            };

            bool access = await ownerApi.AuthenticateOwner(ownerLoginVM);

            if (access)
            {
                OwnerMainMenu ownerMainMenu = new OwnerMainMenu();
                await ownerMainMenu.SetOwnerLoggedIn(ownerLoginVM);
                ownerMainMenu.Show();
            }
            else
            {
                MessageBox.Show("Unsuccessfull login!");

                Login loginWindow = new Login(user);
                loginWindow.TextBlockWelcome.Text = "Welcome Owner";
                loginWindow.TextBlockLoginRegister.Text = "Login or Register";
                Grid.SetColumn(loginWindow.BtnLogin, 2);
                loginWindow.BtnRegister.Visibility = Visibility.Visible;
                loginWindow.Show();
            }
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            await RegisterOwner();
        }

        private async Task RegisterOwner()
        {
            RegisterOwner registerOwnerWindow = new RegisterOwner();
            registerOwnerWindow.ShowDialog();

            string username = registerOwnerWindow.GetRegisteredOwnerUsername();
            string password = registerOwnerWindow.GetRegisteredOwnerPassword();
            bool registrationSuccess = registerOwnerWindow.IsRegistrationSuccessfull();

            if (registrationSuccess)
            {
                await LoginRegisteredOwner(username, password);
            }
        }

        private async Task LoginRegisteredOwner(string username, string password)
        {
            TbUsername.Text = username;
            TbPassword.Password = password;

            Close();
            ls.LblLoading.Text = "Logging in";
            ls.Show();
            await LoginOwner();
            ls.Close();
        }

        private async Task LoginWorker()
        {
            WorkerLoginVM workerLoginVM = new WorkerLoginVM()
            {
                Username = TbUsername.Text,
                Password = TbPassword.Password
            };

            bool access = await workerApi.AuthenticateWorker(workerLoginVM);

            if (access)
            {
                WorkerMainMenu workerMainMenu = new WorkerMainMenu();
                await workerMainMenu.SetWorkerLoggedIn(workerLoginVM);
                workerMainMenu.Show();
            }
            else
            {
                MessageBox.Show("Unsuccessfull login!\nUnemployed worker cannot login!");

                Login loginWindow = new Login(user);
                loginWindow.TextBlockWelcome.Text = "Welcome Worker";
                loginWindow.TextBlockLoginRegister.Text = "Login";
                loginWindow.Show();
            }
        }

        private void BtnBackToStart_Click(object sender, RoutedEventArgs e)
        {
            Start startWindow = new Start();
            startWindow.Show();

            Close();
        }
    }
}
