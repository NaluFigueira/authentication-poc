// See https://aka.ms/new-console-template for more information

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ3ZWJzaXRlLmNvbS5iciIsImlhdCI6IjExLzA0LzIwMjMsIDE5OjIxOjQ5IiwiZXhwIjoiMS42ODEuMjU0LjA0OS45ODMiLCJhY2wiOlsidGVhY2hlciJdLCJ1c2VybmFtZSI6IlJheXNzYSBkYSBDb3N0YSIsImVtYWlsIjoicmF5c3NhX3NhYnJpbmFfZGFjb3N0YUBiYWx0aWNvLmNvbS5iciJ9.W8m1kkptZq321Kh7E-E9EXiaM-YofKJXzdnlmI4w_VE";
string secret = "46070D4BF934FB0D4B06D9E2C46E346944E322444900A435D7D9A95E6D7435F5";

var handler = new JwtSecurityTokenHandler();

var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

var validationParameters = new TokenValidationParameters
                           {
                               ValidateIssuer = false,
                               ValidateAudience = false,
                               ValidateLifetime = false,
                               ValidateIssuerSigningKey = true,
                               RequireExpirationTime = true,
                               IssuerSigningKey = securityKey
                           };

try
{
    SecurityToken validatedToken;
    var principal = handler.ValidateToken(jwtToken, validationParameters, out validatedToken);

    var decodedToken = handler.ReadJwtToken(jwtToken);

    var email = decodedToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
    Console.WriteLine(email);

    var acl = decodedToken.Claims.FirstOrDefault(c => c.Type == "acl")?.Value;
    Console.WriteLine(acl);
}
catch (Exception ex)
{
    throw;
}



