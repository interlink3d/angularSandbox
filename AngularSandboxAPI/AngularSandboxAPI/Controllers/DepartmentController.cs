using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using AngularSandboxAPI.Models;

namespace AngularSandboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId, DepartmentName from dbo.Department";

            DataTable table = new DataTable();
            string sqlDataSourcce = _configuration.GetConnectionString("AngularSandboxConnection");
            
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSourcce))
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
        public JsonResult Post(Department dep)
        {
            string query = @"
                    insert into dbo.Department values
                    ('"+dep.DepartmentName+@"')
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

            return new JsonResult("Added new department successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"
                    update dbo.Department set 
                    DepartmentName = '"+dep.DepartmentName + @"'
                    where DepartmentId = "+dep.DepartmentId + @"
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

            return new JsonResult("Updated department successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Department
                    where DepartmentId = " + id + @"
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

            return new JsonResult("Deleted departmentId: " + id + " successfully");
        }
    }
}
