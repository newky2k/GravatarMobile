
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
using Android.Graphics;
using GravatarMobile.Core.Data;

namespace GravatarMobile.Droid
{
	/// <summary>
	/// Android Gravatar View
	/// </summary>
	public class GravatarView : ImageView, IGravatarView
	{
		#region Fields

		private Gravatar mItem;
		private Enums.GravatarViewStyle mStyle = Enums.GravatarViewStyle.Square;

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
				if (mItem != value)
				{
					mItem = value;

					LoadImage();


				}
			}
		}

		/// <summary>
		/// Gets or sets the view style.
		/// </summary>
		/// <value>The view style.</value>
		public Enums.GravatarViewStyle ViewStyle
		{
			get
			{
				return mStyle;
			}
			set
			{
				if (mStyle != value)
				{
					mStyle = value;

					this.Invalidate();
				}
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

		private void Initialize ()
		{

		}

		#endregion

		private async void LoadImage()
		{

			var size = (this.Width > 80) ? this.Width : 80;
		

			await mItem.LoadImage((int)size);

			this.Post(()=>
			{
				this.SetImageDrawable(mItem.ImageAsDrawable());
			});

		}

		public override void Draw (Android.Graphics.Canvas canvas)
		{
			if (mStyle == Enums.GravatarViewStyle.Round)
			{
				Path path = new Path();
				path.AddOval(new RectF(0,0, Width,Height),Path.Direction.Cw);
				canvas.ClipPath(path);
				canvas.DrawFilter = new PaintFlagsDrawFilter((PaintFlags)1, PaintFlags.AntiAlias);


			}

			base.Draw (canvas);

		}

		/**
     *  try to add antialiasing.
             */
		protected override void DispatchDraw (Canvas canvas)
		{
			if (mStyle == Enums.GravatarViewStyle.Round)
			{
				canvas.DrawFilter = new PaintFlagsDrawFilter((PaintFlags)1, PaintFlags.AntiAlias);

			}

			base.DispatchDraw (canvas);
		}
	}
}

