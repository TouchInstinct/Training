using System;

using MonoTouch.UIKit;
using System.Drawing;
using System.Reflection;

namespace RefCycles
{
	public class MainController : UIViewController
	{
		private UIButton _button;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.White;

			_button = new UIButton (new RectangleF(10f, 30f, 0f, 0f));
			_button.TouchUpInside += HandleTouchUpInside;
			_button.SetTitle ("click me", UIControlState.Normal);
			_button.SetTitleColor (UIColor.Blue, UIControlState.Normal);
			_button.SizeToFit ();

			View.AddSubview (_button);
		}

		private void HandleTouchUpInside (object sender, EventArgs e)
		{
			Console.WriteLine ("HandleTouchUpInside");

			/*
			// Dont leak
			UIView v1 = new UIView ();
			UIView v2 = new UIView ();
			*/

			/*
			// Dont leak
			UIView v1 = new DebugView ();
			UIView v2 = new UIView ();
			*/

			/*
			// Leak
			UIView v1 = new UIView ();
			UIView v2 = new DebugView ();
			*/

			// Leak
			UIView v1 = new DebugView ();
			UIView v2 = new DebugView ();


			v1.AddSubview (v2);
			View.AddSubview (v1);

			var sv = v2.Superview;			// cause of leak
			//v2.ResetSuperViewChache ();	// uncomment to fix 

			v1.RemoveFromSuperview ();
		}
	}

	public class DebugView: UIView
	{
	}

	public static class Ext
	{
		public static void ResetSuperViewChache(this UIView view)
		{
			Type t = typeof(UIView);
			FieldInfo fInfo =  t.GetField("__mt_Superview_var", BindingFlags.NonPublic | BindingFlags.Instance);
			fInfo.SetValue (view, null);
		}
	}
}