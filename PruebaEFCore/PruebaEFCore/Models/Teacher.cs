using System;
using System.Collections.Generic;

namespace PruebaEFCore.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Course = new HashSet<Course>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int? StandardId { get; set; }

        public virtual Standard Standard { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }
}
