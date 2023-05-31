using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Identity.Api.Models.Request;
using ECommerce.Identity.Api.Options;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Identity.Api.Handlers
{
    public class JwtHandler
    {
        private readonly JwtOption _jwtOption;
        private readonly GoogleOAuthOption _googleOAuthOption;
        private readonly UserManager<IdentityUser> _userManager;

        public JwtHandler(UserManager<IdentityUser> userManager, IOptions<JwtOption> jwtOptions, IOptions<GoogleOAuthOption> googleOAuthOption)
        {
            _userManager = userManager;
            _jwtOption = jwtOptions.Value;
            _googleOAuthOption = googleOAuthOption.Value;
        }

        internal async Task<string> GenerateNewToken(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(identityUser);
            var roles = await _userManager.GetRolesAsync(identityUser);

            var EPOCH = ToUnixEpochDate(DateTime.UtcNow);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, identityUser.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, EPOCH.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, EPOCH.ToString(), ClaimValueTypes.Integer64));

            roles.ToList().ForEach(r => claims.Add(new Claim(type: "role", value: r)));

            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            return WriteNewToken(claimsIdentity);
        }

        private string WriteNewToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtOption.Issuer,
                Audience = _jwtOption.Audience,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_jwtOption.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOption.Secret)), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private long ToUnixEpochDate(DateTime data)
            => (long)Math.Round((data.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthRequest externalAuth)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
                    {
                        _googleOAuthOption.ClientId
                    }
            };

            return await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
        }
    }
}
