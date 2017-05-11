namespace WeatherApp
{
    public class Weather
    {
        public string Title { get; set; }
        public string Temperature { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Visibility { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
		public string Icon { get; set; }

		public Weather()
        {
            //Because labels bind to these values, set them to an empty string to
            //ensure that the label appears on all platforms by default.
            this.Title = " ";
            this.Temperature = " ";
            this.Wind = " ";
            this.Humidity = " ";
            this.Visibility = " ";
            this.Sunrise = " ";
            this.Sunset = " ";
        }

		public string IconUrl
		{
			get
			{
				if (!string.IsNullOrEmpty(Icon))
				{
					return $"http://openweathermap.org/img/w/{Icon}.png";
				}
				else
				{
					return string.Empty;
				}
			}
		}
	}
}
