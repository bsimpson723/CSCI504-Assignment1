/*
 * CSCI 504: Programming principles in .NET
 * Assignment 1
 * Benjamin Simpson - Z100820
 * Xueqiong Li - z1785226
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpson_Assign1
{
    static class Program
    {
        private static List<Student> Students = new List<Student>();
        private static List<Course> Courses = new List<Course>();
        static void Main(string[] args)
        {
            Students = InitializeStudents();
            Courses = InitializeCourses();

            do
            {
                PrintMenu();
            }
            while (Console.ReadLine() != "8");
        }

        private static List<Student> InitializeStudents()
        {
            var students = new List<Student>();
            var file = File.ReadAllLines("Students.txt");
            foreach (var line in file)
            {
                var fields = line.Split(',');
                var student = new Student(Convert.ToUInt32(fields[0]), fields[1], fields[2], fields[3], Convert.ToInt32(fields[4]), float.Parse(fields[5]));
                students.Add(student);
            }

            return students;
        }

        private static List<Course> InitializeCourses()
        {
            var courses = new List<Course>();
            var file = File.ReadAllLines("Courses.txt");
            foreach (var line in file)
            {
                var fields = line.Split(',');
                var course = new Course(fields[0], Convert.ToUInt32(fields[1]), fields[2], Convert.ToUInt16(fields[3]), Convert.ToUInt16(fields[4]));
                courses.Add(course);
            }

            return courses;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Print Student List (All)");
            Console.WriteLine("2.Print Student List(Major)");
            Console.WriteLine("3.Print); Student List(Academic Year)");
            Console.WriteLine("4.Print Course List");
            Console.WriteLine("5.Print Course Roster");
            Console.WriteLine("6.Enroll Student");
            Console.WriteLine("7.Drop Student");
            Console.WriteLine("8.Quit");
        }
    }
}
