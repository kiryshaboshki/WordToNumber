using System.Text.RegularExpressions;

namespace WordToNumber.Services
{
    // проверка грамматической корректности числительных
    public class NumberGrammarCheck
    {
        private readonly BaseNumberParser _parser;

        // Конструктор принимает парсер
        public NumberGrammarCheck(BaseNumberParser parser)
        {
            _parser = parser;
        }

        //метод проверки
        public ValidationResult Validate(string input)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(input))
            {
                result.AddError("Входная строка пустая");
                return result;
            }

            //в нижний регистр и убираем лишние пробелы
            input = input.ToLower().Trim();
            input = Regex.Replace(input, @"\s+", " ");

            //наличие недопустимых символов
            if (!IsValidCharacters(input))
            {
                result.AddError("Строка содержит недопустимые символы");
            }

            //разбиваем на слова
            var words = input.Split(' ');

            // Проверяем каждое слово
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];

                // Пропускаем служебные слова
                if (IsServiceWord(word))
                    continue;

                // Проверяем дефисные конструкции
                if (word.Contains("-"))
                {
                    var parts = word.Split('-');
                    foreach (var part in parts)
                    {
                        if (!_parser.IsNumberWord(part))
                        {
                            result.AddError($"Часть '{part}' в слове '{word}' не является числительным");
                        }
                    }
                }
                else
                {
                    if (!_parser.IsNumberWord(word))
                    {
                        result.AddError($"Слово '{word}' не является числительным");
                    }
                }
            }

            //порядок слов (например, нельзя сто один тысяча)
            CheckWordOrder(words, result);

            //согласование родов
            CheckGenderAgreement(words, result);

            //падежное согласование
            CheckCaseAgreement(words, result);

            return result;
        }


        //допустимые символы

        private bool IsValidCharacters(string input)
        {
            // Разрешаем русские буквы, пробелы, дефисы, запятые, точки и слово "минус"
            var regex = new Regex(@"^[а-яё\s\-,\.минус]+$", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }


        //является ли слово служебным

        private bool IsServiceWord(string word)
        {
            var serviceWords = new List<string>
            {
                "и", "целых", "целая", "целое",
                "десятых", "сотых", "тысячных",
                "десятитысячных", "стотысячных", "миллионных",
                "минус", "о", "об", "во"
            };

            return serviceWords.Contains(word);
        }

  
        // порядок слов в числительном

        private void CheckWordOrder(string[] words, ValidationResult result)
        {
            //только числительные слова (без служебных)
            var numberWords = words
                .Where(w => !IsServiceWord(w) && _parser.IsNumberWord(w))
                .Select(w => _parser.GetNumberWord(w))
                .ToList();

            for (int i = 0; i < numberWords.Count - 1; i++)
            {
                var current = numberWords[i];
                var next = numberWords[i + 1];

                // Правило: большие разряды идут перед меньшими
                // (кроме составных чисел типо "двадцать один")
                if (current.Type == "множитель" && next.Type == "множитель")
                {
                    if (current.Value < next.Value)
                    {
                        result.AddError($"Неправильный порядок: '{current.BaseForm}' не может идти перед '{next.BaseForm}'");
                    }
                }
            }
        }

        //согласование родов
        private void CheckGenderAgreement(string[] words, ValidationResult result)
        {
            // Ищем множители тысячи, миллионы и т.д.
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                var numberWord = _parser.GetNumberWord(word);

                if (numberWord != null && numberWord.Type == "множитель")
                {
                    // Для тысяч (женский род) проверяем предыдущее числительное
                    if (numberWord.BaseForm == "тысяча" && i > 0)
                    {
                        var prevWord = _parser.GetNumberWord(words[i - 1]);
                        if (prevWord != null && prevWord.Value < 10)
                        {
                            // 1 тысяча, 2 тысячи, 3 тысячи, 4 тысячи, 5 тысяч...
                            if (prevWord.Value == 1 && numberWord.BaseForm != "тысяча")
                            {
                                result.AddError("С числом 1 тысяча должна быть в единственном числе");
                            }
                            else if (prevWord.Value >= 2 && prevWord.Value <= 4)
                            {
                                if (!numberWord.IsFormOf("тысячи"))
                                {
                                    result.AddError($"С числом {prevWord.Value} должно быть 'тысячи'");
                                }
                            }
                            else if (prevWord.Value >= 5)
                            {
                                if (!numberWord.IsFormOf("тысяч"))
                                {
                                    result.AddError($"С числом {prevWord.Value} должно быть 'тысяч'");
                                }
                            }
                        }
                    }
                }
            }
        }


        // Проверяет падежное согласование упрощенная версия

        private void CheckCaseAgreement(string[] words, ValidationResult result)
        {
            // Для простоты (лени) проверяем только очевидные ошибки

            bool hasPreposition = false;
            for (int i = 0; i < words.Length; i++)
            {
                // Если есть предлог, следующий падеж должен быть предложным
                if (words[i] == "о" || words[i] == "об" || words[i] == "во")
                {
                    hasPreposition = true;
                    if (i + 1 < words.Length)
                    {
                        // Проверяем, что следующее слово в правильном падеже

                    }
                }
            }
        }

        // Результат проверки

        public class ValidationResult
        {
            public bool IsValid { get; private set; }
            public List<string> Errors { get; private set; }

            public ValidationResult()
            {
                IsValid = true;
                Errors = new List<string>();
            }

            public void AddError(string error)
            {
                IsValid = false;
                Errors.Add(error);
            }
        }
    }
}