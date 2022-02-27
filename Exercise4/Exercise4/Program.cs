using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercise4
{
    class Program
    {

       private readonly static String commands = "Commands: <> = required | [] = optional \n    'addstudent <FirstName> <LastName>'\n    'addcourse <CourseName>'\n    'assigncourse <StudentId> <CourseName>'\n    'getstudents [-info]'\n    'getcourses [-info]'";
        static void Main()
        {
            Console.WriteLine(commands);
            NewInput();
        }

        private static void NewInput()
        {
            Console.WriteLine("\nEnter a command: ");
            string[] command = Console.ReadLine().Split(' ');

            switch (command[0])
            {
                case "addstudent":
                    AddStudent(command[1],
                               command[2]);
                    break;

                case "addcourse":
                    AddCourse(command[1]);
                    break;

                case "assigncourse":
                    AssignCourse(command[1],
                                 command[2]);
                    break;

                case "getstudents":
                    GetStudents(command);
                    break;

                case "getcourses":
                    GetCourses(command);
                    break;

                default:
                    Console.WriteLine("Command not found. \n " + commands);
                    break;
            }

            NewInput();
        }

        private static void AddStudent(String FirstName, String LastName)
        {
            try
            {
                using var context = new UniversityContext();
                Student student = new() { FirstName = FirstName, LastName = LastName };
                context.Students.Add(student);
                context.SaveChanges();

                Console.WriteLine("Added student: " + student.GetString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddCourse(String CourseName)
        { 
            try
            {
                using var context = new UniversityContext();
                Course course = new() { CourseName = CourseName };
                context.Courses.Add(course);
                context.SaveChanges();

                Console.WriteLine("Added course: " + course.GetString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AssignCourse(String StudentId, String CourseId)
        {
            try
            {
                using var context = new UniversityContext();
                Student student = context.Students.Find(int.Parse(StudentId));
                Course course = context.Courses.Find(int.Parse(CourseId));

                if (course.Students == null) course.Students = new List<Student>();
                if (student.Courses == null) student.Courses = new List<Course>();

                course.Students.Add(student);
                student.Courses.Add(course);

                context.SaveChanges();

                Console.WriteLine("Added " + student.FirstName + " " + student.LastName + " to " + course.CourseName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetStudents(String[] command)
        {
            Console.WriteLine("Students: ");
            foreach(Student student in GetStudents())
            {
                String print = "    " + student.GetString();
                if(command.Length > 1 && command[1].ToLower().Equals("-info"))
                {
                    print += ", Courses: ";
                    if (student.Courses == null)
                    {
                        print += " none.";
                    }
                    else
                    {
                        foreach (Course course in student.Courses)
                        {
                            print += "\n - " + course.GetString();
                        }
                    }
                }
                Console.WriteLine(print + "\n");
            }
        }

        private static List<Student> GetStudents()
        {
            using (var db = new UniversityContext())
            {
                return db.Students.Include(s => s.Courses).OrderBy(s => s.StudentId).ToList();
            }
        }

        private static void GetCourses(String[] command)
        {
            Console.WriteLine("Courses: ");
            foreach (Course course in GetCourses())
            {
                String print = "    " + course.GetString();
                if (command.Length > 1 && command[1].ToLower().Equals("-info"))
                {
                    print += ", Students: ";
                    if (course.Students == null)
                    {
                        print += " none.";
                    }
                    else
                    {
                        foreach (Student student in course.Students)
                        {
                            print += "\n - " + student.GetString();
                        }
                    }
                }
                Console.WriteLine(print + "\n");
            }
        }

        private static List<Course> GetCourses()
        {
            using (var db = new UniversityContext())
            {
                return db.Courses.Include(c => c.Students).OrderBy(c => c.CourseId).ToList();
            }
        }

    }
}
