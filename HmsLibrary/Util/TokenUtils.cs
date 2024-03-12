using HmsAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Util;

public static class TokenUtils
{
    public static string GenerateAccessToken(ApplicationUser user, List<string> roles)
    {
        // TODO Fetch the accesstoken secret from appsettings.json or similar
        var accessTokenSecret = "This is my custom Secret key for authnetication which will be moved to be more secured later";

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(accessTokenSecret);

        // Give me a list of all the roles the user is in

        var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),

            });

        foreach (var role in roles)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, role));
        }


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(15), // Token expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                                   SecurityAlgorithms.HmacSha256Signature)
        };

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5000",
            audience: "http://localhost:5000",
            claims: tokenDescriptor.Subject.Claims,
            expires: tokenDescriptor.Expires,
            signingCredentials: tokenDescriptor.SigningCredentials);

        return tokenHandler.WriteToken(token);
    }

    public static string GenerateRefreshToken()
    {
        // TODO Generate an actual refresh token with a expiration

        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    //public static string GenerateAccessTokenFromRefreshToken(string refreshToken)
    //{
    //    // TODO Fetch the refreshtoken secret from appsettings.json or similar
    //    var refreshTokenSecret
    //}

    //private static string GenerateToken()
    //{

    //}


    private static bool ValidateAccessToken(string token)
    {
        // TODO Fetch the accesstoken secret from appsettings.json or similar
        var accessTokenSecret = "D/X4yFrh3i1po3MV4DEOdSIeuuii8Hji28bqMBtPwmU=";
        return ValidateJwtToken(token, accessTokenSecret).Result;
    }

    private static bool ValidateRefreshToken(string token)
    {
        // TODO Fetch the refreshtoken secret from appsettings.json or similar
        var refreshTokenSecret = "D/X4yFrh3i1po3MV4DEOdSIeuuii8Hji28bqMBtPwmU=";
        return ValidateJwtToken(token, refreshTokenSecret).Result;
    }

    private static async Task<bool> ValidateJwtToken(string token, string secretKey)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        try
        {
            await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            });
        }
        catch
        {
            return false;
        }

        return true;
    }
}
