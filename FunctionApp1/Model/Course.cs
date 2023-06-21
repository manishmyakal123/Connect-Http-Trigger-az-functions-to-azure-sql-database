using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Model
{
    [Serializable]
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; } 
        public DateTime? StartDate { get; set; }
        public string Batch { get; set; }
    }


    [Serializable]
    public class Error
    {
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Detail { get; set; }
    }
}
