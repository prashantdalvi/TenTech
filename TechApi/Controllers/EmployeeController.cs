using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Mvc;
using TechApi.Data.Repository.IRepository;
using TechApi.Models;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using ExcelDataReader;
using Microsoft.AspNetCore.Cors;

namespace TenWebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("UserDetails")]
        [EnableCors("MyAllowSpecificOrigins")]
        public List<Employee> Index()
        {
            Employee employee = new Employee();


            List<Employee> objList = _unitOfWork.Employee.GetAll().ToList();

            return objList;
        }

        [Route("Employee")]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            _unitOfWork.Employee.Add(employee);
            _unitOfWork.Save();

            return (IHttpActionResult)CreatedAtRoute("DefaultApi", new { id = employee.SchemeId }, employee);
        }

        [Route("UploadExcel")]
        [HttpPost]
        public string ExcelUpload()
        {
            string message = "";
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Request.Form;
            if (httpRequest.Files.Count > 0)
            {
                var file = httpRequest.Files[0];
                Stream stream = file.OpenReadStream();

                IExcelDataReader reader = null;

                if (file.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (file.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    message = "This file format is not supported";
                }



                DataSet excelRecords = reader.AsDataSet();
                reader.Close();

                var finalRecords = excelRecords.Tables[0];
                for (int i = 0; i < finalRecords.Rows.Count; i++)
                {
                    Employee objUser = new Employee();
                    objUser.FirstName = finalRecords.Rows[i][0].ToString();
                    objUser.LastName = finalRecords.Rows[i][1].ToString();
                    objUser.EmailId = finalRecords.Rows[i][2].ToString();
                    objUser.Mobile = finalRecords.Rows[i][3].ToString();

                    _unitOfWork.Employee.Add(objUser);

                }

                _unitOfWork.Save();
            }
            return message;
        }

    }

}
