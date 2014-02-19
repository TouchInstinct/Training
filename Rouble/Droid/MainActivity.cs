using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Rouble
{
	[Activity(Label = "Rouble Demo", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			FindViewById<TextView>(Resource.Id.textView1).SetTextWithRouble("Обычная цена: 100.50 ", RoubleType.Regular);
			FindViewById<TextView>(Resource.Id.textView2).SetTextWithRouble("Жирная цена: 10 000 ", RoubleType.Bold);
		}
	}
}
