using System.Windows;
using GravatarMobile;

namespace WPFSample
{
    /// <summary>
    /// Loads a Gravatar image and profile for an email address.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnLoadClicked(object sender, RoutedEventArgs e)
        {
            btnLoad.IsEnabled = false;
            lblName.Text = string.Empty;
            lblStatus.Text = "Loading...";

            try
            {
                var gravatar = new Gravatar(edtEmail.Text);

                await gravatar.LoadImageAsync(320);
                imgGravatar.Source = gravatar.ImageAsBitmap();

                await gravatar.LoadProfileAsync();
                lblName.Text = gravatar.Profile?.DisplayName ?? string.Empty;
                lblStatus.Text = gravatar.Profile == null ? "No profile" : "Loaded";
            }
            catch (System.Exception ex)
            {
                lblStatus.Text = ex.Message;
            }
            finally
            {
                btnLoad.IsEnabled = true;
            }
        }
    }
}
