using System;

using Foundation;
using AppKit;
using GravatarMobile.Core;
using GravatarMobile.Core.Data;

namespace GravatarMobileMac
{
	public partial class MainWindowController : NSWindowController
	{
		public MainWindowController(IntPtr handle) : base(handle)
		{
		}

		[Export("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder)
		{
		}

		public MainWindowController() : base("MainWindow")
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			grvAvatar.ViewStyle = Enums.GravatarViewStyle.Round;
			grvAvatar.BorderWidth = 2.0f;



		}

		public new MainWindow Window
		{
			get { return (MainWindow)base.Window; }
		}

		partial void OnClickedLoad(AppKit.NSButton sender)
		{
			try
			{
				grvAvatar.Avatar = new Gravatar(txtEmail.StringValue);
			}
			catch (Exception ex)
			{
				Console.WriteLine("");
			}
		}

		partial void OnClickedCircle(AppKit.NSButton sender)
		{
			grvAvatar.BorderWidth = 5.0f;
			grvAvatar.BorderColor = NSColor.White;
			grvAvatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Round;
		}

		partial void OnClickedSquare(AppKit.NSButton sender)
		{
			grvAvatar.BorderWidth = 2.0f;
			grvAvatar.BorderColor = NSColor.Green;
			grvAvatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Square;
		}
	}
}
