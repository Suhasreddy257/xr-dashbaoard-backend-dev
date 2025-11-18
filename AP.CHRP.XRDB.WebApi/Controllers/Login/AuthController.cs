
using AP.CHRP.XRDB.BL.Login;
using AP.CHRP.XRDB.BL.MasterData;
using AP.CHRP.XRDB.DT.Login;
using AP.CHRP.XRDB.DT.MasterData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AP.CHRP.XRDB.WebApi.Controllers.Login
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(ILogger<AuthController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<AuthController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public get_LoginAccess_OP Login([FromBody] get_LoginAccess_IP ip)
        {
            var _dbconstr = _configuration.GetConnectionString("DatabaseConnection");
            ip.UserDBConnStr = _dbconstr;

            get_LoginAccess_OP op = new get_LoginAccess_OP();
            op.IsValidUser = false;

            BL_Login bl = new BL_Login();
            bl.get_LoginAccess(ref ip, ref op);

            if(op.IsValidUser == true)
            {
                var token = GenerateJwtToken(ip.m_UserName);
                op.m_login_token = token;
            }

            return op;
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tkm_ppms_your_super_secret_key_1234567890_slkdjfhlkjsdfhlksjdfhlkfsjd"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}