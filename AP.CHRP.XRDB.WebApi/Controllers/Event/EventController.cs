using AP.CHRP.XRDB.BL.Event;
using AP.CHRP.XRDB.DT.Event;
using AP.CHRP.XRDB.WebApi.Controllers.MasterData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AP.CHRP.XRDB.WebApi.Controllers.Event
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController(ILogger<EventController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<EventController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public put_NewEvent_OP put_NewEvent(put_NewEvent_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_NewEvent_OP op = new put_NewEvent_OP();

            BL_Event bl = new BL_Event();
            bl.put_NewEvent(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_AllEvent_OP get_AllEvent(get_AllEvent_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_AllEvent_OP op = new get_AllEvent_OP();

            BL_Event bl = new BL_Event();
            bl.get_AllEvent(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_EventByID_OP get_EventByID(get_EventByID_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_EventByID_OP op = new get_EventByID_OP();

            BL_Event bl = new BL_Event();
            bl.get_EventByID(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_EventEdit_OP put_EventEdit(put_EventEdit_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_EventEdit_OP op = new put_EventEdit_OP();

            BL_Event bl = new BL_Event();
            bl.put_EventEdit(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_EventDelete_OP put_EventDelete(put_EventDelete_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_EventDelete_OP op = new put_EventDelete_OP();

            BL_Event bl = new BL_Event();
            bl.put_EventDelete(ref ip, ref op);

            return op;
        }
    }
}