using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace StudentManagementSystem
{
    //git add .
    //git commit
    //git push origin main
    //git pull origin main
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Total { get; set; }
        public string Grade { get; set; }
        public string Mentor { get; set; }
        public Subject Subject { get; set; } // Add Subject property

        public void CalculateGrade()
        {
            double totalMarks = Subject.CalculateTotalMarks();
            if (totalMarks >= 490)
                Grade = "A";
            else if (totalMarks >= 450)
                Grade = "B";
            else if (totalMarks >= 400)
                Grade = "C";
            else if (totalMarks >= 350)
                Grade = "D";
            else
                Grade = "F";
        }

    }

    class Subject
    {
        public double Tamil { get; set; }
        public double English { get; set; }
        public double Maths { get; set; }
        public double Science { get; set; }
        public double Social { get; set; }
        public double Total { get; set; }

        public double CalculateTotalMarks()
        {
            return Tamil + English + Maths + Science + Social;
        }
    }

    class StudentManagementSystem
    {
        List<Student> students = new List<Student>();

        public void AddStudent(Student student)
        {
            if (students.Any(s => s.Id == student.Id))
            {
                Console.WriteLine($"Error: Student with ID {student.Id} already exists.");
                return;
            }
            else
            {
                student.Subject.Total = student.Subject.CalculateTotalMarks();
                student.CalculateGrade(); // Calculate grade when adding student

                students.Add(student);
                Console.WriteLine("Student added successfully!");

                WriteStudentDataToFile(student);



            }
        }
        private void WriteStudentDataToFile(Student student)
        {
            // Get the current date and time
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Get the base directory of the current application
            string baseDirectory = Path.GetDirectoryName("C:\\Users\\Agira\\Documents\\Agira_Task\\StudentManagementSystem\\StudentManagementSystem");
          //  Console.WriteLine(baseDirectory);

            // Combine the base directory with the file name
            string filePath = Path.Combine(baseDirectory, "student_data.txt");

           // Console.WriteLine(filePath);

            // Create or append to the student_data.txt file and write student data
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{dateTime} - Student ID: {student.Id}, Name: {student.Name}, Total Marks: {student.Subject.Total}, Grade: {student.Grade}, Mentor: {student.Mentor}");
            }
        }


        public void RemoveStudent(int id)
        {
            // Find the student with the specified ID
            Student studentToRemove = students.Find(s => s.Id == id);

            if (studentToRemove != null)
            {
                // Remove the student from the list
                students.Remove(studentToRemove);
                Console.WriteLine($"Student with ID {id} removed successfully!");
            }
            else
            {
                Console.WriteLine($"Student with ID {id} not found.");
            }
        }

        public void DisplayStudent()
        {
            Console.WriteLine("List Of Students");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("|   ID   |   Name   |   Total   |   Grade   |   Mentor   |");
            Console.WriteLine("---------------------------------------------------------");


            foreach (var s in students)
            {
                double Total = s.Subject.CalculateTotalMarks();

                Console.WriteLine($"|  {s.Id,-6}|  {s.Name,-8}|  {s.Subject.Total,-8}|  {s.Grade,-9}|  {s.Mentor,-10}|");
            }

            Console.WriteLine("---------------------------------------------------------");
        }

        public void SortedStudent()
        {
            Console.WriteLine("List Of Students");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("|   ID   |   Name   |   Total   |   Grade   |   Mentor   |");
            Console.WriteLine("---------------------------------------------------------");

            var sortedStudents = students.OrderByDescending(s => s.Subject.Total);

            foreach (var s in sortedStudents)
            {
                Console.WriteLine($"|  {s.Id,-6}|  {s.Name,-8}|  {s.Subject.Total,-8}|  {s.Grade,-9}|  {s.Mentor,-10}|");
            }

            Console.WriteLine("---------------------------------------------------------");
        }

        public void DisplayAllMarks()
        {
            Console.WriteLine("All Students Marks");
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.WriteLine("|   ID   |   Name   |  Tamil  |  English  |  Maths  |  Science  |  Social  |  Total   |");
            Console.WriteLine("--------------------------------------------------------------------------------------");

            foreach (var student in students)
            {
                Console.WriteLine($"|  {student.Id,-6}|  {student.Name,-8}|  {student.Subject.Tamil,-7}|  {student.Subject.English,-9}|  {student.Subject.Maths,-7}|  {student.Subject.Science,-9}|  {student.Subject.Social,-8}|  {student.Subject.Total,-7}|");
            }

            Console.WriteLine("---------------------------------------------------------------------------------------");
        }

        public void DisplayStudentsBySubject(string subjectName)
        {
            List<Student> studentsWithSubject = students.Where(s => s.Subject != null).ToList();

            if (studentsWithSubject.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            Console.WriteLine($"List Of Students Sorted by {subjectName} Marks (Descending)");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("|   ID   |   Name   |   " + subjectName + "    |");
            Console.WriteLine("-----------------------------------");

            switch (subjectName.ToLower())
            {
                case "tamil":
                    studentsWithSubject = studentsWithSubject.OrderByDescending(s => s.Subject.Tamil).ToList();
                    break;
                case "english":
                    studentsWithSubject = studentsWithSubject.OrderByDescending(s => s.Subject.English).ToList();
                    break;
                case "maths":
                    studentsWithSubject = studentsWithSubject.OrderByDescending(s => s.Subject.Maths).ToList();
                    break;
                case "science":
                    studentsWithSubject = studentsWithSubject.OrderByDescending(s => s.Subject.Science).ToList();
                    break;
                case "social":
                    studentsWithSubject = studentsWithSubject.OrderByDescending(s => s.Subject.Social).ToList();
                    break;
                default:
                    Console.WriteLine("Invalid subject!");
                    return;
            }

            foreach (var student in studentsWithSubject)
            {
                double subjectMark = 0;
                switch (subjectName.ToLower())
                {
                    case "tamil":
                        subjectMark = student.Subject.Tamil;
                        break;
                    case "english":
                        subjectMark = student.Subject.English;
                        break;
                    case "maths":
                        subjectMark = student.Subject.Maths;
                        break;
                    case "science":
                        subjectMark = student.Subject.Science;
                        break;
                    case "social":
                        subjectMark = student.Subject.Social;
                        break;
                }

                Console.WriteLine($"|  {student.Id,-6}|  {student.Name,-8}|  {subjectMark,-10}|");
            }

            Console.WriteLine("----------------------------------");
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            StudentManagementSystem sms = new StudentManagementSystem();
            Console.WriteLine("Student Management System");

            // Sample data
            sms.AddStudent(new Student { Id = 1, Name = "Alice", Mentor = "Anjali", Subject = new Subject { Tamil = 95, English = 85, Maths = 90, Science = 95, Social = 85 } });
            sms.AddStudent(new Student { Id = 2, Name = "Bob", Mentor = "Kaviya", Subject = new Subject { Tamil = 80, English = 75, Maths = 85, Science = 75, Social = 85 } });
            sms.AddStudent(new Student { Id = 3, Name = "Charlie", Mentor = "Anjali", Subject = new Subject { Tamil = 90, English = 85, Maths = 80, Science = 90, Social = 85 } });

            // Loop to keep the program running until the user chooses to exit
            bool exit = false;
            while (!exit)
            {
                // Display options to the user
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a student manually");
                Console.WriteLine("2. Remove a student");
                Console.WriteLine("3. Display all students");
                Console.WriteLine("4. Display the students based upon the highest score");
                Console.WriteLine("5. Display all the marks in subjectwise");
                Console.WriteLine("6. Display the marks based on particular Subject");
                Console.WriteLine("7. Exit");
                Console.WriteLine("---------------------------------------------------------");

                // Read user's choice
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter student details:");
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Tamil: ");
                        double tamil = double.Parse(Console.ReadLine());

                        Console.Write("English: ");
                        double english = double.Parse(Console.ReadLine());

                        Console.Write("Maths: ");
                        double maths = double.Parse(Console.ReadLine());

                        Console.Write("Science: ");
                        double science = double.Parse(Console.ReadLine());

                        Console.Write("Social: ");
                        double social = double.Parse(Console.ReadLine());


                        Console.Write("Mentor: ");
                        string mentor = Console.ReadLine();


                        sms.AddStudent(new Student { Id = id, Name = name, Mentor = mentor, Subject = new Subject { Tamil = tamil, English = english, Maths = maths, Science = science, Social = social } });
                        break;

                    case 2:
                        Console.WriteLine("Enter the ID of the student you want to remove:");
                        int idToRemove = int.Parse(Console.ReadLine());
                        sms.RemoveStudent(idToRemove);
                        break;

                    case 3:
                        sms.DisplayStudent();
                        break;

                    case 4:
                        sms.SortedStudent();
                        break;

                    case 5:
                        sms.DisplayAllMarks();
                        break;

                    case 6:
                        Console.WriteLine("Enter the subject (Tamil, English, Maths, Science, Social):");
                        string subjectInput = Console.ReadLine();
                        sms.DisplayStudentsBySubject(subjectInput);
                        break;


                    case 7:
                        exit = true; // Set exit flag to true to break the loop
                        break;

                    default:
                        Console.WriteLine("Invalid choice!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        break;
                }
            }
        }
    }
}