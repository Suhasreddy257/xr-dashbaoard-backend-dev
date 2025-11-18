using System.Drawing;
using System.Text.RegularExpressions;
using AP.CHRP.XRDB.AC.DataContract;
using AP.CHRP.XRDB.BL.MasterData;
using AP.CHRP.XRDB.BL.User;
using AP.CHRP.XRDB.DB.DataTable;
using AP.CHRP.XRDB.DT.MasterData;
using AP.CHRP.XRDB.DT.User;
using AP.CHRP.XRDB.WebApi.Controllers.MasterData;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AP.CHRP.XRDB.WebApi.Controllers.User
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController(ILogger<FileController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<FileController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet]
        public IActionResult DownloadUserTemplate()
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("UserTemplate");

            int col = 1;

            // HEADER STYLE
            var headerRange = ws.Range("A1:V1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Helper function to apply RED text to required fields
            void SetHeader(IXLWorksheet sheet, int column, string text, bool required)
            {
                var cell = sheet.Cell(1, column);
                cell.Value = text;

                if (required)
                    cell.Style.Font.FontColor = XLColor.Red;  // 🔥 Required = Red text
            }

            // ==============================
            // ADD HEADERS WITH REQUIRED FLAG
            // ==============================

            SetHeader(ws, col++, "UserNumber*", true);
            SetHeader(ws, col++, "FirstName*", true);
            SetHeader(ws, col++, "LastName*", true);
            SetHeader(ws, col++, "Gender", false);
            SetHeader(ws, col++, "DateOfBirth (yyyy-mm-dd)", false);
            SetHeader(ws, col++, "PhonePrimary", false);
            SetHeader(ws, col++, "Email*", true);
            SetHeader(ws, col++, "DepartmentID*", true);
            SetHeader(ws, col++, "DesignationID*", true);
            SetHeader(ws, col++, "BranchID", false);
            SetHeader(ws, col++, "ContractorID", false);
            SetHeader(ws, col++, "EmployeementType*", true);
            SetHeader(ws, col++, "DateOfJoining (yyyy-mm-dd)*", true);
            SetHeader(ws, col++, "NationalIdentificationNumber*", true);
            SetHeader(ws, col++, "LanguageID*", true);
            SetHeader(ws, col++, "Address*", true);
            SetHeader(ws, col++, "Area", false);
            SetHeader(ws, col++, "CityID*", true);
            SetHeader(ws, col++, "PostalCode*", true);
            SetHeader(ws, col++, "StateID*", true);
            SetHeader(ws, col++, "CountryID*", true);
            SetHeader(ws, col++, "AccessCardNumber", false);

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "UserTemplate.xlsx"
            );
        }

        [HttpPost]
        public async Task<IActionResult> ImportUserExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Upload a valid Excel file.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var workbook = new XLWorkbook(stream);
            var ws = workbook.Worksheet(1);
            var rows = ws.RowsUsed().Skip(1);

            int total = 0, success = 0, failed = 0;
            var errorList = new List<object>();


            // GET MASTER – DEPARTMENT

            var deptIp = new get_OrganizationDepartment_IP()
            {
                UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection"),
                MessageInfo = new AC_DT_MessageInfo()
            };

            var deptOp = new get_OrganizationDepartment_OP()
            {
                MessageInfo = new AC_DT_MessageInfo()
            };

            BL_MasterData deptBL = new BL_MasterData();
            deptBL.get_OrganizationDepartment(ref deptIp, ref deptOp);

            var validDepartments = deptOp.ml_OrganizationDepartment
                .Select(d => d.ID)
                .ToHashSet();


            // GET MASTER – DESIGNATION

            var desigIp = new get_OrganizationDesignation_IP()
            {
                UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection"),
                MessageInfo = new AC_DT_MessageInfo()
            };

            var desigOp = new get_OrganizationDesignation_OP()
            {
                MessageInfo = new AC_DT_MessageInfo()
            };

            BL_MasterData desigBL = new BL_MasterData();
            desigBL.get_OrganizationDesignation(ref desigIp, ref desigOp);

            var validDesignations = desigOp.ml_OrganizationDesignation
                .Select(d => d.ID)
                .ToHashSet();
            // ----------------------------------
            // PROCESS EACH EXCEL ROW
            // ----------------------------------
            foreach (var row in rows)
            {
                total++;
                var rowErrors = new List<string>();

                // READ CELLS
                string userNumber = row.Cell(1).GetString().Trim();
                string firstName = row.Cell(2).GetString().Trim();
                string lastName = row.Cell(3).GetString().Trim();
                string gender = row.Cell(4).GetString().Trim();
                string dobStr = row.Cell(5).GetString().Trim();
                string phonePrimary = row.Cell(6).GetString().Trim();
                string email = row.Cell(7).GetString().Trim();
                string departmentStr = row.Cell(8).GetString().Trim();
                string designationStr = row.Cell(9).GetString().Trim();
                string branchStr = row.Cell(10).GetString().Trim();
                string contractorStr = row.Cell(11).GetString().Trim();
                string empType = row.Cell(12).GetString().Trim().ToLower();
                string dojStr = row.Cell(13).GetString().Trim();
                string nationalId = row.Cell(14).GetString().Trim();
                string languageIdStr = row.Cell(15).GetString().Trim();
                string address = row.Cell(16).GetString().Trim();
                string area = row.Cell(17).GetString().Trim();
                string cityStr = row.Cell(18).GetString().Trim();
                string postalCode = row.Cell(19).GetString().Trim();
                string stateStr = row.Cell(20).GetString().Trim();
                string countryStr = row.Cell(21).GetString().Trim();
                string accessCard = row.Cell(22).GetString().Trim();

                // REQUIRED VALIDATIONS
                if (string.IsNullOrEmpty(firstName)) rowErrors.Add("FirstName missing");
                if (string.IsNullOrEmpty(lastName)) rowErrors.Add("LastName missing");
                if (string.IsNullOrEmpty(email)) rowErrors.Add("Email missing");

                if (!Regex.IsMatch(phonePrimary, @"^\d{10}$"))
                    rowErrors.Add("Invalid Mobile Number (must be 10 digits)");

                if (!Regex.IsMatch(nationalId, @"^\d{12}$"))
                    rowErrors.Add("Invalid Aadhaar Number (must be 12 digits)");

                if (!Regex.IsMatch(postalCode, @"^\d{6}$"))
                    rowErrors.Add("Invalid Postal Code (must be 6 digits)");

                // SAFE PARSING
                int.TryParse(departmentStr, out int departmentID);
                int.TryParse(designationStr, out int designationID);
                int.TryParse(branchStr, out int branchID);
                int.TryParse(contractorStr, out int contractorID);
                int.TryParse(languageIdStr, out int languageID);
                int.TryParse(cityStr, out int cityID);
                int.TryParse(stateStr, out int stateID);
                int.TryParse(countryStr, out int countryID);

                DateTime.TryParse(dobStr, out DateTime dob);
                DateTime.TryParse(dojStr, out DateTime doj);

                // MASTER VALIDATION
                if (!validDepartments.Contains(departmentID))
                    rowErrors.Add($"Invalid DepartmentID: {departmentID}");

                if (!validDesignations.Contains(designationID))
                    rowErrors.Add($"Invalid DesignationID: {designationID}");

                // IF ANY ERROR → SKIP ROW
                if (rowErrors.Any())
                {
                    failed++;
                    errorList.Add(new { row = row.RowNumber(), errors = string.Join(" | ", rowErrors) });
                    continue;
                }

                // -------------------------
                // USER OBJECT
                // -------------------------
                var user = new TBL_MAS_USER()
                {
                    UserNumber = userNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = gender,
                    DateOfBirth = dob,
                    PhonePrimary = phonePrimary,
                    Email = email,
                    DepartmentID = departmentID,
                    DesignationID = designationID,
                    BranchID = branchID,
                    ContracterID = contractorID,
                    EmployeementType = empType,
                    DateOfJoining = doj,
                    NationalIdentificationNumber = nationalId,
                    LanguageID = languageID,
                    Language = "English",
                    Address = address,
                    Area = area,
                    CityID = cityID,
                    CityName = "",
                    PostalCode = postalCode,
                    StateID = stateID,
                    StateName = "",
                    CountryID = countryID,
                    CountryName = "",
                    EmploymentStatus = "Active",
                    IsThereSignatureImage = false,
                    SignatureImageURL = "",
                    AccessCardNumber = accessCard,
                    CreatedUserID = 1,
                    CreatedDateTime = DateTime.Now,
                    UpdatedUserID = 1,
                    UpdatedDateTime = DateTime.Now,
                    IsActive = true
                };

                // LOGIN
                var login = new TBL_MAS_USER_LOGIN()
                {
                    UserID = user.ID,
                    UserLoginName = user.Email,
                    UserRoleID = 3,
                    UserRoleName = "Employee",
                    Password = "Emp@123!",
                    PrevPassword1 = "",
                    PrevPassword2 = "",
                    PrevPassword3 = "",
                    PrevPassword4 = "",
                    PrevPassword5 = "",
                    SecretQuestion = "",
                    SecretAnswer = "",
                    UserCategoryID = 0,
                    UserCategoryName = "",
                    LastPasswordChange = DateTime.Now,
                    LastLoginDateTime = DateTime.Now,
                    ResetPasswordFlag = "0",
                    IsLoginActive = true,
                    IsLoginLocked = false,
                    LastLoginIPAddress = "",
                    LastLoginMachineName = "",
                    LastLoginMachineDetails = "",
                    CreatedUserID = 1,
                    CreatedDateTime = DateTime.Now,
                    UpdatedUserID = 1,
                    UpdatedDateTime = DateTime.Now,
                    IsActive = true
                };

                // CALL BL
                var ip = new put_UserNew_IP()
                {
                    m_user = user,
                    m_user_login = login,
                    MessageInfo = new AC_DT_MessageInfo()
                };

                ip.UserDBConnStr = _configuration.GetConnectionString("DatabaseConnection");

                var op = new put_UserNew_OP()
                {
                    MessageInfo = new AC_DT_MessageInfo()
                };

                BL_User bl = new BL_User();
                int rc = bl.put_UserNew(ref ip, ref op);

                if (rc == 0)
                    success++;
                else
                {
                    failed++;
                    errorList.Add(new { row = row.RowNumber(), errors = op.MessageInfo.ReturnMessage });
                }
            }

            return Ok(new { total, success, failed, errors = errorList });
        }


    }
}