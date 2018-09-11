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

            string input = "";
            do
            {
                Console.WriteLine("\nPlease choose from the following options:\n");
                PrintMenu();
                input = Console.ReadLine();

                switch (input)
                {
                    case ("1"):
                        Console.WriteLine("\nStudent List <All Students>:");
                        Console.WriteLine("------------------------------------------------------");
                        foreach (Student eachStudent in Students)
                        {
                            Console.WriteLine(eachStudent.ToString());
                        }
                        Console.WriteLine("");
                        break;
                    case ("2"):
                        Console.Write("Which major list would you like printed? ");
                        string major = Console.ReadLine();
                        Console.Write("\nStudent List <{0} Majors>", major);
                        Console.WriteLine("\n------------------------------------------------------");
                        // looking for students in the major
                        List<Student> foundStudent = Students.FindAll(x => x.Major == major);
                        foreach (Student eachStudent in foundStudent)
                        {
                            Console.WriteLine(eachStudent.ToString());
                        }
                        // if not found, print warning message
                        if (foundStudent.Count == 0)
                        {
                            Console.WriteLine("There doesn't appear to be any students majoring in '{0}'.", major);
                        }
                        Console.WriteLine("");
                        break;
                    case ("3"):
                        Console.WriteLine("Which academic year would you like printed?");
                        Console.Write("<Freshman, Sophomore, Junior, Senior, PostBacc> ");
                        string year = Console.ReadLine();
                        // print error if user input is not a type of academic year
                        if (!Enum.IsDefined(typeof(AcademicYear), year))
                        {
                            Console.WriteLine("'{0}' is not an acamedic year.", year);
                        } else
                        {
                            Console.Write("\nStudent List <{0} Majors>", year);
                            Console.WriteLine("\n------------------------------------------------------");
                            AcademicYear year2;
                            // transfer academic year input to string and find in students list
                            Enum.TryParse(year, out year2);
                            List<Student> foundStudent2 = Students.FindAll(x => x.Year == year2);
                            foreach (Student eachStudent in foundStudent2)
                            {
                                Console.WriteLine(eachStudent.ToString());
                            }
                            if (foundStudent2.Count == 0)
                            {
                                Console.WriteLine("There doesn't appear to be any students in '{0}' year.", year);
                            }
                        }
                        Console.WriteLine("");
                        break;
                    case ("4"):
                        Console.WriteLine("\nCourse List <All Courses>:");
                        Console.WriteLine("------------------------------------------------------");
                        foreach (Course eachCourse in Courses)
                        {
                            Console.WriteLine(eachCourse.ToString());
                        }
                        Console.WriteLine("");
                        break;
                    case ("5"):
                        Console.WriteLine("Which course roster would you like printed?");
                        Console.Write("<DEPT COURSE_NUM-SECTION_NUM> ");
                        string crs = Console.ReadLine();
                        Course foundCourse = null;
                        // print error if the input is not in the required format
                        try
                        {
                            // split input and looking for matching course
                            string[] words = crs.Split(' ');
                            string[] words2 = words[1].Split('-');
                            foundCourse = Courses.Find(x => x.DepartmentCode == words[0] && x.CourseNumber == Convert.ToUInt64(words2[0]) && x.SectionNumber == words2[1]);
                        } catch (Exception)
                        {
                            Console.WriteLine("'{0}' doesn't follow required format.", crs);
                            continue;
                        }
                        // if course not found, print error
                        if (foundCourse != null)
                        {
                            foundCourse.PrintRoster(Students);
                        } else 
                        {
                            Console.WriteLine("'{0}' does not exist.", crs);
                        }
                        Console.WriteLine("");
                        break;
                    case ("6"):
                        Console.Write("Please enter the Z-ID <omitting the Z character> of the student you would like to enroll into a course.");
                        string zid = Console.ReadLine();
                        Student foundStudent4 = Students.Find(x => x.ZId == Convert.ToUInt64(zid));
                        // if student not found, print error
                        if(foundStudent4 == null)
                        {
                            Console.WriteLine("Student {0} not exist.", zid);
                            continue;
                        }
                        Console.WriteLine("Which course will this student be enrolled into?");
                        Console.Write("<DEPT COURSE_NUM-SECTION_NUM> ");
                        string enrollcrs = Console.ReadLine();
                        Course foundCourse2 = null;
                        // checking whether input is in required format
                        try
                        {
                            // split input and find course matches requirement
                            string[] words3 = enrollcrs.Split(' ');
                            string[] words4 = words3[1].Split('-');
                            foundCourse2 = Courses.Find(x => x.DepartmentCode == words3[0] && x.CourseNumber == Convert.ToUInt64(words4[0]) && x.SectionNumber == words4[1]);
                        } catch (Exception)
                        {
                            Console.WriteLine("'{0}' doesn't follow required format.", enrollcrs);
                            continue;
                        }
                        // error if not found course
                        if (foundCourse2 == null)
                        {
                            Console.WriteLine("Course {0} not exist.", enrollcrs);
                        }
                        else
                        {
                            // check enrollment whether succeed or not
                            int success = foundStudent4.Enroll(foundCourse2);
                            if (success == 0)
                            {
                                Console.WriteLine("\nz{0} has successfully enrolled into {1}.", zid, enrollcrs);
                            }
                        }
                        Console.WriteLine("");
                        break;
                    case ("7"):
                        Console.Write("Please enter the Z-ID <omitting the Z character> of the student you like to drop from a course.");
                        string zid2 = Console.ReadLine();
                        Student foundStudent5 = Students.Find(x => x.ZId == Convert.ToUInt64(zid2));
                        // print error if student not found
                        if (foundStudent5 == null)
                        {
                            Console.WriteLine("Student {0} not exist.", zid2);
                            continue;
                        }
                        Console.WriteLine("Which course will this student be dropped from?");
                        Console.Write("<DEPT COURSE_NUM-SECTION_NUM> ");
                        string dropcrs = Console.ReadLine();
                        Course foundCourse3 = null;
                        // print error if input is not formatted
                        try
                        {
                            string[] words5 = dropcrs.Split(' ');
                            string[] words6 = words5[1].Split('-');
                            foundCourse3 = Courses.Find(x => x.DepartmentCode == words5[0] && x.CourseNumber == Convert.ToUInt64(words6[0]) && x.SectionNumber == words6[1]);
                        } catch (Exception)
                        {
                            Console.WriteLine("'{0}' doesn't follow required format.", dropcrs);
                            continue;
                        }
                        // check whether found the course
                        if (foundCourse3 == null)
                        {
                            Console.WriteLine("Course {0} not exist.", dropcrs);
                        }
                        else
                        {
                            // check if succeed in dropping class
                            int success2 = foundStudent5.Drop(foundCourse3);
                            if (success2 == 0)
                            {
                                Console.WriteLine("\nz{0} has successfully dropped from {1}.", zid2, dropcrs);
                            }
                        }
                        Console.WriteLine("");
                        break;
                    default:
                        break;
                }
            }
            while (input != "8" && input.ToUpper() != "H" && input.ToUpper() != "Q" && input.ToUpper() != "QUIT" && input.ToUpper() != "EXIT");
        }

        private static List<Student> InitializeStudents()
        {
            var students = new List<Student>();
            // check opening file successfully
            try
            {
                var file = File.ReadAllLines("Students.txt");
                foreach (var line in file)
                {
                    var fields = line.Split(',');
                    var student = new Student(Convert.ToUInt32(fields[0]), fields[1], fields[2], fields[3], Convert.ToInt32(fields[4]), float.Parse(fields[5]));
                    students.Add(student);
                }
            } catch (Exception)
            {
                Console.WriteLine("Can't read Students.txt file.");
            }

            return students;
        }

        private static List<Course> InitializeCourses()
        {
            var courses = new List<Course>();
            // check opening file successfully
            try
            {
                var file = File.ReadAllLines("Courses.txt");
                foreach (var line in file)
                {
                    var fields = line.Split(',');
                    var course = new Course(fields[0], Convert.ToUInt32(fields[1]), fields[2], Convert.ToUInt16(fields[3]), Convert.ToUInt16(fields[4]));
                    courses.Add(course);
                }
            } catch (Exception)
            {
                Console.WriteLine("Can't read Courses.txt file.");
            }

            return courses;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Print Student List (All)");
            Console.WriteLine("2. Print Student List(Major)");
            Console.WriteLine("3. Print Student List(Academic Year)");
            Console.WriteLine("4. Print Course List");
            Console.WriteLine("5. Print Course Roster");
            Console.WriteLine("6. Enroll Student");
            Console.WriteLine("7. Drop Student");
            Console.WriteLine("8. Quit\n");
        }
    }
}
