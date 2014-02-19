using System;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

using MonoTouch.FacebookConnect;
using Touch.Common;

namespace Social
{
	public class MainController : UIViewController
	{
		private UIButton _fbShare;

		public MainController ()
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			_fbShare = new UIButton ();
			_fbShare.TouchUpInside += OnLoginClicked;
			_fbShare.SetTitle("Share in Facebook", UIControlState.Normal);
			_fbShare.SetTitleColor (UIColor.White, UIControlState.Normal);
			_fbShare.BackgroundColor = UIColor.Blue;

			_fbShare.SizeToFit ();


			View.AddSubview (_fbShare);
			_fbShare.Begin().MoveY(50).LMargin(20).Commit();
		}

		private void OnLoginClicked (object sender, EventArgs e)
		{
			FBShareDialogParams shareParams = new FBShareDialogParams ()
			{
				Link = NSUrl.FromString("http://touchin.ru/"),
				Name = "Touch instinct",
				Caption = "Greatest mobile apps for you",
				Description = "Touch instinct team work for you"
			};

			if (FBDialogs.CanPresentShareDialog (shareParams))
				PresentFacebookShareDialog (shareParams);
			else
				;
		}

		private void PresentFacebookShareDialog(FBShareDialogParams shareParams)
		{
			FBDialogs.PresentShareDialog(shareParams, null, FBDialogAppCallCompletion);
		}

		private void FBDialogAppCallCompletion(FBAppCall call, NSDictionary results, NSError error)
		{
			// review results for fething additonal info such completed or canceled, post id etc.
			// for more info https://developers.facebook.com/docs/ios/share#linkshare

			if (error == null)
				return;

			Console.WriteLine (error.Code);
			Console.WriteLine (error.DebugDescription);
			Console.WriteLine (error.Description);
			Console.WriteLine (error.Domain);
			Console.WriteLine (error.UserInfo);
			Console.WriteLine (error);
			Console.WriteLine (call);
		}
	}
}