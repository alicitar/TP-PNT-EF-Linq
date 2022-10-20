using System;
using System.Collections.Generic;

namespace PruebaEFCore.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int? StandardId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Standard Standard { get; set; }
        public virtual StudentAddress StudentAddress { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
