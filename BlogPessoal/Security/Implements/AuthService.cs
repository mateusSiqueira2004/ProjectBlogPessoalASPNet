using blogpessoal.Service;
using BlogPessoal.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogPessoal.Security.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserLogin?> Autenticar(UserLogin userLogin)
        {
            string FotoDeFault = "https://i.imgur.com/I8MfmC8.png";
            if (userLogin == null || string.IsNullOrEmpty(userLogin.Usuario) || string.IsNullOrEmpty(userLogin.Senha))
                return null;

            var BuscarUsuario = await _userService.GetByUsuario(userLogin.Usuario);
            if (BuscarUsuario is null)
                return null;
            if (!BCrypt.Net.BCrypt.Verify(userLogin.Senha, BuscarUsuario.Senha))
                return null;
            var tokenHadler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userLogin.Usuario)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHadler.CreateToken(tokenDescriptor);

            userLogin.Id = BuscarUsuario.Id;
            userLogin.Nome = BuscarUsuario.Nome;
            userLogin.Foto = BuscarUsuario.Foto is null ? FotoDeFault : BuscarUsuario.Foto;
            userLogin.Token = "Bearer " + tokenHadler.WriteToken(token).ToString();
            userLogin.Senha = string.Empty;

            return userLogin;
           }
    }
}
