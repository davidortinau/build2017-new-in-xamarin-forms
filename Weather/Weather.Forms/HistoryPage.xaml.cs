using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace Weather.Forms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public const string HistoryItemSelected = "HistoryItemSelected";

		public HistoryPage()
		{
			InitializeComponent();

			HistoryItems.ItemsSource = HistoryRecorder.LocationHistory;
			HistoryItems.ItemTapped += HistoryItemsOnItemTapped;

			BindingContext = this;
		}

		public string PlatformName => $"{Device.RuntimePlatform} ";

		private void HistoryItemsOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
		{
			var historyItem = itemTappedEventArgs.Item as HistoryItem;

			if (historyItem == null)
			{
				return;
			}

			MessagingCenter.Send(this, HistoryItemSelected, historyItem.PostalCode);
		}
	}

	public class HistoryRecorder
	{
		public const string LocationSubmitted = "LocationSubmitted";

		public static HistoryRecorder Instance => _instance ?? (_instance = new HistoryRecorder());
		private static HistoryRecorder _instance;

		public HistoryRecorder()
		{
			MessagingCenter.Subscribe<HistoryRecorder, HistoryItem>(this, LocationSubmitted,
                                                                    (recorder, historyItem) => AddHistory(historyItem));
		}

		public static ObservableCollection<HistoryItem> LocationHistory = new ObservableCollection<HistoryItem>
		{
			new HistoryItem(DateTime.Now.AddHours(-4), "98101", "Seattle", "day200"),
			new HistoryItem(DateTime.Now.AddHours(-3), "94016", "San Francisco", "day800"),
			new HistoryItem(DateTime.Now.AddHours(-2), "63101", "Saint Louis", "day800"),
			new HistoryItem(DateTime.Now.AddHours(-1), "02110", "Boston", "day800"),
			new HistoryItem(DateTime.Now.AddMinutes(-42), "80203", "Denver", "day800"),
		};

        void AddHistory(HistoryItem historyItem)
        {
            if (LocationHistory.Count(x => x.LocationName.ToLower() == historyItem.LocationName.ToLower()) == 0)
            {
                LocationHistory.Add(historyItem);
            }
        }
	}

	public class HistoryItem
	{
		internal HistoryItem(DateTime dateTime, string postalCode, string locationName, string icon)
		{
			DateTime = dateTime;
			PostalCode = postalCode;
			LocationName = locationName;
			Icon = icon;
		}

		public HistoryItem(string postalCode, string locationName, string icon) : this(DateTime.Now, postalCode, locationName, icon)
		{
		}

		public DateTime DateTime { get; set; }
		public string PostalCode { get; set; }
		public string LocationName { get; set; }
		public string Icon { get; set; }

		private string IconToWeatherIcon(string icon)
		{
			if (!icon.StartsWith("day") && !icon.StartsWith("night"))
			{
				icon = "day" + icon;
			}

			return _weatherCodes[icon];
		}

		public string WeatherIcon => IconToWeatherIcon(Icon);

		readonly Dictionary<string, string> _weatherCodes = new Dictionary<string, string>
		{
			{"day200", "\xf010"}, {"day201", "\xf010"}, {"day202", "\xf010"}, {"day210", "\xf005"},
			{"day211", "\xf005"}, {"day212", "\xf005"}, {"day221", "\xf005"}, {"day230", "\xf010"},
			{"day231", "\xf010"}, {"day232", "\xf010"}, {"day300", "\xf00b"}, {"day301", "\xf00b"},
			{"day302", "\xf008"}, {"day310", "\xf008"}, {"day311", "\xf008"}, {"day312", "\xf008"},
			{"day313", "\xf008"}, {"day314", "\xf008"}, {"day321", "\xf00b"}, {"day500", "\xf00b"},
			{"day501", "\xf008"}, {"day502", "\xf008"}, {"day503", "\xf008"}, {"day504", "\xf008"},
			{"day511", "\xf006"}, {"day520", "\xf009"}, {"day521", "\xf009"}, {"day522", "\xf009"},
			{"day531", "\xf00e"}, {"day600", "\xf00a"}, {"day601", "\xf0b2"}, {"day602", "\xf00a"},
			{"day611", "\xf006"}, {"day612", "\xf006"}, {"day615", "\xf006"}, {"day616", "\xf006"},
			{"day620", "\xf006"}, {"day621", "\xf00a"}, {"day622", "\xf00a"}, {"day701", "\xf009"},
			{"day711", "\xf062"}, {"day721", "\xf0b6"}, {"day731", "\xf063"}, {"day741", "\xf003"},
			{"day761", "\xf063"}, {"day762", "\xf063"}, {"day781", "\xf056"}, {"day800", "\xf00d"},
			{"day801", "\xf000"}, {"day802", "\xf000"}, {"day803", "\xf000"}, {"day804", "\xf00c"},
			{"day900", "\xf056"}, {"day902", "\xf073"}, {"day903", "\xf076"}, {"day904", "\xf072"},
			{"day906", "\xf004"}, {"day957", "\xf050"}, {"night200", "\xf02d"}, {"night201", "\xf02d"},
			{"night202", "\xf02d"}, {"night210", "\xf025"}, {"night211", "\xf025"}, {"night212", "\xf025"},
			{"night221", "\xf025"}, {"night230", "\xf02d"}, {"night231", "\xf02d"}, {"night232", "\xf02d"},
			{"night300", "\xf02b"}, {"night301", "\xf02b"}, {"night302", "\xf028"}, {"night310", "\xf028"},
			{"night311", "\xf028"}, {"night312", "\xf028"}, {"night313", "\xf028"}, {"night314", "\xf028"},
			{"night321", "\xf02b"}, {"night500", "\xf02b"}, {"night501", "\xf028"}, {"night502", "\xf028"},
			{"night503", "\xf028"}, {"night504", "\xf028"}, {"night511", "\xf026"}, {"night520", "\xf029"},
			{"night521", "\xf029"}, {"night522", "\xf029"}, {"night531", "\xf02c"}, {"night600", "\xf02a"},
			{"night601", "\xf0b4"}, {"night602", "\xf02a"}, {"night611", "\xf026"}, {"night612", "\xf026"},
			{"night615", "\xf026"}, {"night616", "\xf026"}, {"night620", "\xf026"}, {"night621", "\xf02a"},
			{"night622", "\xf02a"}, {"night701", "\xf029"}, {"night711", "\xf062"}, {"night721", "\xf0b6"},
			{"night731", "\xf063"}, {"night741", "\xf04a"}, {"night761", "\xf063"}, {"night762", "\xf063"},
			{"night781", "\xf056"}, {"night800", "\xf02e"}, {"night801", "\xf022"}, {"night802", "\xf022"},
			{"night803", "\xf022"}, {"night804", "\xf086"}, {"night900", "\xf056"}, {"night902", "\xf073"},
			{"night903", "\xf076"}, {"night904", "\xf072"}, {"night906", "\xf024"}, {"night957", "\xf050"}
		};
	}
}