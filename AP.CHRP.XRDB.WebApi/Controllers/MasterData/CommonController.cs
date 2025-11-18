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
    public class CommonController(ILogger<CommonController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<CommonController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public get_MDForUser_OP get_MDForUser(get_MDForUser_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_MDForUser_OP op = new get_MDForUser_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_MDForUser(ref ip, ref op);

            return op;
        }

    }
}