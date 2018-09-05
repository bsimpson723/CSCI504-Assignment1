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
    }
}
