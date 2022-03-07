using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class ConnectApi
    {
        HttpClient client = default;
        string apiKey = string.Empty;
        public ConnectApi(string apiKey)
        {
            this.apiKey = apiKey;
            client = new HttpClient();
        }

        public async Task OutPutInfo()
        {
            Console.WriteLine("The name of city");
            string city = Console.ReadLine().Trim();
            string responseString = string.Empty;
            try
            {
                responseString = await client.GetStringAsync($@"http://api.openweathermap.org/data/2.5/weather?q=" + city + "&APPID=" + apiKey);
            }
            catch
            {
                Console.WriteLine("Data not found");
                return;
            }
            var currentObject = WeatherFull.FromJson(responseString);
            Console.WriteLine("The temparature scale (Fahrenheit or Celsius)");
            switch (Console.ReadLine().Trim().ToLower())
            {
                case "fahrenheit":
                    PrintData(currentObject, "fahrenheit");
                    break;
                case "celsius":
                    PrintData(currentObject, "celsius");
                    break;
                default:
                    Console.WriteLine("String scale is not valid");
                    break;
            }

        }

        void PrintData(WeatherFull weather, string scale)
        {
            Console.WriteLine("=======================");
            var temperature = scale == "celsius" ? GetCelciusFromCelvin(weather.Main.Temp) : GetFahrenheitFromCelvin(weather.Main.Temp);
            var temperatureFeels = scale == "celsius" ? GetCelciusFromCelvin(weather.Main.FeelsLike) : GetFahrenheitFromCelvin(weather.Main.FeelsLike);
            Console.WriteLine($"City: {weather.Name}");
            Console.WriteLine($"Weather: {weather.Weather[0].Main}({weather.Weather[0].Description})");
            Console.WriteLine($"Temperature({scale}): {Math.Round(temperature, 1)}");
            Console.WriteLine($"Temperature feels like({scale}): {Math.Round(temperatureFeels, 1)}");
        }
        public static double GetCelciusFromCelvin(double temperature) => temperature - 273;
        public static double GetFahrenheitFromCelvin(double temperature) => GetCelciusFromCelvin(temperature) * 18 / 10 + 32;

    }
}
