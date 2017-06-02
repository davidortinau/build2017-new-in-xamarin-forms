using CoreGraphics;
using Foundation;
using UIKit;
using Weather.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace WeatherApp.iOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public static AppDelegate Shared;
		public static UIStoryboard Storyboard = UIStoryboard.FromName("Main", null);

		UIWindow _window;
		UINavigationController _navigation;
		UIBarButtonItem _aboutButton;
		ViewController _weatherController;
        UIViewController _historyViewController;

		public UIBarButtonItem CreateAboutButton()
		{
			if (_aboutButton == null)
			{
				var btn = new UIButton(new CGRect(0, 0, 88, 44));
				btn.SetTitle("History", UIControlState.Normal);
				btn.TouchUpInside += (sender, e) => ShowHistory();

				_aboutButton = new UIBarButtonItem(btn);
			}
			return _aboutButton;
		}

		public void ShowHistory()
		{
			if (_historyViewController == null)
			{
                // #2 Use it
				_historyViewController = new HistoryPage().CreateViewController();
			}

			// And push it onto the navigation stack
			_navigation.PushViewController(_historyViewController, true);
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
            // #1 Initialize
            Forms.Init();

			Shared = this;
			_window = new UIWindow(UIScreen.MainScreen.Bounds);

			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
			{
				TextColor = UIColor.White
			});


            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.BarTintColor = Color.FromHex("#002050").ToUIColor();
            UINavigationBar.Appearance.TintColor = UIColor.White;

			_weatherController = Storyboard.InstantiateInitialViewController() as ViewController;
			_navigation = new UINavigationController(_weatherController);
            
			MessagingCenter.Subscribe<HistoryPage, string>(this, HistoryPage.HistoryItemSelected, (history, postalCode) =>
			{
				_navigation.PopToRootViewController(true);
				_weatherController.SetPostalCode(postalCode);
			});

			_window.RootViewController = _navigation;
			_window.MakeKeyAndVisible();
			
			return true;
		}


	}
}


