using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectClaseCore.Token
{
    public class HelperToken
    {
        public String Issuer { get; set; }
        public String Audience { get; set; }
        public String Secretkey { get; set; }
        public HelperToken(IConfiguration configuration)
        {
            Issuer = configuration["ApiAuth:Issuer"];
            Audience = configuration["ApiAuth:Audience"];
            Secretkey = configuration["ApiAuth:SecretKey"];
        }
        public SymmetricSecurityKey GetKeyToken()
        {
            byte[] data = System.Text.Encoding.UTF32.GetBytes(this.Secretkey);
            return new SymmetricSecurityKey(data);
        }
        public Action<JwtBearerOptions> GetJwtOptions()
        {
            Action<JwtBearerOptions> jwtoptions = new Action<JwtBearerOptions>(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = GetKeyToken()
                };
            });
            return jwtoptions;
        }
        public Action<AuthenticationOptions> GetAuthOptions()
        {
            Action<AuthenticationOptions> authoptions = new Action<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            return authoptions;
        }
    }
}
