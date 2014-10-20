using System;
using System.Drawing;
using GravatarMobile.Core;

#if __UNIFIED__
using UIKit;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif

namespace GravatarMobileForiOS.ViewControllers
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController(IntPtr handle) : base(handle)
		{
		}

		#region View lifecycle

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			edtEmail.EditingDidEndOnExit += (object sender, EventArgs e) =>
			{

			};


			btnLoad.TouchUpInside += (object sender, EventArgs e) =>
			{
				try
				{
					this.View.EndEditing(true);

					imgGravatar.Avatar = new Gravatar(edtEmail.Text);
				}
				catch (Exception ex)
				{
					var alt = new UIAlertView("Error", ex.Message, null,"OK");
					alt.Show();
				}


			};

			btnCircle.TouchUpInside += (object sender, EventArgs e) =>
			{
				this.View.EndEditing(true);

				imgGravatar.BorderColor = UIColor.Gray;
				imgGravatar.BorderWidth = 5.0f;
				imgGravatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Round;

			};

			btnSquare.TouchUpInside += (object sender, EventArgs e) =>
			{
				this.View.EndEditing(true);

				imgGravatar.BorderColor = UIColor.Green;
				imgGravatar.BorderWidth = 2.0f;
				imgGravatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Square;
			};
		}
			
		#endregion
	}
}

