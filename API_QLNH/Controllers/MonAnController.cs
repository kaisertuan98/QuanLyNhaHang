using API_QLNH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace API_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonAnController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env; 
        // private: han che truy cap
        // readonly: khong the thay doi duoc gia tri

        public MonAnController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet] // Get database
        public JsonResult Get()
        {
            //Tao query table
            string query = "Select MaMonAn, TenMonAn, ThucDon, NgayTao" + ", AnhMonAn from MonAn";
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
            return new JsonResult(table);
        }

        [HttpPost] //Them data
        public JsonResult Post(MonAn monAn)
        {
            //Tao query table
            string query = @"Insert into MonAn values (
                                                         N'" + monAn.TenMonAn + "'" +
                                                         ",N'" + monAn.ThucDon + "'" +
                                                         ", '" + monAn.NgayTao + "'" +
                                                         ", N'" + monAn.AnhMonAn + "'" +
                                                         ")";
            //Them thuc don string sql
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
        public JsonResult Put(MonAn monAn)
        {
            //Tao query table
            string query = @"Update MonAn set 
                            TenMonAn = N'" + monAn.TenMonAn + "' "
                            + ", ThucDon = N'" + monAn.ThucDon + "' "
                            + ", NgayTao = '" + monAn.NgayTao + "' "
                            + ", AnhMonAn = N'" + monAn.AnhMonAn + "' "
                            + " where MaMonAn = " + monAn.MaMonAn; //Update thuc don string sql
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
            string query = @"Delete from MonAn " + "where MaMonAn = " + ma; //Update thuc don string sql
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


        [Route("SaveFile")] // nen dung try catch khi upload file image
        [HttpPost] //Them data
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            } catch(Exception)
            {
                return new JsonResult("com.jpg");
            }
        }

        [Route("GetAllTenThucDon")] // Get database
        [HttpGet]
        public JsonResult GetAllTenThucDon()
        {

            string query = "Select TenThucDon from ThucDon";

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
            return new JsonResult(table);
        }


    }
}
