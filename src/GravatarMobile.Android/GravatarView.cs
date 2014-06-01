
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GravatarMobile.Core.Interfaces;
using GravatarMobile.Core;

namespace GravatarMobile.Android
{
	/// <summary>
	/// Android Gravatar View
	/// </summary>
	public class GravatarView : ImageView, IGravatarView
	{
		#region Fields

		private Gravatar mItem;

		#endregion
		#region Properties
		public Gravatar Avatar
		{
			get
			{
				return mItem;
			}
			set
			{
				mItem = value;
			}
		}
		#endregion

		#region Constructors
		public GravatarView(Context context) :
			base(context)
		{
			Initialize();
		}

		public GravatarView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			Initialize();
		}

		public GravatarView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			Initialize();
		}

		void Initialize()
		{

		}

		#endregion
	}
}

