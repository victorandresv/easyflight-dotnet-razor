using System.Net.Http.Headers;

namespace easyflight_mvc_dotnet_razor.Libraries
{
	public class AmadeusApiSdk
	{

		private HttpClient httpClient;
		private String apiKey;
		private String apiSecret;
		private String baseUrlDev = "https://test.api.amadeus.com/v1";

        public AmadeusApiSdk(String apiKey, String apiSecret)
		{
			httpClient = new HttpClient();
			this.apiKey = apiKey;
			this.apiSecret = apiSecret;

            httpClient.BaseAddress = new Uri(baseUrlDev);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

		private async void GetToken()
		{
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            HttpResponseMessage response = await httpClient.GetAsync("/security/oauth2/token");
        }

        public async void AirportAndCitySearch()
		{
            HttpResponseMessage response = await httpClient.GetAsync("/reference-data/locations");
        }
	}
}

