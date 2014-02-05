using System;

using MonoTouch.UIKit;
using System.Drawing;

namespace Rouble
{
	public class MainController : UIViewController
	{
		private const string Text = "Цена: 100 {0}";

		private UILabel _label1;
		private UILabel _label2;
		private UILabel _label3;


		public MainController ()
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			_label1 = new UILabel ();
			_label1.Text = Text;
			View.AddSubview (_label1);

			_label2 = new UILabel ();
			_label2.Text = Text;
			View.AddSubview (_label2);

			_label3 = new UILabel ();
			_label3.Text = Text;
			View.AddSubview (_label3);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewWillLayoutSubviews ();

			_label1.SizeToFit ();
			_label1.Center = View.Center;

			_label2.SizeToFit ();
			_label2.Center = View.Center;
			MoveY (_label2, -50f);

			_label3.SizeToFit ();
			_label3.Center = View.Center;
			MoveY (_label3, 50f);
		}

		private void MoveY(UIView view, float dy)
		{
			RectangleF frame = view.Frame;
			frame.Y += dy;
			view.Frame = frame;
		}
	}
}

