using System;
using System.Drawing;

using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace Rouble
{
	public class MainController : UIViewController
	{
		private const string Text = "Цена: 100 ";

		private UILabel _label1;
		private UILabel _label2;
		private UILabel _label3;

		private UIFont _font1;
		private UIFont _font2;
		private UIFont _font3;
		private UIFont _roubleFont;

		public MainController ()
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitFonts ();
			View.BackgroundColor = UIColor.White;

			_label1 = new UILabel ();
			SetText (_label1, _font1, RoubleType.Regular);
			View.AddSubview (_label1);

			_label2 = new UILabel ();
			SetText (_label2, _font2, RoubleType.Medium);
			View.AddSubview (_label2);

			_label3 = new UILabel ();
			SetText (_label3, _font3, RoubleType.Bold);
			View.AddSubview (_label3);
		}

		private void InitFonts()
		{
			_font1 = UIFont.FromName ("HelveticaNeue", 18f);
			_font2 = UIFont.FromName ("HelveticaNeue-Medium", 18f);
			_font3 = UIFont.FromName ("HelveticaNeue-Bold", 18f);
			_roubleFont = UIFont.FromName("Ruble", 18f);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewWillLayoutSubviews ();

			_label1.SizeToFit ();
			_label1.Center = View.Center;
			MoveY (_label1, -50f);

			_label2.SizeToFit ();
			_label2.Center = View.Center;

			_label3.SizeToFit ();
			_label3.Center = View.Center;
			MoveY (_label3, 50f);
		}

		private void SetText(UILabel label, UIFont font, RoubleType type)
		{
			char roubleSym = Roubles.GetRoubleSymbFor (type);

			NSMutableAttributedString attrString = new NSMutableAttributedString();
			attrString.Append(new NSAttributedString(Text, font: font, foregroundColor: UIColor.Black));
			attrString.Append(new NSAttributedString(roubleSym.ToString(), font: _roubleFont, foregroundColor: UIColor.Black));

			label.AttributedText = attrString;
		}

		private void MoveY(UIView view, float dy)
		{
			RectangleF frame = view.Frame;
			frame.Y += dy;
			view.Frame = frame;
		}
	}
}

