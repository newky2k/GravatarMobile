// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace GravatarMobile_iOS.ViewControllers
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnGo { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField edtEmail { get; set; }

		[Outlet]
		GravatarMobile.iOS.GravatarView imgGravatar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnGo != null) {
				btnGo.Dispose ();
				btnGo = null;
			}

			if (imgGravatar != null) {
				imgGravatar.Dispose ();
				imgGravatar = null;
			}

			if (edtEmail != null) {
				edtEmail.Dispose ();
				edtEmail = null;
			}
		}
	}
}
