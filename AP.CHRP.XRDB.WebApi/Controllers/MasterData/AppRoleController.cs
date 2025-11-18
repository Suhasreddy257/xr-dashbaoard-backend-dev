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

    public class AppRoleController(ILogger<AppRoleController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<AppRoleController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public get_AppRole_OP get_AppRole(get_AppRole_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_AppRole_OP op = new get_AppRole_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_AppRole(ref ip, ref op);

            return op;
        }

    }
}
