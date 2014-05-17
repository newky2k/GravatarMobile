using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GravatarMobile.Core;

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
			
            btnGo.TouchUpInside += async (object sender, EventArgs e) =>
            {
                var newAvatar = new Gravatar(@"newky2k@mac.com");

                var aString = newAvatar.Hash;

                Console.WriteLine(newAvatar.Hash);

                await newAvatar.LoadImage();

                var img = UIImage.LoadFromData(NSData.FromStream(newAvatar.ImageAsStream));

                imgGravatar.Image = img;

            };

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}

