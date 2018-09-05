using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpson_Assign1
{
    public class Course
    {
        public string DepartmentCode { get; set; }
        public uint? CourseNumber { get; set; }
        public string SectionNumber { get; set; }
        public ushort? CreditHours { get; set; }
        public List<int> EnrolledStudents { get; set; }
        public ushort? EnrolledCount { get; set; }
        public ushort? MaximumCapacity { get; set; }

        public Course()
        {
            DepartmentCode = string.Empty;
            CourseNumber = null;
            SectionNumber = string.Empty;
            CreditHours = null;
            EnrolledStudents = new List<int>();
            EnrolledCount = null;
            MaximumCapacity = null;
        }

        public Course(string deptCode, uint courseNum, string sectNumber, ushort hours, ushort capacity)
        {
            DepartmentCode = deptCode;
            CourseNumber = courseNum;
            SectionNumber = sectNumber;
            CreditHours = hours;
            MaximumCapacity = capacity;
        }
    }
}
