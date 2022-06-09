namespace PollenApi.Models
{
    public class Forecast
    {
        public List<ForecastDay> forecast { get; set; }
    }
    public class ForecastDay
    {
        public string date { get; set; }
        public ForecastDayData dayData { get; set; }

        public class ForecastDayData
        {
            public float maxtemp_c { get; set; }
            public float maxtemp_f { get; set; }
            public float mintemp_c { get; set; }
            public float mintemp_f { get; set; }
            public float avgtemp_c { get; set; }
            public float avgtemp_f { get; set; }
        }

    }
    
}
