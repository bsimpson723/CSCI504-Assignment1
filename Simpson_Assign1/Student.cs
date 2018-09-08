﻿using System;
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
        private float? gpa;
        private ushort? creditHours;

        //using auto-properties for all properties that don't require custom logic
        public uint ZId { get; }    //get only so that this field can only be set once via the constructor
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Major{ get; set; }
        public AcademicYear? Year { get; set; }

        //custom GPA property only allows set if value is between 0 and 4
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

        //custom CreditHours property only allows set if value is between 0 and 18
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
            if (gpa >= 0 && gpa <= 4.00)
            {
                Gpa = gpa;
            }
            CreditHours = 0;
        }

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
            return string.Format("z{0} -- {1}, {2} [{3}] ({4}) |{5}|", ZId, LastName, FirstName, Year, Major, Gpa);
        }
    }
}
