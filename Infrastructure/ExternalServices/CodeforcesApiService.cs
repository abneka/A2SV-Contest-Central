using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.ExternalServices
{
    public static class CodeforcesApiService
    {
        public static CodeforcesAPISettings codeforcesAPISettings { get; set; } = null!;
        public static HttpClient httpClient { get; set; } = null!;

        public static async Task<dynamic> GetContestData(string contestId)
        {
            string apiKey = codeforcesAPISettings.ApiKey;
            string apiSecret = codeforcesAPISettings.ApiSecret;
            string randStr = GenerateRandomString();
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            string apiSig = $"{randStr}/contest.standings?apiKey={apiKey}&contestId={contestId}&time={currentTime}#{apiSecret}";
            string hash = CalculateSHA512(apiSig);

            string url = $"https://codeforces.com/api/contest.standings?contestId={contestId}&apiKey={apiKey}&time={currentTime}&apiSig={randStr}{hash}";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            string responseData = await response.Content.ReadAsStringAsync();

            dynamic data = JsonConvert.DeserializeObject(responseData)!;
            return data; 
        }

        private static string GenerateRandomString()
        {
            Random random = new Random();
            int rand = random.Next(100000);
            return rand.ToString("D6");
        }

        public static string CalculateSHA512(string input)
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

        public static async Task<bool> IsHandleValid(string handle)
        {
            HttpClient client = new HttpClient();
            string url = $"https://codeforces.com/api/user.info?handles={handle}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                dynamic userInfo = JsonConvert.DeserializeObject<dynamic>(result)!;
                
                // Return true if the status is OK, otherwise return false
                return userInfo.status == "OK";
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }


    }
}


