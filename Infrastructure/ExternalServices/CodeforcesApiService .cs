using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using Application.Contracts.Infrastructure.ExternalServices;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.ExternalServices
{
    public class CodeforcesApiService : ICodeforcesApiService
    {
        private readonly CodeforcesAPISettings _codeforcesAPISettings;
        private readonly HttpClient _httpClient;
        public CodeforcesApiService(IOptions<CodeforcesAPISettings> codeforcesAPISettings, HttpClient httpClient)
        {
            _codeforcesAPISettings = codeforcesAPISettings.Value;
            _httpClient = httpClient;
        }

        public async Task MakeApiRequest(string contestId, string method)
        {
            String apiKey = _codeforcesAPISettings.ApiKey;
            string apiSecret = _codeforcesAPISettings.ApiSecret;
            Random random = new Random();
            int rand = random.Next(100000);
            string randStr = rand.ToString("D6");

            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string currentTimeStr = currentTime.ToString();

            string apiSig =
                $"{randStr}/contest.{method}?apiKey={apiKey}&contestId={contestId}&time={currentTime}#{apiSecret}";
            string hash = CalculateSHA512(apiSig);

            string url =
                $"https://codeforces.com/api/contest.{method}?contestId={contestId}&apiKey={apiKey}&time={currentTime}&apiSig={randStr}{hash}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject(responseData);

                string filename = "data.json";
                File.WriteAllText(filename, JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }

        static string CalculateSHA512(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
