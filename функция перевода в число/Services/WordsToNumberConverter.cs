using System.Text.RegularExpressions;
using WordToNumber.Interfaces;
using WordToNumber.Models;

namespace WordToNumber.Services
{

    // Основной конвертер слов в числа с поддержкой дробей и больших чисел

    public class WordsToNumberConverter : INumberConverter
    {
        private readonly BaseNumberParser _parser;
        private readonly NumberGrammarCheck _validator;

        // Конструктор с внедрением зависимостей
        public WordsToNumberConverter(BaseNumberParser parser, NumberGrammarCheck validator)
        {
            _parser = parser;
            _validator = validator;
        }


        //Основной метод конвертации

        public ParsedNumber ConvertWordsToNumber(string words)
        {
            var result = new ParsedNumber
            {
                OriginalText = words
            };

            try
            {
                // Шаг 1 проверка
                var validationResult = Validate(words, out var errors);
                if (!validationResult)
                {
                    result.HasErrors = true;
                    result.ErrorMessages = errors;
                    return result;
                }

                // Шаг 2 обработка
                words = PreprocessInput(words);

                // Шаг 3 тип числа
                if (IsFractionalNumber(words))
                {
                    result = ConvertFractionalNumber(words);
                }
                else if (IsNegativeNumber(words))
                {
                    result = ConvertNegativeNumber(words);
                }
                else
                {
                    result.NumerValue = ConvertIntegerNumber(words);
                }

                result.OriginalText = words;
            }
            catch (Exception ex)
            {
                result.HasErrors = true;
                result.ErrorMessages.Add($"Ошибка конвертации: {ex.Message}");
            }

            return result;
        }

 
        // Метод проверки из интерфейса

        public bool Validate(string words, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(words))
            {
                errors.Add("Входная строка пустая");
                return false;
            }

            var validationResult = _validator.Validate(words);
            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors);
                return false;
            }

            return true;
        }


        //обработка входной строки

        private string PreprocessInput(string input)
        {
            //к нижнему регистру
            input = input.ToLower().Trim();

            //Заменяем несколько пробелов на один
            input = Regex.Replace(input, @"\s+", " ");

            // Заменяем запятые в дробях на точку
            input = input.Replace(",", ".");

 
            //удаляем только отдельное слово "и"
            input = Regex.Replace(input, @"\bи\b", " ", RegexOptions.IgnoreCase);

            // Убираем лишние пробелы
            input = Regex.Replace(input, @"\s+", " ");

            return input.Trim();
        }


        //является ли число дробным

        private bool IsFractionalNumber(string words)
        {
            return words.Contains("цел") ||
                   words.Contains(".") ||
                   words.Contains("десятых") ||
                   words.Contains("сотых") ||
                   words.Contains("тысячных");
        }


        // Проверяет, является ли число отрицательным

        private bool IsNegativeNumber(string words)
        {
            return words.StartsWith("минус");
        }


        // целое число
        private long ConvertIntegerNumber(string words)
        {
            var wordArray = words.Split(' ');

            long total = 0;
            long currentBlock = 0; // Текущий блок чисел (до множителя)

            foreach (var word in wordArray)
            {
                if (string.IsNullOrWhiteSpace(word))
                    continue;

                var numberWord = _parser.GetNumberWord(word);
                if (numberWord == null)
                    continue;

                if (numberWord.Type == "множитель")
                {
                    // Если встретили множитель тысяча, миллион и т.д.
                    if (currentBlock == 0)
                    {
                        // Пример тысяча без числа перед ним означает 1 тысяча
                        currentBlock = 1;
                    }

                    total += currentBlock * numberWord.Value;
                    currentBlock = 0;
                }
                else
                {
                    // Добавляем число к текущему блоку
                    if (numberWord.Value >= 100)
                    {
                        // Сотни
                        if (currentBlock == 0)
                        {
                            currentBlock = numberWord.Value;
                        }
                        else
                        {
                            currentBlock += numberWord.Value;
                        }
                    }
                    else if (numberWord.Value >= 10)
                    {
                        // Десятки
                        currentBlock += numberWord.Value;
                    }
                    else
                    {
                        // Единицы
                        currentBlock += numberWord.Value;
                    }
                }
            }

            //последний блок если нет множителя в конце
            total += currentBlock;

            return total;
        }


        //дробное число

        private ParsedNumber ConvertFractionalNumber(string words)
        {
            var result = new ParsedNumber
            {
                HasDrobPart = true,
                OriginalText = words
            };

            //отрицательное ли
            bool isNegative = words.StartsWith("минус");
            if (isNegative)
            {
                words = words.Substring(5).Trim();
                result.ISNegative = true;
            }

            // Разделяем целую и дробную части
            string integerPartStr = "";
            string fractionalPartStr = "";

            if (words.Contains("цел"))
            {
                // Формат "две целых пять десятых"
                var parts = words.Split(new[] { "целых", "целая", "целое" },
                                       StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 1)
                    integerPartStr = parts[0].Trim();

                if (parts.Length >= 2)
                    fractionalPartStr = parts[1].Trim();
            }
            else if (words.Contains("."))
            {
                // Формат "двадцать три.пять"
                var parts = words.Split('.');
                if (parts.Length >= 1)
                    integerPartStr = parts[0].Trim();
                if (parts.Length >= 2)
                    fractionalPartStr = parts[1].Trim();
            }
            else
            {
                // Формат "пять десятых"
                fractionalPartStr = words.Trim();
            }

            // Конвертируем целую часть
            long integerPart = 0;
            if (!string.IsNullOrEmpty(integerPartStr))
            {
                integerPart = ConvertIntegerNumber(integerPartStr);
            }

            // Конвертируем дробную часть
            double fractionalPart = 0;
            if (!string.IsNullOrEmpty(fractionalPartStr))
            {
                fractionalPart = ConvertFractionalPart(fractionalPartStr);
            }

            // Собираем результат
            double finalResult = integerPart + fractionalPart;
            if (isNegative)
            {
                finalResult = -finalResult;
            }

            result.NumerValue = finalResult;
            return result;
        }


        // Конвертирует дробную часть (улучшенная версия)

        private double ConvertFractionalPart(string fractionalStr)
        {
            // Если это просто число например, "пять"
            if (_parser.IsNumberWord(fractionalStr))
            {
                var value = _parser.GetWordValue(fractionalStr) ?? 0;
                //разрядность по длине строки
                return value / Math.Pow(10, fractionalStr.Length);
            }

            // Если это пять десятых, три сотых и т.д.
            var words = fractionalStr.Split(' ');
            if (words.Length >= 2)
            {
                var numerator = _parser.GetWordValue(words[0]) ?? 0;
                var denominatorWord = words[1].ToLower();

                //знаменатель по слову
                double denominator = 1;
                if (denominatorWord.Contains("десят"))
                    denominator = 10;
                else if (denominatorWord.Contains("сот"))
                    denominator = 100;
                else if (denominatorWord.Contains("тысячн"))
                    denominator = 1000;
                else if (denominatorWord.Contains("десятитысячн"))
                    denominator = 10000;
                else if (denominatorWord.Contains("стотысячн"))
                    denominator = 100000;
                else if (denominatorWord.Contains("миллионн"))
                    denominator = 1000000;
                else
                {
                    //число из слова
                    foreach (var word in words)
                    {
                        if (_parser.IsNumberWord(word))
                        {
                            denominator = _parser.GetWordValue(word) ?? 1;
                            break;
                        }
                    }
                }

                return numerator / denominator;
            }

            return 0;
        }


        //отрицательное число

        private ParsedNumber ConvertNegativeNumber(string words)
        {
            // Убираем "минус"
            string positivePart = words.Substring(5).Trim();

            //положительная часть
            var positiveResult = ConvertWordsToNumber(positivePart);

            if (!positiveResult.HasErrors)
            {
                positiveResult.NumerValue = -positiveResult.NumerValue;
                positiveResult.ISNegative = true;
            }

            return positiveResult;
        }

        public bool Check(string words, out List<string> errors)
        {
            throw new NotImplementedException();
        }
    }
}