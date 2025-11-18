using AP.CHRP.XRDB.BL.Certificate;
using AP.CHRP.XRDB.BL.User;
using AP.CHRP.XRDB.DT.Certificate;
using AP.CHRP.XRDB.DT.User;
using AP.CHRP.XRDB.WebApi.Controllers.MasterData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AP.CHRP.XRDB.WebApi.Controllers.Certificate
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CertificateController(ILogger<CertificateController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<CertificateController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public get_AllCertificate_OP get_AllCertificate(get_AllCertificate_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_AllCertificate_OP op = new get_AllCertificate_OP();

            BL_Certificate bl = new BL_Certificate();
            bl.get_AllCertificate(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_NewCertificate_OP put_NewCertificate(put_NewCertificate_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_NewCertificate_OP op = new put_NewCertificate_OP();

            BL_Certificate bl = new BL_Certificate();
            bl.put_NewCertificate(ref ip, ref op);

            return op;
        }
    }
}