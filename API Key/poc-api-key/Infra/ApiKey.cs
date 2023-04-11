using System;
using System.Security.Cryptography;
using System.Text;

namespace poc_api_key.Infra
{
	public static class ApiKey
	{
		public static string GenerateApiKey(string secret, string salt)
		{
            // Generate random number to use as nonce
            var nonce = Guid.NewGuid().ToString("N");

            // Concatenate nonce with salt in a single value
            var data = $"{nonce}-{salt}";

            // Convert a secret key to bytes
            var secretBytes = Encoding.UTF8.GetBytes(secret);

            // Genrate secret key with a hash SHA256 and nonce-sal
            using (var hmac = new HMACSHA256(secretBytes))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));

                // Concatenate nonce and hash in a single value
                return $"{nonce}-{Convert.ToBase64String(hash)}";
            }
        }
	}
}

