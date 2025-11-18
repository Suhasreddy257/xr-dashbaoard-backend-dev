using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AP.CHRP.XRDB.WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        [HttpGet("{DCName}")]
        public object get_DC(String DCName)
        {

            DCName = "AP.CHRP.XRDB.DT." + DCName;

            var newObject = new object();
            try
            {
                var dc = Activator.CreateInstance("AP.CHRP.XRDB.DT", DCName);
                newObject = dc.Unwrap();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return newObject;
        }
    }
}

