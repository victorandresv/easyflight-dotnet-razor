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

        private AmadeusApiSdkTokenModel? token;

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

        public async Task<string> AirportAndCitySearch()
        {
            token = await getToken();
            
            return token.access_token;
        }

        private async Task<AmadeusApiSdkTokenModel> getToken()
        {
            AmadeusApiSdkTokenModel? itoken = null;

            if (AppContext.GetData("token") != null)
            {
                itoken = (AmadeusApiSdkTokenModel)AppContext.GetData("token")!;
                if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3000 > itoken.expires_in)
                {
                    return itoken;
                }
            }

            string url = baseUrl + "/security/oauth2/token?";
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

            itoken = JsonConvert.DeserializeObject<AmadeusApiSdkTokenModel>(data);

            long seconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            itoken!.expires_in = seconds + itoken.expires_in;

            AppContext.SetData("token", itoken);

            return itoken;
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
        public long? expires_in { get; set; }
        public string? state { get; set; }
        public string? scope { get; set; }
    }
}

