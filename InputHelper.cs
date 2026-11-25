using System;
using System.Globalization;

namespace StudentApp
{
    static class InputHelper
    {
        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                int number;

                if (int.TryParse(input, out number) && number >= min && number <= max)
                {
                    return number;
                }

                Console.WriteLine("Ошибка: введите целое число от " + min + " до " + max + "!");
            }
        }

        public static int ReadPositiveInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                int number;

                if (int.TryParse(input, out number) && number > 0)
                {
                    return number;
                }

                Console.WriteLine("Ошибка: введите целое число больше нуля!");
            }
        }

        public static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                double number;

                if (input != null && double.TryParse(input.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out number) && number >= min && number <= max)
                {
                    return number;
                }

                Console.WriteLine("Ошибка: введите число от " + min + " до " + max + "!");
            }
        }

        public static string ReadText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine("Ошибка: строка не должна быть пустой!");
            }
        }

        public static int ReadChoice(string title, string[] options)
        {
            Console.WriteLine(title);

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(i + " - " + options[i]);
            }

            return ReadInt("Ваш выбор: ", 0, options.Length - 1);
        }
    }
}
