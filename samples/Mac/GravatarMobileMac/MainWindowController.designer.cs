// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace GravatarMobileMac
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		GravatarMobileMac.Views.MacGravatarView grvAvatar { get; set; }

		[Outlet]
		AppKit.NSTextField txtEmail { get; set; }

		[Action ("OnClickedCircle:")]
		partial void OnClickedCircle (AppKit.NSButton sender);

		[Action ("OnClickedLoad:")]
		partial void OnClickedLoad (AppKit.NSButton sender);

		[Action ("OnClickedSquare:")]
		partial void OnClickedSquare (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (grvAvatar != null) {
				grvAvatar.Dispose ();
				grvAvatar = null;
			}

			if (txtEmail != null) {
				txtEmail.Dispose ();
				txtEmail = null;
			}
		}
	}
}
