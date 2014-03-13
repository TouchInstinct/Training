using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Share_Android
{
	[Activity(Label = "Share_Android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate
			{

				Intent sendIntent = new Intent();
				sendIntent.SetAction(Intent.ActionSend);

				sendIntent.PutExtra(
					Intent.ExtraText,
					"#tag and a link: http://media.desura.com/images/members/1/398/397709/124316909678.png");

				sendIntent.SetType("text/plain");

				StartActivity(Intent.CreateChooser(sendIntent, "Send to.."));

			};
		}
	}
}


