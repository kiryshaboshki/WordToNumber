using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordToNumber.Models
{
    public class NumberWord
    {
        public string BaseForm {  get; set; }//формы слов

        public long Value { get; set; } // само число 

        public string Type { get; set; } // единица десяток и т.д

        public Dictionary<Enums.GrammaticalCase, List<string>> AllForms { get; set; }

        public string Gender { get; set; } //hjl


        // конструктор
        public NumberWord(string baseform, long value, string type, 
            string gender = null)
        {
            BaseForm = baseform;
            Value = value;
            Type = type;
            Gender = gender;
            AllForms = new Dictionary<Enums.GrammaticalCase, List<string>>();
        }
        //добавление форм слов
        public void AddForm(Enums.GrammaticalCase grammaticalCase, params  string[] forms)
        {
            if (!AllForms.ContainsKey(grammaticalCase))
            {
                AllForms[grammaticalCase] = new List<string>();
            }
            AllForms[grammaticalCase].AddRange(forms);
        }
        //слово - форма этого числительного????
        public bool IsFormOf(string word)
        {
            string lowerWord = word.ToLower();

            foreach (var forms in AllForms.Values)
            {
                if (forms.Contains(lowerWord))
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetBaseFormFromAny(string word, List<NumberWord> dictionary)
        {
            foreach (var numberWord in dictionary)
            {
                if (numberWord.IsFormOf(word))
                {
                    return numberWord.BaseForm;
                }
            }
            return null;
        }



    }
}
