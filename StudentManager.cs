using System;

namespace StudentApp
{
    static class StudentManager
    {
        public static int FindIndexById(Student[] students, int id)
        {
            if (students == null)
            {
                return -1;
            }

            for (int i = 0; i < students.Length; i++)
            {
                if (students[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        public static bool ChangeStatusById(Student[] students, int id, StudyStatus newStatus)
        {
            if (students == null || students.Length == 0)
            {
                Console.WriteLine("Список студентов пуст!");
                return false;
            }

            int index = FindIndexById(students, id);

            if (index == -1)
            {
                Console.WriteLine("Студент с Id " + id + " не найден!");
                return false;
            }

            return students[index].ChangeStatus(newStatus);
        }
    }
}
