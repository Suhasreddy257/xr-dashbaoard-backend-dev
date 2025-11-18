
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AP.CHRP.XRDB.DT.MasterData;
using AP.CHRP.XRDB.BL.MasterData;
using Microsoft.AspNetCore.Authorization;

namespace AP.CHRP.XRDB.WebApi.Controllers.MasterData
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController(ILogger<LocationController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<LocationController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public get_LocationCity_OP get_LocationCity(get_LocationCity_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_LocationCity_OP op = new get_LocationCity_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_LocationCity(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_LocationState_OP get_LocationState(get_LocationState_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_LocationState_OP op = new get_LocationState_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_LocationState(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_LocationCountry_OP get_LocationCountry(get_LocationCountry_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_LocationCountry_OP op = new get_LocationCountry_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_LocationCountry(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_LocationPostalCode_OP get_LocationPostalCode(get_LocationPostalCode_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_LocationPostalCode_OP op = new get_LocationPostalCode_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_LocationPostalCode(ref ip, ref op);

            return op;
        }        
    }
}