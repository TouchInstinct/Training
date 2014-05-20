using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Touchin.SQLiteExtensions
{
	[Register (Name)]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private const string Name = "AppDelegate";

		UIWindow _window;
		UIViewController _viewController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			_window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			_viewController = new MainViewController ();
			_window.RootViewController = new UINavigationController (_viewController);
			_window.MakeKeyAndVisible ();
			
			return true;
		}

		private static void Main (string[] args)
		{
			UIApplication.Main (args, null, Name);
		}
	}
}

