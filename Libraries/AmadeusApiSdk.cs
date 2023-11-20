using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace easyflight_mvc_dotnet_razor.Libraries
{
	public class AmadeusApiSdk
	{

		private HttpClient httpClient;
		private string apiKey;
		private string apiSecret;
		private string baseUrlDev = "https://test.api.amadeus.com";
        private string baseUrl = "https://test.api.amadeus.com";
		private string baseUrlVersion = "/v1";

        private AmadeusApiSdkTokenModel token;

        public AmadeusApiSdk(string apiKey, string apiSecret)
		{
			httpClient = new HttpClient();
			this.apiKey = apiKey;
			this.apiSecret = apiSecret;

			baseUrl = baseUrlDev + baseUrlVersion;

            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

		private async Task<string> getToken()
		{ 
			string url = baseUrl+"/security/oauth2/token?";
            url += "grant_type=client_credentials&client_id=" + apiKey + "&client_secret=" + apiSecret;
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            HttpResponseMessage response = await httpClient.PostAsync(url, new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", apiKey),
                new KeyValuePair<string, string>("client_secret", apiSecret)
            }));
            string data = await response.Content.ReadAsStringAsync();

            token = JsonConvert.DeserializeObject<AmadeusApiSdkTokenModel>(data);
            System.Diagnostics.Debug.WriteLine(token.access_token);

            return data;
        }

        public async Task<string> AirportAndCitySearch()
		{
			return await getToken();

			//System.Diagnostics.Debug.WriteLine(httpResponseMessage.);

            //HttpResponseMessage response = await httpClient.GetAsync("/reference-data/locations");
        }
	}

    public class AmadeusApiSdkTokenModel
    {
        public string? type { get; set; }
        public string? username { get; set; }
        public string? application_name { get; set; }
        public string? client_id { get; set; }
        public string? token_type { get; set; }
        public string? access_token { get; set; }
        public string? expires_in { get; set; }
        public string? state { get; set; }
        public string? scope { get; set; }
    }
}

