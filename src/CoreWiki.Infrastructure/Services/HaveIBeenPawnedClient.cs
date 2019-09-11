namespace CoreWiki.Infrastructure.Services
{
    using System;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Domain.Logger;
    using Domain.Services;

    public class HaveIBeenPawnedClient : IHaveIBeenPawnedClient
    {
        private readonly HttpClient client;
        private readonly Uri baseUri = new Uri("https://api.pwnedpasswords.com/range/");

        public HaveIBeenPawnedClient(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = this.baseUri;
            client.DefaultRequestHeaders.Add("api-version", "2");
            client.DefaultRequestHeaders.Add("User-Agent", "PwnedClient.Net");
        }

        /// <summary>
        /// Returns number if hits password has in HIBP db
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<int> GetHitsPlainAsync(string password)
        {
            var hash = Sha1Hash(password);

            return await this.GetHitsAsync(hash);
        }

        /// <summary>
        /// Returns number if hits hash has in HIBP db
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        public async Task<int> GetHitsAsync(string hashedPassword)
        {
            var res = await this.CallApiAsync(hashedPassword);

            // Find hash in result
            var regex = new Regex($"({hashedPassword.Substring(5)})[:](\\d+)");
            var matces = regex.Matches(res);

            if (matces.Count == 0)
            {
                return 0;
            }
            var t = matces[0].Groups[2].Value;

            if (int.TryParse(t, out var val))
            {
                return val;
            }

            return 0;
        }

        /// <summary>
        /// Queries HIBP with the first 5 chars of the provided sha1 string
        /// </summary>
        /// <param name="sha1"></param>
        /// <returns></returns>
        private async Task<string> CallApiAsync(string sha1)
        {
            var query = FirstFive(sha1);
            try
            {
                var response = await this.client.GetAsync(query);
                var results = await response.Content.ReadAsStringAsync();
                return results;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Sha1Hash(string input)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (var b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private static string FirstFive(string input) => input.Substring(0, 5);
    }
}