using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GravatarMobile.Droid;
using GravatarMobile.Core;

namespace GravatarMobile_Android
{
	[Activity(Label = "GravatarMobile_Android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		EditText edtEmail;
		GravatarView imgGravatar;

		int count = 1;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			edtEmail = this.FindViewById<EditText>(Resource.Id.edtEmail);

			edtEmail.Text = String.Empty;

			imgGravatar = this.FindViewById<GravatarView>(Resource.Id.imgGravatar);
			var btnLoad = this.FindViewById<Button>(Resource.Id.btnLoad);
			var btnSquare = this.FindViewById<Button>(Resource.Id.btnSquare);
			var btnCircle = this.FindViewById<Button>(Resource.Id.btnCircle);

			btnLoad.Click += (object sender, EventArgs e) => 
			{
				try
				{
					imgGravatar.Avatar = new Gravatar(edtEmail.Text);
				}
				catch (Exception ex)
				{
					var dlgAlert = (new AlertDialog.Builder (this)).Create ();
					dlgAlert.SetMessage (ex.Message);
					dlgAlert.SetTitle ("Alert Dialog");
					dlgAlert.SetButton("OK",handllerNotingButton);
					dlgAlert.Show ();
				}
			};

			btnCircle.Click += (object sender, EventArgs e) => 
			{
				imgGravatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Round;
			};

			btnSquare.Click += (object sender, EventArgs e) => 
			{
				imgGravatar.ViewStyle = GravatarMobile.Core.Data.Enums.GravatarViewStyle.Square;
			};
		}

		void handllerNotingButton (object sender, DialogClickEventArgs e)
		{

		}
	}
}


