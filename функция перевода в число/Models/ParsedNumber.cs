using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordToNumber.Models
{
        //разбор числительных
    public class ParsedNumber
    {
        // значение
        public double NumerValue { get; set; }
        // оригинальный текст
        public string OriginalText { get; set; }
        // ошибки?
        public bool HasErrors { get; set; }
        //какие ошибки?
        public List<string> ErrorMessages { get; set; }
        //отрицательное число?
        public bool ISNegative { get; set; }
        // дробное число?
        public bool HasDrobPart { get; set; }
        // конструкторв
        public ParsedNumber() 
        { 
            ErrorMessages = new List<string>();
        }

        public ParsedNumber(double value, string text)
        {
            NumerValue = value;
            OriginalText = text;
            ErrorMessages = new List<string>();
        }
    }
}
