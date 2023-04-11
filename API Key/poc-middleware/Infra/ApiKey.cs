using System;
using System.Security.Cryptography;
using System.Text;

namespace poc_middleware.Infra
{
    public static class ApiKey
    {
        public static bool VerifyApiKey(string apiKey)
        {
            var secret = "46070D4BF934FB0D4B06D9E2C46E346944E322444900A435D7D9A95E6D7435F5";
            var salt = $"4E32244490{DateTime.Now.Hour - 1}";


            // Divide a API Key em seu nonce e hash
            var parts = apiKey.Split('-');
            if (parts.Length != 2)
            {
                return false;
            }

            // Recupera o nonce e o hash da API Key
            var nonce = parts[0];
            var hash = Convert.FromBase64String(parts[1]);

            // Concatena o nonce e o sal em um único valor
            var data = $"{nonce}-{salt}";

            // Converte a chave secreta em bytes
            var secretBytes = Encoding.UTF8.GetBytes(secret);

            // Gera um hash SHA256 da chave secreta e do valor nonce-sal
            using (var hmac = new HMACSHA256(secretBytes))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));

                // Verifica se o hash recebido é o mesmo que o hash gerado
                return hash.SequenceEqual(computedHash);
            }
        }
    }
}

