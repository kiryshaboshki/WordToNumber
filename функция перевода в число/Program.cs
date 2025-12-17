using System;
using WordToNumber.Models;
using WordToNumber.Services;
using WordToNumber.Models;
using WordToNumber.Services;

namespace WordToNumberProject
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Конвертер слов в числа");
            Console.WriteLine("Поддерживаемые функции:");
            Console.WriteLine("1. Целые числа (один, двадцать три, сто пять)");
            Console.WriteLine("2. Большие числа (тысяча, миллион, миллиард)");
            Console.WriteLine("3. Дробные числа (две целых пять десятых)");
            Console.WriteLine("4. Отрицательные числа (минус пять)");
            Console.WriteLine("5. Падежи (двух тысяч, трёхстам)");
            Console.WriteLine("Для выхода введите 'exit'");
            Console.WriteLine(new string('-', 50));

            // Создаем зависимости
            var parser = new BaseNumberParser();
            var validator = new NumberGrammarCheck(parser);
            var converter = new WordsToNumberConverter(parser, validator);

            while (true)
            {
                Console.Write("\nВведите число словами: ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    continue;

                if (input.ToLower() == "exit")
                    break;

                try
                {
                    // Конвертируем
                    ParsedNumber result = converter.ConvertWordsToNumber(input);

                    // Выводим результат
                    Console.WriteLine("\nРезультат:");
                    Console.WriteLine($"Текст: {result.OriginalText}");

                    if (result.HasErrors)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Обнаружены ошибки:");
                        foreach (var error in result.ErrorMessages)
                        {
                            Console.WriteLine($"  • {error}");
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Число: {result.NumerValue}");

                        // Дополнительная информация
                        if (result.ISNegative)
                            Console.WriteLine("Отрицательное число");
                        if (result.HasDrobPart)
                            Console.WriteLine("Дробное число");

                        Console.ResetColor();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.ResetColor();
                }

                Console.WriteLine(new string('-', 30));
            }

            Console.WriteLine("\nСпасибо за использование программы!");
        }
    }
}