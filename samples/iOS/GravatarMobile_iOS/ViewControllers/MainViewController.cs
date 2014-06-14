
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GravatarMobile.Core;

namespace GravatarMobile_iOS.ViewControllers
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController(IntPtr handle) : base(handle)
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			edtEmail.Text = @"newky2k@mac.com";

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
				imgGravatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Round;
			};

			btnSquare.TouchUpInside += (object sender, EventArgs e) =>
			{
				this.View.EndEditing(true);
				imgGravatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Square;
			};
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
		}

	
		#endregion
	}
}

