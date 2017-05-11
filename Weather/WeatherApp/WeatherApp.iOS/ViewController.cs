using System;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;
using Weather.Forms;
using Xamarin.Forms;

namespace WeatherApp.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
            base.ViewDidLoad();
			this.Title = "Weather";
            this.weatherBtn.Layer.BorderColor = UIColor.White.CGColor;
            this.weatherBtn.Layer.BorderWidth = 1;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NavigationItem.RightBarButtonItem = AppDelegate.Shared.CreateAboutButton();
		}

		public async void SetPostalCode(string postalCode)
		{
			zipCodeEntry.Text = postalCode;
			await GetWeather();
		}

		private async Task GetWeather()
		{
			if (!String.IsNullOrEmpty(this.zipCodeEntry.Text))
			{
				Weather weather = await Core.GetWeather(zipCodeEntry.Text);
				if (weather != null)
				{
					locationText.Text = weather.Title;
					tempText.Text = weather.Temperature;
					windText.Text = weather.Wind;
					visibilityText.Text = weather.Visibility;
					humidityText.Text = weather.Humidity;
					sunriseText.Text = weather.Sunrise;
					sunsetText.Text = weather.Sunset;

					// Let the history tracker know that the user just successfully looked up a postal code
					var item = new HistoryItem(zipCodeEntry.Text, weather.Title, weather.Icon);
					MessagingCenter.Send(HistoryRecorder.Instance, HistoryRecorder.LocationSubmitted, item);
				}
			}
		}

		async partial void GetWeatherBtn_Click(UIButton sender)
		{
			await GetWeather();
		}
	}
}

