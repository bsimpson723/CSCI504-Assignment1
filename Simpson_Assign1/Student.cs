/*
 * CSCI 504: Programming principles in .NET
 * Assignment 1
 * Benjamin Simpson - Z100820
 * Xueqiong Li - z1785226
*/
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpson_Assign1
{
    public enum AcademicYear { Freshman, Sophomore, Junior, Senior, PostBacc }
    public class Student : IComparable<Student>
    {
        #region Properties
        private float? gpa;
        private ushort? creditHours;

        //using auto-properties for all properties that don't require custom logic setter logic
        public uint? ZId { get; }    //get only so that this field can only be set once via the constructor
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Major{ get; set; }
        public AcademicYear? Year { get; set; }

        //properties with custom validation defined below
        public float? Gpa
        {
            get
            {
                return gpa;
            }
            set
            {
                if (value >= 0 && value <= 4)
                {
                    gpa = value;
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
                if (value >= 0 && value <= 18)
                {
                    creditHours = value;
                }
            }
        }
        #endregion

        #region Constructors
        public Student()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Major = string.Empty;
            Year = null;
            Gpa = null;
            CreditHours = null;
        }

        public Student(uint zid, string lastName, string firstName,
            string major, int year, float gpa)
        {
            if (zid >= 1000000)
            {
                ZId = zid;
            }
            LastName = lastName;
            FirstName = firstName;
            Major = major;
            Year = (AcademicYear)year;
            Gpa = gpa;
            CreditHours = 0;
        }
        #endregion

        #region Methods
        public int Enroll(Course course)
        {
            //Check conditions and return error code if any are true
            if (course.EnrolledStudents.Contains(ZId))
            {
                return 10;
            }
            if (course.EnrolledCount >= course.MaximumCapacity)
            {
                return 5;
            }

            if (CreditHours + course.CreditHours >= 18)
            {
                return 15;
            }

            //If it makes it this far without returning, operate on the appropriate properties and return 0
            course.EnrolledStudents.Add(ZId);
            CreditHours += course.CreditHours;
            return 0;
        }

        public int Drop(Course course)
        {
            //if the student isn't enrolled in the course return error code 20
            if (!course.EnrolledStudents.Contains(ZId))
            {
                return 20;
            }

            //if the student IS enrolled in the class, operate on the appropriate properties and return 0
            course.EnrolledStudents.Remove(ZId);
            CreditHours -= course.CreditHours;
            return 0;
        }

        public override string ToString()
        {
            return string.Format("z{0} -- {1,12}, {2,-10} [{3,9}] ({4,17}) | {5:0.000} | ", ZId, LastName, FirstName, Year, Major, Gpa);
        }

        public int CompareTo(Student student)
        {
            if (student == null)
            {
                return 1;
            }
            
            //if argument zid is lower it should come out first
            if (ZId > student.ZId)
            {
                return -1;
            }
            //if zids are equal it doesn't matter which order they come out in (hopefully the IDs are unique though)
            if (ZId == student.ZId)
            {
                return 0;
            }
            //otherwise the instance object will come out first
            return 1;
        }
        #endregion
    }
}
