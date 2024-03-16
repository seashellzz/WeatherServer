namespace WeatherServer.Dto
{
    public class CountryPopulation
    {
        public int countryId { get; set; }
        public required string countryName { get; set; }

        public int population { get; set; }
    }
}
