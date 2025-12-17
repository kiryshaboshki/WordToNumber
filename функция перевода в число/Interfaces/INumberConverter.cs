using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordToNumber.Interfaces
{
    //интерфейс для конвертера слов в числа 
    public interface INumberConverter
    {
        // метод конвертации
        Models.ParsedNumber ConvertWordsToNumber(string words);
        // валидации
        bool Check(string words, out List<string> errors);
    }
}
