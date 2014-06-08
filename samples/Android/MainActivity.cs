using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GravatarMobile.Android;
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
			imgGravatar = this.FindViewById<GravatarView>(Resource.Id.imgGravatar);
			var btnLoad = this.FindViewById<Button>(Resource.Id.btnLoad);

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

		}

		void handllerNotingButton (object sender, DialogClickEventArgs e)
		{

		}
	}
}


