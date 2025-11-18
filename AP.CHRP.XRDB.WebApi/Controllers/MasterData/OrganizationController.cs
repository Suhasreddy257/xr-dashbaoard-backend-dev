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
    public class OrganizationController(ILogger<OrganizationController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<OrganizationController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public get_Organization_OP get_Organization(get_Organization_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_Organization_OP op = new get_Organization_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_Organization(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_OrganizationDepartment_OP get_OrganizationDepartment(get_OrganizationDepartment_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_OrganizationDepartment_OP op = new get_OrganizationDepartment_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_OrganizationDepartment(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_OrganizationDesignation_OP get_OrganizationDesignation(get_OrganizationDesignation_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_OrganizationDesignation_OP op = new get_OrganizationDesignation_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.get_OrganizationDesignation(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_OrganizationDepartment_OP put_OrganizationDepartment(put_OrganizationDepartment_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_OrganizationDepartment_OP op = new put_OrganizationDepartment_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.put_OrganizationDepartment(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_OrganizationDesignation_OP put_OrganizationDesignation(put_OrganizationDesignation_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_OrganizationDesignation_OP op = new put_OrganizationDesignation_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.put_OrganizationDesignation(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_OrganizationDepartmentEdit_OP put_OrganizationDepartmentEdit(put_OrganizationDepartmentEdit_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_OrganizationDepartmentEdit_OP op = new put_OrganizationDepartmentEdit_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.put_OrganizationDepartmentEdit(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_OrganizationDesignationEdit_OP put_OrganizationDesignationEdit(put_OrganizationDesignationEdit_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_OrganizationDesignationEdit_OP op = new put_OrganizationDesignationEdit_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.put_OrganizationDesignationEdit(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_OrganizationDepartmentDelete_OP put_OrganizationDepartmentDelete(put_OrganizationDepartmentDelete_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_OrganizationDepartmentDelete_OP op = new put_OrganizationDepartmentDelete_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.put_OrganizationDepartmentDelete(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_OrganizationDesignationDelete_OP put_OrganizationDesignationDelete(put_OrganizationDesignationDelete_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_OrganizationDesignationDelete_OP op = new put_OrganizationDesignationDelete_OP();

            BL_MasterData bl = new BL_MasterData();
            bl.put_OrganizationDesignationDelete(ref ip, ref op);

            return op;
        }
    }
}