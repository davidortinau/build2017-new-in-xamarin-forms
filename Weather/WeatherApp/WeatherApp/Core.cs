using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            //Sign up for a free API key at http://openweathermap.org/appid
            string key = Api.Key;
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
                + zipCode + ",&appid=" + key;

            //Make sure developers running this sample replaced the API key
            if (key == "YOUR API KEY HERE")
            {
                throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
            }

            var results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                weather.Temperature = ConvertToFahrenheit((string)results["main"]["temp"]) + " F";
                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";
				weather.Icon = (string)results["weather"][0]["id"];
				return weather;
            }
            else
            {
                return null;
            }
        }

	    private static string ConvertToFahrenheit(string temp)
	    {
		    float kelvin;
		    if (float.TryParse(temp, out kelvin))
		    {
			    return CelciusToFahrenheit(KelvinToCelcius(kelvin)).ToString();
		    }

		    return string.Empty;
	    }

	    private static float KelvinToCelcius(float kelvin)
	    {
			return kelvin - 273;
	    }

	    private static float CelciusToFahrenheit(float celcius)
	    {
		    return ((celcius * 9) / 5) + 32;
	    }
    }
}