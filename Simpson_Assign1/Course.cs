/*
 * CSCI 504: Programming principles in .NET
 * Assignment 1
 * Benjamin Simpson - Z100820
 * Xueqiong Li - z1785226
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpson_Assign1
{
    public class Course : IComparable<Course>
    {
        #region Properties
        private string departmentCode;
        private uint? courseNumber;
        private string sectionNumber;
        private ushort? creditHours;

        public string DepartmentCode
        {
            get
            {
                return departmentCode;
            }
            set
            {
                if (value.Length <= 4 && value.Length > 0)
                {
                    departmentCode = value.ToUpper();
                }
            }
        }

        public uint? CourseNumber
        {
            get
            {
                return courseNumber;
            }
            set
            {
                if (value >= 100 && value <= 499)
                {
                    courseNumber = value;
                }
            }
        }

        public string SectionNumber
        {
            get
            {
                return sectionNumber;
            }
            set
            {
                if (value.Length == 4)
                {
                    sectionNumber = value;
                }
            }
        }

        public ushort? CreditHours
        {
            get
            {
                return creditHours;
            }
            set
            {
                if (value >= 0 && value <= 6)
                {
                    creditHours = value;
                }
            }
        }

        //using auto-properties for all properties that don't require custom logic
        public List<uint?> EnrolledStudents { get; set; }
        public ushort? EnrolledCount => Convert.ToUInt16(EnrolledStudents.Count());
        public ushort? MaximumCapacity { get; set; }
        #endregion

        #region Constructors
        public Course()
        {
            DepartmentCode = string.Empty;
            CourseNumber = null;
            SectionNumber = string.Empty;
            CreditHours = null;
            EnrolledStudents = new List<uint?>();
            MaximumCapacity = null;
        }

        public Course(string deptCode, uint courseNum, string sectNumber, ushort hours, ushort capacity)
        {
            DepartmentCode = deptCode.ToUpper();
            CourseNumber = courseNum;
            SectionNumber = sectNumber;
            CreditHours = hours;
            EnrolledStudents = new List<uint?>();
            MaximumCapacity = capacity;
        }
        #endregion

        #region Methods
        public void PrintRoster(List<Student> students)
        {
            Console.WriteLine("\nCourse: {0} {1}-{2} ({3}/{4})", DepartmentCode, CourseNumber, SectionNumber, EnrolledCount, MaximumCapacity);
            Console.WriteLine("-------------------------------------------------------------------");
            if (!EnrolledStudents.Any())
            {
                Console.WriteLine("There are no students currently enrolled in this course.");
            }
            else
            {
                foreach (var student in students)
                {
                    if (EnrolledStudents.Contains(student.ZId))
                    {
                        Console.WriteLine("{0} {1}, {2} {3}", student.ZId, student.LastName, student.FirstName, student.Major);
                    }
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}-{2} ({3}/{4})", DepartmentCode, CourseNumber, SectionNumber, EnrolledCount, MaximumCapacity);
        }

        public int CompareTo(Course course)
        {
            if (course == null)
            {
                return 1;
            }

            //if argument DepartmentCode is lower it should come out first
            if (string.Compare(DepartmentCode, course.DepartmentCode) < 0)
            {
                return -1;
            }

            //if argument Department code is equal, but the courseNumber is loweer, it should still come out first
            if (DepartmentCode == course.DepartmentCode && CourseNumber > course.CourseNumber)
            {
                return -1;
            }

            //if both fields are equal they will come out equal
            if (DepartmentCode == course.DepartmentCode && CourseNumber == course.CourseNumber)
            {
                return 0;
            }

            //every other case is going to return 1
            return 1;
        }
        #endregion
    }
}
