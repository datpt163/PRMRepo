using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Shopee
{
    public class ShopAuth
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string host = "https://partner.test-stable.shopeemobile.com";
        private static readonly string partnerKey = "546874517078584b706e53624c4c5a616d524d674b7a69555a4a4f576b77506f";
        private static readonly long partnerId = 1180548;
        private static readonly long shopId = 93287;

        public static async Task<string> AddItemAsync()
        {
            string accessToken = "77466f6176546b47736575614c6e6742";
            string path = "/api/v2/product/add_item";
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string baseString = $"{partnerId}{path}{timestamp}{accessToken}{shopId}";

            string sign = GenerateSignature(baseString, partnerKey);

            string url = $"{host}{path}?partner_id={partnerId}&timestamp={timestamp}&access_token={accessToken}&shop_id={shopId}&sign={sign}";

            var payload = new
            {
                original_price = 123.3,
                description = "item description test",
                weight = 1.1,
                item_name = "Item Name Example",
                category_id = 14695,
                image = new
                {
                    image_id_list = new[] { "-" }
                },
                logistic_info = new[]
    {
        new
        {
            size_id = 0,
            shipping_fee = 23.12,
            enabled = true,
            logistic_id = 80101,
            is_free = true
        }
    },
                attribute_list = new[]
    {
        new
        {
            attribute_id = 4990,
            attribute_value_list = new[]
            {
                new
                {
                    value_id = 32142,
                    original_value_name = "Brand",
                    value_unit = " kg"
                }
            }
        }
    }
            };


            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }

        private static string GenerateSignature(string baseString, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] baseStringBytes = Encoding.UTF8.GetBytes(baseString);

            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(baseStringBytes);
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }

        public static string GetLoginLink()
        {
            long timest = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string host = "https://partner.test-stable.shopeemobile.com";
            string path = "/api/v2/shop/auth_partner";
            string redirectUrl = "https://tradeplatform.coedu.vn";
            long partnerId = 1180548;
            string tmpPartnerKey = "546874517078584b706e53624c4c5a616d524d674b7a69555a4a4f576b77506f";
            string tmpBaseString = $"{partnerId}{path}{timest}";
            string sign = "";

            try
            {
                byte[] baseString = Encoding.UTF8.GetBytes(tmpBaseString);
                byte[] partnerKey = Encoding.UTF8.GetBytes(tmpPartnerKey);
                using (HMACSHA256 hmac = new HMACSHA256(partnerKey))
                {
                    byte[] hashValue = hmac.ComputeHash(baseString);
                    sign = BitConverter.ToString(hashValue).Replace("-", "").ToLower();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string url = $"{host}{path}?partner_id={partnerId}&timestamp={timest}&sign={sign}&redirect={redirectUrl}";
            return url;
        }

        public static async Task<string> GetAccessTokenAsync(string code, long shopIdToken)
        {
            string path = "/api/v2/auth/token/get";
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string baseString = $"{partnerId}{path}{timestamp}";

            string sign = GenerateSignature(baseString, partnerKey);

            string url = $"{host}{path}?partner_id={partnerId}&timestamp={timestamp}&sign={sign}";

            var payload = new
            {
                code = code,
                partner_id = partnerId,
                shop_id = shopIdToken
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();

                var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
                await Console.Out.WriteLineAsync(responseContent);

                return responseData?.access_token ?? responseContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

    }
}
