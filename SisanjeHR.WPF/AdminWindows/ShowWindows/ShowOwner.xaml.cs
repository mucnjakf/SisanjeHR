using DataAccessLayer.Models;
using System;
using System.Windows;

namespace SisanjeHrDesktopApp.AdminWindows.ShowWindows
{
    /// <summary>
    /// Interaction logic for ShowOwner.xaml
    /// </summary>
    public partial class ShowOwner : Window
    {
        private readonly Owner owner;

        public ShowOwner(Owner owner)
        {
            InitializeComponent();

            this.owner = owner;

            SetOwnerValues();
        }

        private void SetOwnerValues()
        {
            LblId.Text = owner.Id.ToString();
            LblFirstName.Text = owner.FirstName;
            LblLastName.Text = owner.LastName;
            LblPin.Text = owner.Pin;
            LblEmail.Text = owner.Email;
            LblUsername.Text = owner.Username;
            LblSubscription.Text = owner.Subscription.ToString();
            LbHairSalons.ItemsSource = owner.HairSalons;
            LbWorkers.ItemsSource = owner.Workers;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
