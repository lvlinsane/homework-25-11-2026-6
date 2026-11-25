using System;
using System.Text;

namespace StudentApp
{
    class Program
    {
        static Student[] students = new Student[]
        {
            new Student(101, "Иванов Иван Иванович", 19, "CS-24", 89.5, EducationLevel.Bachelor),
            new Student(102, "Петрова Анна Сергеевна", 20, "CS-23", 92.3, EducationLevel.Bachelor),
            new Student(103, "Сидоров Пётр Алексеевич", 24, "MG-21", 78.3, EducationLevel.Master),
            new Student(104, "Козлова Мария Олеговна", 27, "DK-31", 95.7, EducationLevel.Doctoral),
            new Student(105, "Смирнов Максим Андреевич", 20, "CS-24", 54, EducationLevel.Bachelor)
        };

        static Course[] courses = new Course[]
        {
            new Course(1, "Программирование на C#", 6, 1, CourseType.Mandatory),
            new Course(2, "Высшая математика", 5, 1, CourseType.Mandatory),
            new Course(3, "Базы данных", 4, 2, CourseType.Mandatory),
            new Course(4, "Веб-дизайн", 3, 3, CourseType.Elective),
            new Course(5, "Английский для IT", 2, 4, CourseType.Optional)
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;

            while (running)
            {
                ShowMenu();
                int choice = InputHelper.ReadInt("Выберите действие: ", 0, 9);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        ShowStudents();
                        break;
                    case 3:
                        FindStudent();
                        break;
                    case 4:
                        ChangeStudentStatus();
                        break;
                    case 5:
                        ShowCourses();
                        break;
                    case 6:
                        AddCourse();
                        break;
                    case 7:
                        ShowBestStudent();
                        break;
                    case 8:
                        ShowStudentsByStatus();
                        break;
                    case 9:
                        ShowStatistics();
                        break;
                    case 0:
                        running = false;
                        Console.WriteLine("Работа программы завершена. До свидания!");
                        break;
                    default:
                        Console.WriteLine("Такого пункта меню нет!");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("===== Система учёта студентов =====");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Просмотреть список студентов");
            Console.WriteLine("3. Найти студента по ID");
            Console.WriteLine("4. Изменить статус студента");
            Console.WriteLine("5. Просмотреть учебные дисциплины");
            Console.WriteLine("6. Добавить дисциплину");
            Console.WriteLine("7. Найти лучшего студента");
            Console.WriteLine("8. Просмотреть студентов по статусу");
            Console.WriteLine("9. Статистика успеваемости");
            Console.WriteLine("0. Выход");
        }

        static void AddStudent()
        {
            int id = InputHelper.ReadPositiveInt("Введите ID студента: ");

            if (StudentManager.FindIndexById(students, id) != -1)
            {
                Console.WriteLine("Ошибка: студент с таким ID уже существует!");
                return;
            }

            string fullName = InputHelper.ReadText("Введите ФИО: ");
            int age = InputHelper.ReadInt("Введите возраст (16-60): ", 16, 60);
            string group = InputHelper.ReadText("Введите группу: ");
            double grade = InputHelper.ReadDouble("Введите средний балл (0-100): ", 0, 100);
            int level = InputHelper.ReadChoice("Выберите уровень образования:", Enum.GetNames(typeof(EducationLevel)));

            try
            {
                Student student = new Student(id, fullName, age, group, grade, (EducationLevel)level);
                Array.Resize(ref students, students.Length + 1);
                students[students.Length - 1] = student;
                Console.WriteLine("Студент добавлен! Начальный статус: Active.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void ShowStudents()
        {
            Console.WriteLine("===== Список студентов =====");
            StudentAnalyzer.PrintAll(students);
        }

        static void FindStudent()
        {
            int id = InputHelper.ReadPositiveInt("Введите ID студента: ");
            int index = StudentManager.FindIndexById(students, id);

            if (index == -1)
            {
                Console.WriteLine("Студент с ID " + id + " не найден!");
                return;
            }

            Console.WriteLine("Студент найден!");
            students[index].DisplayInfo();
        }

        static void ChangeStudentStatus()
        {
            if (StudentAnalyzer.IsEmpty(students))
            {
                Console.WriteLine("Список студентов пуст!");
                return;
            }

            int id = InputHelper.ReadPositiveInt("Введите ID студента: ");

            if (StudentManager.FindIndexById(students, id) == -1)
            {
                Console.WriteLine("Студент с ID " + id + " не найден!");
                return;
            }

            int status = InputHelper.ReadChoice("Выберите новый статус:", Enum.GetNames(typeof(StudyStatus)));
            StudentManager.ChangeStatusById(students, id, (StudyStatus)status);
        }

        static void ShowCourses()
        {
            Console.WriteLine("===== Учебные дисциплины =====");

            if (courses.Length == 0)
            {
                Console.WriteLine("Список дисциплин пуст!");
                return;
            }

            foreach (Course course in courses)
            {
                course.DisplayCourseInfo();
            }
        }

        static int FindCourseIndexById(int id)
        {
            for (int i = 0; i < courses.Length; i++)
            {
                if (courses[i].CourseId == id)
                {
                    return i;
                }
            }

            return -1;
        }

        static void AddCourse()
        {
            int id = InputHelper.ReadPositiveInt("Введите ID дисциплины: ");

            if (FindCourseIndexById(id) != -1)
            {
                Console.WriteLine("Ошибка: дисциплина с таким ID уже существует!");
                return;
            }

            string name = InputHelper.ReadText("Введите название дисциплины: ");
            int credits = InputHelper.ReadPositiveInt("Введите количество кредитов: ");
            int semester = InputHelper.ReadInt("Введите номер семестра (1-8): ", 1, 8);
            int type = InputHelper.ReadChoice("Выберите тип дисциплины:", Enum.GetNames(typeof(CourseType)));

            try
            {
                Course course = new Course(id, name, credits, semester, (CourseType)type);
                Array.Resize(ref courses, courses.Length + 1);
                courses[courses.Length - 1] = course;
                Console.WriteLine("Дисциплина добавлена!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void ShowBestStudent()
        {
            StudentAnalyzer.PrintBestStudent(students);
        }

        static void ShowStudentsByStatus()
        {
            if (StudentAnalyzer.IsEmpty(students))
            {
                Console.WriteLine("Список студентов пуст!");
                return;
            }

            int status = InputHelper.ReadChoice("Выберите статус:", Enum.GetNames(typeof(StudyStatus)));
            StudyStatus selected = (StudyStatus)status;
            int count = 0;

            foreach (Student student in students)
            {
                if (student.Status == selected)
                {
                    student.DisplayInfo();
                    count++;
                }
            }

            if (count == 0)
            {
                Console.WriteLine("Студентов со статусом " + selected + " нет.");
            }
            else
            {
                Console.WriteLine("Найдено студентов: " + count);
            }
        }

        static void ShowStatistics()
        {
            Console.WriteLine("===== Статистика успеваемости =====");

            if (StudentAnalyzer.IsEmpty(students))
            {
                Console.WriteLine("Список студентов пуст, статистики нет!");
                return;
            }

            double max = students[0].AverageGrade;
            double min = students[0].AverageGrade;
            int excellent = 0;
            int good = 0;
            int other = 0;

            for (int i = 0; i < students.Length; i++)
            {
                double grade = students[i].AverageGrade;

                if (grade > max)
                {
                    max = grade;
                }

                if (grade < min)
                {
                    min = grade;
                }

                if (grade >= 90)
                {
                    excellent++;
                }
                else if (grade >= 70)
                {
                    good++;
                }
                else
                {
                    other++;
                }
            }

            StudentAnalyzer.PrintAverageGrade(students);
            Console.WriteLine("Максимальный балл: " + max);
            Console.WriteLine("Минимальный балл: " + min);
            Console.WriteLine("Баллов 90 и выше: " + excellent);
            Console.WriteLine("Баллов от 70 до 89.99: " + good);
            Console.WriteLine("Баллов ниже 70: " + other);
            Console.WriteLine();
            Console.WriteLine("Количество студентов по статусам:");
            StudentAnalyzer.PrintCountByStatus(students);
        }
    }
}
