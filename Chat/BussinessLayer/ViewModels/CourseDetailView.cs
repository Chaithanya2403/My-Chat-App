using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ViewModels
{
    public class CourseDetailView
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Domain { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }
        public string Fee { get; set; }
        public string TrainerName { get; set; }
        public Nullable<int> IsEnabled { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
}
