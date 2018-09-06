using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpson_Assign1
{
    public enum AcademicYear { Freshman, Sophomore, Junior, Senior, PostBacc }
    public class Student
    {
        public uint ZId { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Major{ get; set; }
        public AcademicYear? Year { get; set; }
        public float? Gpa { get; set; }
        public ushort? CreditHours { get; set; }

        public Student()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Major = string.Empty;
            Year = null;
            Gpa = null;
            CreditHours = null;
        }

        public Student(uint zid, string firstName, string lastName,
            string major, int year, float gpa)
        {
            ZId = zid;
            FirstName = firstName;
            LastName = lastName;
            Major = major;
            Year = (AcademicYear)year;
            Gpa = gpa;
            CreditHours = 0;
        }

        public int Enroll(Course newCourse)
        {
            //Check conditions and return error code if any are true
            if (newCourse.EnrolledStudents.Contains(this.ZId))
            {
                return 10;
            }
            if (newCourse.EnrolledCount >= newCourse.MaximumCapacity)
            {
                return 5;
            }

            if (this.CreditHours + newCourse.CreditHours >= 18)
            {
                return 15;
            }

            //If it makes it this far without returning, operate on the appropriate properties and return 0
            newCourse.EnrolledCount += 1;
            newCourse.EnrolledStudents.Add(this.ZId);
            this.CreditHours += newCourse.CreditHours;
            return 0;
        }
    }
}
