using System;
using AppKit;
using Foundation;
using GravatarMobile.Mac;

namespace GravatarMobileMac.Views
{
	[Register("MacGravatarView")]
	public class MacGravatarView : GravatarView
	{
		public MacGravatarView(IntPtr ptr) 
			: base(ptr)
		{

		}
	}
}

