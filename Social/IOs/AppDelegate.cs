using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MonoTouch.FacebookConnect;

namespace Social
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private UIWindow window;
		private MainController _mainController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			_mainController = new MainController ();

			window.RootViewController = _mainController;
			window.MakeKeyAndVisible ();

			return true;
		}

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			return FBAppCall.HandleOpenURL (url, sourceApplication);
		}
	}
}

