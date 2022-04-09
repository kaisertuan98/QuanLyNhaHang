using API_QLNH.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace API_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucDonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ThucDonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet] // Get database
        public JsonResult Get()
        {
            //Tao query table
            string query = "Select MaThucDon, TenThucDon from ThucDon";
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("QLNH"); //Ket noi Database
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand =new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
           
            }
            return new JsonResult(table);
        }

        [HttpPost] //Them data
        public JsonResult Post(ThucDon thucDon)
        {
            //Tao query table
            string query = @"Insert into ThucDon values (N'"+thucDon.TenThucDon+"')"; //Them thuc don string sql
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("QLNH"); //Ket noi Database
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
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
            return new JsonResult("Add successful");
        }

        [HttpPut] //Update data
        public JsonResult Put(ThucDon thucDon)
        {
            //Tao query table
            string query = @"Update ThucDon set TenThucDon = N'"+ thucDon.TenThucDon +"'" + "where MaThucDon = " +thucDon.MaThucDon; //Update thuc don string sql
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("QLNH"); //Ket noi Database
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
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
            return new JsonResult("Update successful");
        }

        [HttpDelete("{ma}")] //Delete Data
        public JsonResult Delete(int ma)
        {
            //Tao query table
            string query = @"Delete from ThucDon " + "where MaThucDon = " + ma; //Update thuc don string sql
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("QLNH"); //Ket noi Database
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
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
            return new JsonResult("Deleted");
        }

    }
}
