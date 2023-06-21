using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp1.Model;
using System.Data.SqlClient;
using System.Data;

namespace FunctionApp1
{
    public static class AddCourses
    {
        [FunctionName("AddCourses")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,"post", Route = null)] HttpRequest req,
            ILogger log)
        {
            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Course data = JsonConvert.DeserializeObject<Course>(requestBody);

            string _connection_string = "Server=tcp:coursedb1001.database.windows.net,1433;Initial Catalog=Course;Persist Security Info=False;User ID=dbCourse;Password=Manish@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            // string _connection_string = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
            string _statement = "INSERT INTO Course(CourseId,Name,Batch,StartDate) VALUES(@param1,@param2,@param3,@param4)";
            //string _statement = "DELETE FROM Course WHERE CourseId = @param1";
            //string _statement = "UPDATE Course SET Name=@param2, Batch=@param3,  StartDate =@param4 WHERE CourseId = @param1";
          
            SqlConnection _connection = new SqlConnection(_connection_string);
            _connection.Open();
            using (SqlCommand _command = new SqlCommand(_statement, _connection))
            {
                _command.Parameters.Add("@param1", SqlDbType.Int).Value = data.CourseId;
                _command.Parameters.Add("@param2", SqlDbType.VarChar, 1000).Value = data.Name;
                _command.Parameters.Add("@param3", SqlDbType.VarChar, 2).Value = data.Batch;
                _command.Parameters.Add("@param4", SqlDbType.DateTime).Value = data.StartDate;
                _command.CommandType = CommandType.Text;
                _command.ExecuteNonQuery();
            }
            
            _connection.Close();

            return new OkObjectResult("Course added");
        }
    }
}
