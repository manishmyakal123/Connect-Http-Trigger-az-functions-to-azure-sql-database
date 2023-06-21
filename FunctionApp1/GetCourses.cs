using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using FunctionApp1.Model;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FunctionApp1
{
    public static class GetCourses
    {
        
        [FunctionName("GetCourses")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //int CourseId = 0;
            //DateTime startDate = DateTime.MinValue;

            Error er = new Error();
            er.Message = Constants.Msg_Common;
            er.Exception = Constants.Msg_Common;


            try
            {
                //Course course = new Course();

                //int.TryParse(req.Query[Constants.req_Id], out CourseId);
                //course.CourseId = CourseId > 0 ? CourseId : 0;

                //course.Name = !string.IsNullOrEmpty(req.Query[Constants.req_Name]) ? req.Query[Constants.req_Name].ToString() : string.Empty;

                //DateTime.TryParse(req.Query[Constants.req_StartDate], out startDate);
                //course.StartDate = startDate != DateTime.MinValue ? startDate : DateTime.MinValue;

                //course.Batch = !string.IsNullOrEmpty(req.Query[Constants.req_Batch]) ? req.Query[Constants.req_Batch].ToString() : string.Empty;

                //log.LogInformation("Request Parameter Validations");
                //if (course.CourseId == 0)
                //{
                //    er.Message = Constants.Msg_InvalidBusUtId;
                //    er.Exception = Constants.Msg_InvalidBusUtId;
                //    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //    {
                //        Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
                //    };
                //}

                //if (course.Name == string.Empty)
                //{
                //    er.Message = Constants.Msg_InvalidSrcCate;
                //    er.Exception = Constants.Msg_InvalidSrcCate;
                //    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //    {
                //        Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
                //    };
                //}
                //else
                //{
                //    Regex regex = new Regex("[a-zA-Z]+$");
                //    if (!regex.IsMatch(course.Name))
                //    {
                //        er.Message = Constants.Msg_InvalidSrcCate;
                //        er.Exception = Constants.Msg_InvalidSrcCate;
                //        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //        {
                //            Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
                //        };

                //    }
                //}

                //if (course.StartDate == DateTime.MinValue)
                //{
                //    er.Message = Constants.Msg_InvalidBusDaydt;
                //    er.Exception= Constants.Msg_InvalidBusDaydt;
                //    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //    {
                //        Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
                //    };
                //}

                //if(course.Batch == string.Empty) 
                //{
                //    er.Message = Constants.Msg_InvalidSrcCate;
                //    er.Exception = Constants.Msg_InvalidSrcCate;
                //    return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //    {
                //        Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
                //    };
                //}
                //else
                //{
                //    if(course.Batch.Length > 1)
                //    {
                //        er.Message = Constants.Msg_InvalidSrcCate;
                //        er.Exception = Constants.Msg_InvalidSrcCate;
                //        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //        {
                //            Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
                //        };
                //    }

                //    Regex regex = new Regex("[a-zA-Z]+$");
                //    if (!regex.IsMatch(course.Batch))
                //    {
                //        er.Message = Constants.Msg_InvalidSrcCate;
                //        er.Exception = Constants.Msg_InvalidSrcCate;
                //        return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //        {
                //            Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
                //        };

                //    }
                //}



                List<Course> _lst = new List<Course>();
                // Ensure to use the Environment class to get the connection string
                string _connection_string = "Server=tcp:coursedb1001.database.windows.net,1433;Initial Catalog=Course;Persist Security Info=False;User ID=dbCourse;Password=Manish@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
               // string _connection_string = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
               // string _statement = "SELECT CourseId,Name,Batch,StartDate from Course";
                SqlConnection _connection = new SqlConnection(_connection_string);
                _connection.Open();
                SqlCommand _sqlcommand = new SqlCommand("GetCourses", _connection);
                using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
                {
                    while (_reader.Read())
                    {
                        Course _course = new Course()
                        {
                            CourseId = (int) _reader["CourseId"] ,
                            Name = _reader["Name"] as string,
                            Batch = _reader["Batch"] as string,
                            StartDate = _reader["StartDate"] as DateTime?
                        };

                        _lst.Add(_course);
                    }
                }
                _connection.Close();
               
                //return var.IsNullOrEmpty(_lst) ?
                //    new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //    {
                //        Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
          //} :
                  return  new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(_lst), Encoding.UTF8, "application/json")
                    };


        }

            catch (Exception ex)
            {
                log.LogError("BeginingEnding Total-----" + ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(er.Serialize(), Encoding.UTF8, Constants.ContentType)
                };
            }
        }
    }
}
