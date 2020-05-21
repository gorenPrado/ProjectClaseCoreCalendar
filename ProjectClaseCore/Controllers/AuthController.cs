using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProjectClaseCore.Models;
using ProjectClaseCore.Repositorio;
using ProjectClaseCore.Token;

namespace ProjectClaseCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        RepositorioProject repo;
        HelperToken helper;
        public AuthController(RepositorioProject repo, IConfiguration configuration)
        {
            this.helper = new HelperToken(configuration);
            this.repo = repo;
        }
        [HttpPost("[action]")]
        public IActionResult Login(Login model)
        {
            Usuarios usuario = repo.ExisteUsuario(model.Username, int.Parse(model.Password));
            if (usuario != null)
            {
                Claim[] claims = new[]
                {
                    new Claim("UserData",JsonConvert.SerializeObject(usuario))
                };
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: helper.Issuer,
                    audience: helper.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(helper.GetKeyToken(),
                    SecurityAlgorithms.HmacSha256));
                return Ok(new { response = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}