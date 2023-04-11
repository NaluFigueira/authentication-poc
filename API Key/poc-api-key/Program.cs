using poc_api_key.Infra;

#region [Generate ApiKey]

var secret =
  "46070D4BF934FB0D4B06D9E2C46E346944E322444900A435D7D9A95E6D7435F5";

var salt = $"4E32244490{DateTime.Now.Hour - 1}";

var apiKey = ApiKey.GenerateApiKey(secret, salt);

Console.WriteLine("API Key: " + apiKey);

#endregion

#region [Send ApiKey]

var client = new HttpClient();
client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
var response = client.GetAsync("http://localhost:5051/").Result;

Console.WriteLine("Response HTTP status code:" + response.StatusCode);

#endregion