using AP.CHRP.XRDB.BL.MasterData;
using AP.CHRP.XRDB.BL.User;
using AP.CHRP.XRDB.DT.MasterData;
using AP.CHRP.XRDB.DT.User;
using AP.CHRP.XRDB.WebApi.Controllers.MasterData;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AP.CHRP.XRDB.WebApi.Controllers.User
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController(ILogger<LocationController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<LocationController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost()]
        public get_AllUser_OP get_AllUser(get_AllUser_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_AllUser_OP op = new get_AllUser_OP();

            BL_User bl = new BL_User();
            bl.get_AllUser(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_ViewUserDetail_OP get_ViewUserDetail(get_ViewUserDetail_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_ViewUserDetail_OP op = new get_ViewUserDetail_OP();

            BL_User bl = new BL_User();
            bl.get_ViewUserDetail(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_UserDetail_OP get_UserDetail(get_UserDetail_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_UserDetail_OP op = new get_UserDetail_OP();

            BL_User bl = new BL_User();
            bl.get_UserDetail(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_UserNew_OP put_UserNew(put_UserNew_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_UserNew_OP op = new put_UserNew_OP();

            BL_User bl = new BL_User();
            bl.put_UserNew(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_UserEdit_OP put_UserEdit(put_UserEdit_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_UserEdit_OP op = new put_UserEdit_OP();

            BL_User bl = new BL_User();
            bl.put_UserEdit(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_AssignRole_OP put_AssignRole(put_AssignRole_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_AssignRole_OP op = new put_AssignRole_OP();

            BL_User bl = new BL_User();
            bl.put_AssignRole(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public put_UserDelete_OP put_UserDelete(put_UserDelete_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            put_UserDelete_OP op = new put_UserDelete_OP();

            BL_User bl = new BL_User();
            bl.put_UserDelete(ref ip, ref op);

            return op;
        }

        [HttpPost()]
        public get_AllUserDetails_OP get_AllUserDetails(get_AllUserDetails_IP ip)
        {
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            get_AllUserDetails_OP op = new get_AllUserDetails_OP();

            BL_User bl = new BL_User();
            bl.get_AllUserDetails(ref ip, ref op);

            return op;
        }

        [HttpGet]
        public IActionResult ExportAllUsersExcel()
        {
            // 1. Call your existing BL
            var ip = new get_AllUser_IP()
            {
                UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection")
            };
            var op = new get_AllUser_OP();
            BL_User bl = new BL_User();
            bl.get_AllUser(ref ip, ref op);

            if (op.ml_User == null || op.ml_User.Count == 0)
                return BadRequest("No user data found.");

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Users");

            // Header
            ws.Cell(1, 1).Value = "UserID";
            ws.Cell(1, 2).Value = "First Name";
            ws.Cell(1, 3).Value = "Last Name";
            ws.Cell(1, 4).Value = "Employee Code";
            ws.Cell(1, 5).Value = "Employment Type";
            ws.Cell(1, 6).Value = "Department";
            ws.Cell(1, 7).Value = "Designation";
            ws.Cell(1, 8).Value = "User Role ID";

            ws.Range("A1:H1").Style.Font.Bold = true;
            ws.Range("A1:H1").Style.Fill.BackgroundColor = XLColor.LightGray;

            // Data rows
            int row = 2;
            foreach (var u in op.ml_User)
            {
                ws.Cell(row, 1).Value = u.UserID;
                ws.Cell(row, 2).Value = u.FirstName;
                ws.Cell(row, 3).Value = u.LastName;
                ws.Cell(row, 4).Value = u.EmployeeCode;
                ws.Cell(row, 5).Value = u.EmployeeCode;
                ws.Cell(row, 6).Value = u.Department;
                ws.Cell(row, 7).Value = u.Designation;
                ws.Cell(row, 8).Value = u.UserRoleID;

                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "AllUsers.xlsx"
            );
        }

        [HttpPost]
        public IActionResult ExportSelectedUsersExcel(get_AllUserDetails_IP ip)
        {
            // Validate request
            if (string.IsNullOrWhiteSpace(ip.ml_UserID))
                return BadRequest("UserID list is required.");

            // Convert string → List<int>
            var selectedIds = ip.ml_UserID
                                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(id => int.Parse(id.Trim()))
                                .ToHashSet();

            // Get DB connection
            ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

            // Call BL → fetch all users
            var op = new get_AllUserDetails_OP();
            BL_User bl = new BL_User();
            bl.get_AllUserDetails(ref ip, ref op);

            if (op.ml_user == null || op.ml_user.Count == 0)
                return BadRequest("No user data found.");

            // Filter only selected users
            var filteredUsers = op.ml_user
                                  .Where(u => selectedIds.Contains(u.UserID))
                                  .ToList();

            if (!filteredUsers.Any())
                return BadRequest("No selected users found.");

            // Excel creation
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("SelectedUsers");

            // Header
            ws.Cell(1, 1).Value = "UserID";
            ws.Cell(1, 2).Value = "First Name";
            ws.Cell(1, 3).Value = "Last Name";
            ws.Cell(1, 4).Value = "Employee Code";
            ws.Cell(1, 5).Value = "Employment Type";
            ws.Cell(1, 6).Value = "Department";
            ws.Cell(1, 7).Value = "Designation";
            ws.Cell(1, 8).Value = "User Role ID";

            ws.Range("A1:H1").Style.Font.Bold = true;
            ws.Range("A1:H1").Style.Fill.BackgroundColor = XLColor.LightGray;

            // Data rows
            int row = 2;
            foreach (var u in filteredUsers)
            {
                ws.Cell(row, 1).Value = u.UserID;
                ws.Cell(row, 2).Value = u.FirstName;
                ws.Cell(row, 3).Value = u.LastName;
                ws.Cell(row, 4).Value = u.EmployeeCode;
                ws.Cell(row, 5).Value = u.EmployeementType;
                ws.Cell(row, 6).Value = u.Department;
                ws.Cell(row, 7).Value = u.Designation;
                ws.Cell(row, 8).Value = u.UserRoleID;

                row++;
            }

            ws.Columns().AdjustToContents();

            // Return file
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "SelectedUsers.xlsx"
            );
        }

    }
}