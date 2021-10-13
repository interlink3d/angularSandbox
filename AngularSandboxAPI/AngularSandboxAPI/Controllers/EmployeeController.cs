using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using AngularSandboxAPI.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AngularSandboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select EmployeeId, EmployeeName, Department, 
                    convert(varchar(10),DateOfJoining,120) as DateOfJoining, PhotoFileName 
                    from dbo.Employee
                    ";

            DataTable table = new DataTable();
            string sqlDataSourcce = _configuration.GetConnectionString("AngularSandboxConnection");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSourcce))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"
                    insert into dbo.Employee 
                    (EmployeeName, Department, DateOfJoining, PhotoFileName)
                    values
                    ('" 
                    + emp.EmployeeName + 
                    @"','" + emp.Department + 
                    @"','" + emp.DateOfJoining + 
                    @"','" + emp.PhotoFileName + 
                    @"')";

            DataTable table = new DataTable();
            string sqlDataSourcce = _configuration.GetConnectionString("AngularSandboxConnection");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSourcce))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added new Employee successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
                    update dbo.Employee set 
                    EmployeeName = '" + emp.EmployeeName + @"'
                    ,Department = '" + emp.Department + @"'
                    ,DateOfJoining = '" + emp.DateOfJoining + @"'
                    ,PhotoFileName = '" + emp.PhotoFileName + @"'
                    where EmployeeId = " + emp.EmployeeId + @"                    
                    ";

            DataTable table = new DataTable();
            string sqlDataSourcce = _configuration.GetConnectionString("AngularSandboxConnection");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSourcce))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Employee successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Employee
                    where EmployeeId = " + id + @"
                    ";

            DataTable table = new DataTable();
            string sqlDataSourcce = _configuration.GetConnectionString("AngularSandboxConnection");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSourcce))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted EmployeeId: " + id + " successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (System.Exception)
            {
                // default photo file to use if unable to save user uploaded photo
                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"
                    select DepartmentName from dbo.Department
                    ";

            DataTable table = new DataTable();
            string sqlDataSourcce = _configuration.GetConnectionString("AngularSandboxConnection");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSourcce))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
