// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using System.CodeDom.Compiler;

#if __UNIFIED__
using UIKit;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif


namespace GravatarMobileForiOS.ViewControllers
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCircle { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLoad { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSquare { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField edtEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		GravatarMobile.iOS.GravatarView imgGravatar { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCircle != null) {
				btnCircle.Dispose ();
				btnCircle = null;
			}
			if (btnLoad != null) {
				btnLoad.Dispose ();
				btnLoad = null;
			}
			if (btnSquare != null) {
				btnSquare.Dispose ();
				btnSquare = null;
			}
			if (edtEmail != null) {
				edtEmail.Dispose ();
				edtEmail = null;
			}
			if (imgGravatar != null) {
				imgGravatar.Dispose ();
				imgGravatar = null;
			}
		}
	}
}
