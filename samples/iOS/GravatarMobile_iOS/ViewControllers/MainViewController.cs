using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GravatarMobile.Core;
using System.Diagnostics;

namespace GravatarMobile_iOS.ViewControllers
{
    public partial class MainViewController : UIViewController
    {
        public MainViewController() : base("MainViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            edtEmail.Text = @"newky2k@mac.com";

            edtEmail.EditingDidEndOnExit += (object sender, EventArgs e) =>
            {

            };

			
            btnGo.TouchUpInside += (object sender, EventArgs e) =>
            {
                imgGravatar.Avatar = new Gravatar(edtEmail.Text);

            };
                
        }
    }
}

