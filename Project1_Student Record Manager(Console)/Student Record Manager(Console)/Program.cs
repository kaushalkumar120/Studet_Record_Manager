using System;
using System.Collections.Generic;
using System.IO;

class Student
{
    public int RollNo { get; set; }
    public string Name { get; set; }
    public DateTime DOB { get; set; }
    public double Marks { get; set; }

    public int CalculateAge()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - DOB.Year;

        if (DOB.Date > today.AddYears(-age))
            age--;

        return age;
    }
}

class Program
{
    static List<Student> students = new List<Student>();
    static string fileName = "students.txt";

    static void Main()
    {
        LoadStudents();

        while (true)
        {
            Console.WriteLine("\n===== STUDENT RECORD MANAGER =====");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Save Records");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;

                case "2":
                    ViewStudents();
                    break;

                case "3":
                    SearchStudent();
                    break;

                case "4":
                    DeleteStudent();
                    break;

                case "5":
                    SaveStudents();
                    break;

                case "6":
                    SaveStudents();
                    Console.WriteLine("Program closed.");
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static void AddStudent()
    {
        Student s = new Student();

        Console.Write("Enter Roll No: ");
        s.RollNo = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Name: ");
        s.Name = Console.ReadLine().Trim();

        Console.Write("Enter DOB (dd/MM/yyyy): ");
        s.DOB = DateTime.ParseExact(
            Console.ReadLine(),
            "dd/MM/yyyy",
            null
        );

        Console.Write("Enter Marks: ");
        s.Marks = Convert.ToDouble(Console.ReadLine());

        students.Add(s);

        Console.WriteLine("Student added successfully.");
    }

    static void ViewStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No student records found.");
            return;
        }

        Console.WriteLine("\n----- STUDENT RECORDS -----");

        foreach (Student s in students)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Roll No : " + s.RollNo);
            Console.WriteLine("Name    : " + s.Name);
            Console.WriteLine("DOB     : " + s.DOB.ToString("dd/MM/yyyy"));
            Console.WriteLine("Age     : " + s.CalculateAge());
            Console.WriteLine("Marks   : " + s.Marks);
        }
    }

    static void SearchStudent()
    {
        Console.Write("Enter name to search: ");

        string searchName = Console.ReadLine()
                                    .Trim()
                                    .ToUpper();

        bool found = false;

        foreach (Student s in students)
        {
            if (s.Name.ToUpper().Contains(searchName))
            {
                Console.WriteLine("\nStudent Found:");
                Console.WriteLine("Roll No : " + s.RollNo);
                Console.WriteLine("Name    : " + s.Name);
                Console.WriteLine("DOB     : " + s.DOB.ToString("dd/MM/yyyy"));
                Console.WriteLine("Age     : " + s.CalculateAge());
                Console.WriteLine("Marks   : " + s.Marks);

                found = true;
            }
        }

        if (!found)
            Console.WriteLine("Student not found.");
    }

    static void DeleteStudent()
    {
        Console.Write("Enter Roll No to delete: ");
        int rollNo = Convert.ToInt32(Console.ReadLine());

        Student student = students.Find(s => s.RollNo == rollNo);

        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Student deleted successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void SaveStudents()
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Student s in students)
            {
                writer.WriteLine(
                    s.RollNo + "|" +
                    s.Name + "|" +
                    s.DOB.ToString("dd/MM/yyyy") + "|" +
                    s.Marks
                );
            }
        }

        Console.WriteLine("Records saved successfully.");
    }

    static void LoadStudents()
    {
        if (!File.Exists(fileName))
            return;

        string[] lines = File.ReadAllLines(fileName);

        foreach (string line in lines)
        {
            string[] data = line.Split('|');

            if (data.Length == 4)
            {
                Student s = new Student();

                s.RollNo = Convert.ToInt32(data[0]);
                s.Name = data[1];
                s.DOB = DateTime.ParseExact(
                    data[2],
                    "dd/MM/yyyy",
                    null
                );
                s.Marks = Convert.ToDouble(data[3]);

                students.Add(s);
            }
        }
    }
}
