using System.IdentityModel.Tokens.Jwt;
using ApiStore.Data;
using ApiStore.Models;
using ApiStore.Models.Dto;
using ApiStore.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {

        private readonly StoreDbContext _context;
        private readonly IConfiguration _configuration;

        public AutenticacionController(StoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //LOGIN
        [AllowAnonymous]
        [HttpPost("Validar")]
        public async Task<IActionResult> ValidarCredencial([FromBody] UsuarioLoginDto usuario)
        {
            try
            {
                var existeLogin = await _context.Usuarios
                .AnyAsync(x => x.Email.Equals(usuario.Email) && x.Password.Equals(usuario.Password));

                Usuarios usuarioLogin = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.Equals(usuario.Email) && x.Password.Equals(usuario.Password));


                if (!existeLogin)
                {
                    return NotFound("Usuario No Existe");
                }

                var token = GenerateJwtToken(usuario.Email);

                LoginResponseDto loginReponse = new LoginResponseDto()
                {
                    Autenticado = existeLogin,
                    Email = existeLogin ? usuarioLogin.Email : "",
                    Nombre = existeLogin ? usuarioLogin.Usuario : "",
                    IdRol = existeLogin ? usuarioLogin.IdRol : 0,
                    Id = existeLogin ? usuarioLogin.Id : 0,
                    Token = token
                };

                return Ok(loginReponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings").GetSection("Secret").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /*private readonly string secretKey;

        public AutenticacionController(IConfiguration config)
        {
            secretKey = config.GetSection("AppSettings").GetSection("Secret").ToString();
        }

        [HttpPost]
        [Route("ValidarUsuario")]
        public IActionResult Validar([FromBody] UsuarioLoginDto request)
        {

            if (request.Email == "c@gmail.com" && request.Password == "123")
            {

                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);


                return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });

            }
            else
            {

                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }



        }*/
    }
}
