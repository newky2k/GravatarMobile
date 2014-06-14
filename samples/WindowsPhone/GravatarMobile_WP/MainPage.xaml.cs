using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GravatarMobile_WP.Resources;
using GravatarMobile.Core;

namespace GravatarMobile_WP
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            edtEmail.Text = @"newky2k@mac.com";
        }

        private void OnGoClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                imgGravatar.Avatar = new Gravatar(edtEmail.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imgGravatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Square;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            imgGravatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Round;
        }



    }
}