using AP.CHRP.XRDB.BL.MasterData;
using AP.CHRP.XRDB.DT.MasterData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AP.CHRP.XRDB.WebApi.Controllers.MasterData
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CertificateController(ILogger<CertificateController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<CertificateController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public get_MasCertificate_OP get_MasCertificate(get_MasCertificate_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_MasCertificate_OP op = new get_MasCertificate_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_MasCertificate(ref ip, ref op);

            return op;
        }
    }
}