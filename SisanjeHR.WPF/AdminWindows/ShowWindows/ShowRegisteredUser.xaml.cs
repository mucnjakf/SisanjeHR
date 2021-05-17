using DataAccessLayer.Models;
using System;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.ShowWindows
{
    /// <summary>
    /// Interaction logic for ShowRegisteredUser.xaml
    /// </summary>
    public partial class ShowRegisteredUser : Window
    {
        private readonly RegisteredUser registeredUser;
        
        public ShowRegisteredUser(RegisteredUser registeredUser)
        {
            InitializeComponent();

            this.registeredUser = registeredUser;

            SetRegisteredUserValues();
        }

        private void SetRegisteredUserValues()
        {
            LblId.Text = registeredUser.Id.ToString();
            LblFirstName.Text = registeredUser.FirstName;
            LblLastName.Text = registeredUser.LastName;
            LblEmail.Text = registeredUser.Email;
            LblUsername.Text = registeredUser.Username;
            LblAddress.Text = registeredUser.Location.Address;
            LblCity.Text = registeredUser.Location.City.Name;
            LblCountry.Text = registeredUser.Location.City.Country.Name;

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
