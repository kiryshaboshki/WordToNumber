using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordToNumber.Enums;
using WordToNumber.Models;

namespace WordToNumber.Services
{
    // базовый парсер который знает числительные и их формы
    public class BaseNumberParser
    {
        // словарь всез числительных
        protected List<NumberWord> NumberDictionary { get; private set; }

        public BaseNumberParser()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            NumberDictionary = new List<NumberWord>();
            // единицы
            AddUnits();

            AddTens(); //десятки

            AddHundreds(); // сотни

            AddMultipliers(); // множитель для больших чисел 


        }

        private void AddUnits()
        {
            // ноль
            var zero = new NumberWord("ноль", 0, "единица");
            zero.AddForm(GrammaticalCase.Nominative, "ноль");
            zero.AddForm(GrammaticalCase.Genitive, "ноля");
            zero.AddForm(GrammaticalCase.Dative, "нолю");
            zero.AddForm(GrammaticalCase.Accusative, "ноль");
            zero.AddForm(GrammaticalCase.Instrumental, "нолём");
            zero.AddForm(GrammaticalCase.Prepositional, "ноле");
            NumberDictionary.Add(zero);


            // 1 (мужской род) - для миллионов, миллиардов
            var oneM = new NumberWord("один", 1, "единица", "мужской");
            oneM.AddForm(GrammaticalCase.Nominative, "один");
            oneM.AddForm(GrammaticalCase.Genitive, "одного");
            oneM.AddForm(GrammaticalCase.Dative, "одному");
            oneM.AddForm(GrammaticalCase.Accusative, "один");
            oneM.AddForm(GrammaticalCase.Instrumental, "одним");
            oneM.AddForm(GrammaticalCase.Prepositional, "одном");
            NumberDictionary.Add(oneM);

            // 1 (женский род) - для тысяч
            var oneF = new NumberWord("одна", 1, "единица", "женский");
            oneF.AddForm(GrammaticalCase.Nominative, "одна");
            oneF.AddForm(GrammaticalCase.Genitive, "одной");
            oneF.AddForm(GrammaticalCase.Dative, "одной");
            oneF.AddForm(GrammaticalCase.Accusative, "одну");
            oneF.AddForm(GrammaticalCase.Instrumental, "одной");
            oneF.AddForm(GrammaticalCase.Prepositional, "одной");
            NumberDictionary.Add(oneF);

            // 1 (средний род) - для некоторых конструкций
            var oneN = new NumberWord("одно", 1, "единица", "средний");
            oneN.AddForm(GrammaticalCase.Nominative, "одно");
            oneN.AddForm(GrammaticalCase.Genitive, "одного");
            oneN.AddForm(GrammaticalCase.Dative, "одному");
            oneN.AddForm(GrammaticalCase.Accusative, "одно");
            oneN.AddForm(GrammaticalCase.Instrumental, "одним");
            oneN.AddForm(GrammaticalCase.Prepositional, "одном");
            NumberDictionary.Add(oneN);

            // 2 (мужской/средний род)
            var twoM = new NumberWord("два", 2, "единица", "мужской");
            twoM.AddForm(GrammaticalCase.Nominative, "два");
            twoM.AddForm(GrammaticalCase.Genitive, "двух");
            twoM.AddForm(GrammaticalCase.Dative, "двум");
            twoM.AddForm(GrammaticalCase.Accusative, "два");
            twoM.AddForm(GrammaticalCase.Instrumental, "двумя");
            twoM.AddForm(GrammaticalCase.Prepositional, "двух");
            NumberDictionary.Add(twoM);

            // 2 (женский род) - для тысяч
            var twoF = new NumberWord("две", 2, "единица", "женский");
            twoF.AddForm(GrammaticalCase.Nominative, "две");
            twoF.AddForm(GrammaticalCase.Genitive, "двух");
            twoF.AddForm(GrammaticalCase.Dative, "двум");
            twoF.AddForm(GrammaticalCase.Accusative, "две");
            twoF.AddForm(GrammaticalCase.Instrumental, "двумя");
            twoF.AddForm(GrammaticalCase.Prepositional, "двух");
            NumberDictionary.Add(twoF);

            // 3
            var three = new NumberWord("три", 3, "единица");
            three.AddForm(GrammaticalCase.Nominative, "три");
            three.AddForm(GrammaticalCase.Genitive, "трёх", "трех");  // две формы: с ё и с е
            three.AddForm(GrammaticalCase.Dative, "трём", "трем");
            three.AddForm(GrammaticalCase.Accusative, "три");
            three.AddForm(GrammaticalCase.Instrumental, "тремя");
            three.AddForm(GrammaticalCase.Prepositional, "трёх", "трех");
            NumberDictionary.Add(three);

            // 4
            var four = new NumberWord("четыре", 4, "единица");
            four.AddForm(GrammaticalCase.Nominative, "четыре");
            four.AddForm(GrammaticalCase.Genitive, "четырёх", "четырех");
            four.AddForm(GrammaticalCase.Dative, "четырём", "четырем");
            four.AddForm(GrammaticalCase.Accusative, "четыре");
            four.AddForm(GrammaticalCase.Instrumental, "четырьмя");
            four.AddForm(GrammaticalCase.Prepositional, "четырёх", "четырех");
            NumberDictionary.Add(four);

            // 5
            var five = new NumberWord("пять", 5, "единица");
            five.AddForm(GrammaticalCase.Nominative, "пять");
            five.AddForm(GrammaticalCase.Genitive, "пяти");
            five.AddForm(GrammaticalCase.Dative, "пяти");
            five.AddForm(GrammaticalCase.Accusative, "пять");
            five.AddForm(GrammaticalCase.Instrumental, "пятью");
            five.AddForm(GrammaticalCase.Prepositional, "пяти");
            NumberDictionary.Add(five);

            // 6
            var six = new NumberWord("шесть", 6, "единица");
            six.AddForm(GrammaticalCase.Nominative, "шесть");
            six.AddForm(GrammaticalCase.Genitive, "шести");
            six.AddForm(GrammaticalCase.Dative, "шести");
            six.AddForm(GrammaticalCase.Accusative, "шесть");
            six.AddForm(GrammaticalCase.Instrumental, "шестью");
            six.AddForm(GrammaticalCase.Prepositional, "шести");
            NumberDictionary.Add(six);

            // 7
            var seven = new NumberWord("семь", 7, "единица");
            seven.AddForm(GrammaticalCase.Nominative, "семь");
            seven.AddForm(GrammaticalCase.Genitive, "семи");
            seven.AddForm(GrammaticalCase.Dative, "семи");
            seven.AddForm(GrammaticalCase.Accusative, "семь");
            seven.AddForm(GrammaticalCase.Instrumental, "семью");
            seven.AddForm(GrammaticalCase.Prepositional, "семи");
            NumberDictionary.Add(seven);

            // 8
            var eight = new NumberWord("восемь", 8, "единица");
            eight.AddForm(GrammaticalCase.Nominative, "восемь");
            eight.AddForm(GrammaticalCase.Genitive, "восьми");
            eight.AddForm(GrammaticalCase.Dative, "восьми");
            eight.AddForm(GrammaticalCase.Accusative, "восемь");
            eight.AddForm(GrammaticalCase.Instrumental, "восемью", "восьмью");
            eight.AddForm(GrammaticalCase.Prepositional, "восьми");
            NumberDictionary.Add(eight);

            // 9
            var nine = new NumberWord("девять", 9, "единица");
            nine.AddForm(GrammaticalCase.Nominative, "девять");
            nine.AddForm(GrammaticalCase.Genitive, "девяти");
            nine.AddForm(GrammaticalCase.Dative, "девяти");
            nine.AddForm(GrammaticalCase.Accusative, "девять");
            nine.AddForm(GrammaticalCase.Instrumental, "девятью");
            nine.AddForm(GrammaticalCase.Prepositional, "девяти");
            NumberDictionary.Add(nine);

            // 10
            var ten = new NumberWord("десять", 10, "единица");
            ten.AddForm(GrammaticalCase.Nominative, "десять");
            ten.AddForm(GrammaticalCase.Genitive, "десяти");
            ten.AddForm(GrammaticalCase.Dative, "десяти");
            ten.AddForm(GrammaticalCase.Accusative, "десять");
            ten.AddForm(GrammaticalCase.Instrumental, "десятью");
            ten.AddForm(GrammaticalCase.Prepositional, "девяти");
            NumberDictionary.Add(ten);

            // 11
            var eleven = new NumberWord("одиннадцать", 11, "единица");
            eleven.AddForm(GrammaticalCase.Nominative, "одиннадцать");
            eleven.AddForm(GrammaticalCase.Genitive, "одиннадцати");
            eleven.AddForm(GrammaticalCase.Dative, "одиннадцати");
            eleven.AddForm(GrammaticalCase.Accusative, "одиннадцать");
            eleven.AddForm(GrammaticalCase.Instrumental, "одиннадцатью");
            eleven.AddForm(GrammaticalCase.Prepositional, "одиннадцати");
            NumberDictionary.Add(eleven);

            // 12
            var twelve = new NumberWord("двенадцать", 12, "единица");
            twelve.AddForm(GrammaticalCase.Nominative, "двенадцать");
            twelve.AddForm(GrammaticalCase.Genitive, "двенадцати");
            twelve.AddForm(GrammaticalCase.Dative, "двенадцати");
            twelve.AddForm(GrammaticalCase.Accusative, "двенадцать");
            twelve.AddForm(GrammaticalCase.Instrumental, "двенадцатью");
            twelve.AddForm(GrammaticalCase.Prepositional, "двенадцати");
            NumberDictionary.Add(twelve);

            // 13
            var thirteen = new NumberWord("тринадцать", 13, "единица");
            thirteen.AddForm(GrammaticalCase.Nominative, "тринадцать");
            thirteen.AddForm(GrammaticalCase.Genitive, "тринадцати");
            thirteen.AddForm(GrammaticalCase.Dative, "тринадцати");
            thirteen.AddForm(GrammaticalCase.Accusative, "тринадцать");
            thirteen.AddForm(GrammaticalCase.Instrumental, "тринадцатью");
            thirteen.AddForm(GrammaticalCase.Prepositional, "тринадцати");
            NumberDictionary.Add(thirteen);

            // 14
            var fourteen = new NumberWord("четырнадцать", 14, "единица");
            fourteen.AddForm(GrammaticalCase.Nominative, "четырнадцать");
            fourteen.AddForm(GrammaticalCase.Genitive, "четырнадцати");
            fourteen.AddForm(GrammaticalCase.Dative, "четырнадцати");
            fourteen.AddForm(GrammaticalCase.Accusative, "четырнадцать");
            fourteen.AddForm(GrammaticalCase.Instrumental, "четырнадцатью");
            fourteen.AddForm(GrammaticalCase.Prepositional, "четырнадцати");
            NumberDictionary.Add(fourteen);

            // 15
            var fifteen = new NumberWord("пятнадцать", 15, "единица");
            fifteen.AddForm(GrammaticalCase.Nominative, "пятнадцать");
            fifteen.AddForm(GrammaticalCase.Genitive, "пятнадцати");
            fifteen.AddForm(GrammaticalCase.Dative, "пятнадцати");
            fifteen.AddForm(GrammaticalCase.Accusative, "пятнадцать");
            fifteen.AddForm(GrammaticalCase.Instrumental, "пятнадцатью");
            fifteen.AddForm(GrammaticalCase.Prepositional, "пятнадцати");
            NumberDictionary.Add(fifteen);

            // 16
            var sixteen = new NumberWord("шестнадцать", 16, "единица");
            sixteen.AddForm(GrammaticalCase.Nominative, "шестнадцать");
            sixteen.AddForm(GrammaticalCase.Genitive, "шестнадцати");
            sixteen.AddForm(GrammaticalCase.Dative, "шестнадцати");
            sixteen.AddForm(GrammaticalCase.Accusative, "шестнадцать");
            sixteen.AddForm(GrammaticalCase.Instrumental, "шестнадцатью");
            sixteen.AddForm(GrammaticalCase.Prepositional, "шестнадцати");
            NumberDictionary.Add(sixteen);

            // 17
            var seventeen = new NumberWord("семнадцать", 17, "единица");
            seventeen.AddForm(GrammaticalCase.Nominative, "семнадцать");
            seventeen.AddForm(GrammaticalCase.Genitive, "семнадцати");
            seventeen.AddForm(GrammaticalCase.Dative, "семнадцати");
            seventeen.AddForm(GrammaticalCase.Accusative, "семнадцать");
            seventeen.AddForm(GrammaticalCase.Instrumental, "семнадцатью");
            seventeen.AddForm(GrammaticalCase.Prepositional, "семнадцати");
            NumberDictionary.Add(seventeen);

            // 18
            var eighteen = new NumberWord("восемнадцать", 18, "единица");
            eighteen.AddForm(GrammaticalCase.Nominative, "восемнадцать");
            eighteen.AddForm(GrammaticalCase.Genitive, "восемнадцати");
            eighteen.AddForm(GrammaticalCase.Dative, "восемнадцати");
            eighteen.AddForm(GrammaticalCase.Accusative, "восемнадцать");
            eighteen.AddForm(GrammaticalCase.Instrumental, "восемнадцатью");
            eighteen.AddForm(GrammaticalCase.Prepositional, "восемнадцати");
            NumberDictionary.Add(eighteen);

            // 19
            var nineteen = new NumberWord("девятнадцать", 19, "единица");
            nineteen.AddForm(GrammaticalCase.Nominative, "девятнадцать");
            nineteen.AddForm(GrammaticalCase.Genitive, "девятнадцати");
            nineteen.AddForm(GrammaticalCase.Dative, "девятнадцати");
            nineteen.AddForm(GrammaticalCase.Accusative, "девятнадцать");
            nineteen.AddForm(GrammaticalCase.Instrumental, "девятнадцатью");
            nineteen.AddForm(GrammaticalCase.Prepositional, "девятнадцати");
            NumberDictionary.Add(nineteen);
        }

        private void AddTens()
        {
            // 20
            var twenty = new NumberWord("двадцать", 20, "десяток");
            twenty.AddForm(GrammaticalCase.Nominative, "двадцать");
            twenty.AddForm(GrammaticalCase.Genitive, "двадцати");
            twenty.AddForm(GrammaticalCase.Dative, "двадцати");
            twenty.AddForm(GrammaticalCase.Accusative, "двадцать");
            twenty.AddForm(GrammaticalCase.Instrumental, "двадцатью");
            twenty.AddForm(GrammaticalCase.Prepositional, "двадцати");
            NumberDictionary.Add(twenty);

            // 30
            var thirty = new NumberWord("тридцать", 30, "десяток");
            thirty.AddForm(GrammaticalCase.Nominative, "тридцать");
            thirty.AddForm(GrammaticalCase.Genitive, "тридцати");
            thirty.AddForm(GrammaticalCase.Dative, "тридцати");
            thirty.AddForm(GrammaticalCase.Accusative, "тридцать");
            thirty.AddForm(GrammaticalCase.Instrumental, "тридцатью");
            thirty.AddForm(GrammaticalCase.Prepositional, "тридцати");
            NumberDictionary.Add(thirty);

            // 40
            var forty = new NumberWord("сорок", 40, "десяток");
            forty.AddForm(GrammaticalCase.Nominative, "сорок");
            forty.AddForm(GrammaticalCase.Genitive, "сорока");
            forty.AddForm(GrammaticalCase.Dative, "сорока");
            forty.AddForm(GrammaticalCase.Accusative, "сорок");
            forty.AddForm(GrammaticalCase.Instrumental, "сорока");
            forty.AddForm(GrammaticalCase.Prepositional, "сорока");
            NumberDictionary.Add(forty);

            // 50
            var fifty = new NumberWord("пятьдесят", 50, "десяток");
            fifty.AddForm(GrammaticalCase.Nominative, "пятьдесят");
            fifty.AddForm(GrammaticalCase.Genitive, "пятидесяти");
            fifty.AddForm(GrammaticalCase.Dative, "пятидесяти");
            fifty.AddForm(GrammaticalCase.Accusative, "пятьдесят");
            fifty.AddForm(GrammaticalCase.Instrumental, "пятьюдесятью");
            fifty.AddForm(GrammaticalCase.Prepositional, "пятидесяти");
            NumberDictionary.Add(fifty);

            // 60
            var sixty = new NumberWord("шестьдесят", 60, "десяток");
            sixty.AddForm(GrammaticalCase.Nominative, "шестьдесят");
            sixty.AddForm(GrammaticalCase.Genitive, "шестидесяти");
            sixty.AddForm(GrammaticalCase.Dative, "шестидесяти");
            sixty.AddForm(GrammaticalCase.Accusative, "шестьдесят");
            sixty.AddForm(GrammaticalCase.Instrumental, "шестьюдесятью");
            sixty.AddForm(GrammaticalCase.Prepositional, "шестидесяти");
            NumberDictionary.Add(sixty);

            // 70
            var seventy = new NumberWord("семьдесят", 70, "десяток");
            seventy.AddForm(GrammaticalCase.Nominative, "семьдесят");
            seventy.AddForm(GrammaticalCase.Genitive, "семидесяти");
            seventy.AddForm(GrammaticalCase.Dative, "семидесяти");
            seventy.AddForm(GrammaticalCase.Accusative, "семьдесят");
            seventy.AddForm(GrammaticalCase.Instrumental, "семьюдесятью");
            seventy.AddForm(GrammaticalCase.Prepositional, "семидесяти");
            NumberDictionary.Add(seventy);

            // 80
            var eighty = new NumberWord("восемьдесят", 80, "десяток");
            eighty.AddForm(GrammaticalCase.Nominative, "восемьдесят");
            eighty.AddForm(GrammaticalCase.Genitive, "восьмидесяти");
            eighty.AddForm(GrammaticalCase.Dative, "восьмидесяти");
            eighty.AddForm(GrammaticalCase.Accusative, "восемьдесят");
            eighty.AddForm(GrammaticalCase.Instrumental, "восемьюдесятью", "восьмьюдесятью");
            eighty.AddForm(GrammaticalCase.Prepositional, "восьмидесяти");
            NumberDictionary.Add(eighty);

            // 90
            var ninety = new NumberWord("девяносто", 90, "десяток");
            ninety.AddForm(GrammaticalCase.Nominative, "девяносто");
            ninety.AddForm(GrammaticalCase.Genitive, "девяноста");
            ninety.AddForm(GrammaticalCase.Dative, "девяноста");
            ninety.AddForm(GrammaticalCase.Accusative, "девяносто");
            ninety.AddForm(GrammaticalCase.Instrumental, "девяноста");
            ninety.AddForm(GrammaticalCase.Prepositional, "девяноста");
            NumberDictionary.Add(ninety);
        }

        private void AddHundreds()
        {
            // 100
            var hundred = new NumberWord("сто", 100, "сотня");
            hundred.AddForm(GrammaticalCase.Nominative, "сто");
            hundred.AddForm(GrammaticalCase.Genitive, "ста");
            hundred.AddForm(GrammaticalCase.Dative, "ста");
            hundred.AddForm(GrammaticalCase.Accusative, "сто");
            hundred.AddForm(GrammaticalCase.Instrumental, "ста");
            hundred.AddForm(GrammaticalCase.Prepositional, "ста");
            NumberDictionary.Add(hundred);

            // 200
            var twoHundred = new NumberWord("двести", 200, "сотня");
            twoHundred.AddForm(GrammaticalCase.Nominative, "двести");
            twoHundred.AddForm(GrammaticalCase.Genitive, "двухсот");
            twoHundred.AddForm(GrammaticalCase.Dative, "двумстам");
            twoHundred.AddForm(GrammaticalCase.Accusative, "двести");
            twoHundred.AddForm(GrammaticalCase.Instrumental, "двумястами");
            twoHundred.AddForm(GrammaticalCase.Prepositional, "двухстах");
            NumberDictionary.Add(twoHundred);

            // 300
            var threeHundred = new NumberWord("триста", 300, "сотня");
            threeHundred.AddForm(GrammaticalCase.Nominative, "триста");
            threeHundred.AddForm(GrammaticalCase.Genitive, "трёхсот", "трехсот");
            threeHundred.AddForm(GrammaticalCase.Dative, "трёмстам", "тремстам");
            threeHundred.AddForm(GrammaticalCase.Accusative, "триста");
            threeHundred.AddForm(GrammaticalCase.Instrumental, "тремястами");
            threeHundred.AddForm(GrammaticalCase.Prepositional, "трёхстах", "трехстах");
            NumberDictionary.Add(threeHundred);

            // 400
            var fourHundred = new NumberWord("четыреста", 400, "сотня");
            fourHundred.AddForm(GrammaticalCase.Nominative, "четыреста");
            fourHundred.AddForm(GrammaticalCase.Genitive, "четырёхсот", "четырехсот");
            fourHundred.AddForm(GrammaticalCase.Dative, "четырёмстам", "четыремстам");
            fourHundred.AddForm(GrammaticalCase.Accusative, "четыреста");
            fourHundred.AddForm(GrammaticalCase.Instrumental, "четырьмястами");
            fourHundred.AddForm(GrammaticalCase.Prepositional, "четырёхстах", "четырехстах");
            NumberDictionary.Add(fourHundred);

            // 500
            var fiveHundred = new NumberWord("пятьсот", 500, "сотня");
            fiveHundred.AddForm(GrammaticalCase.Nominative, "пятьсот");
            fiveHundred.AddForm(GrammaticalCase.Genitive, "пятисот");
            fiveHundred.AddForm(GrammaticalCase.Dative, "пятистам");
            fiveHundred.AddForm(GrammaticalCase.Accusative, "пятьсот");
            fiveHundred.AddForm(GrammaticalCase.Instrumental, "пятьюстами");
            fiveHundred.AddForm(GrammaticalCase.Prepositional, "пятистах");
            NumberDictionary.Add(fiveHundred);

            // 600
            var sixHundred = new NumberWord("шестьсот", 600, "сотня");
            sixHundred.AddForm(GrammaticalCase.Nominative, "шестьсот");
            sixHundred.AddForm(GrammaticalCase.Genitive, "шестисот");
            sixHundred.AddForm(GrammaticalCase.Dative, "шестистам");
            sixHundred.AddForm(GrammaticalCase.Accusative, "шестьсот");
            sixHundred.AddForm(GrammaticalCase.Instrumental, "шестьюстами");
            sixHundred.AddForm(GrammaticalCase.Prepositional, "шестистах");
            NumberDictionary.Add(sixHundred);

            // 700
            var sevenHundred = new NumberWord("семьсот", 700, "сотня");
            sevenHundred.AddForm(GrammaticalCase.Nominative, "семьсот");
            sevenHundred.AddForm(GrammaticalCase.Genitive, "семисот");
            sevenHundred.AddForm(GrammaticalCase.Dative, "семистам");
            sevenHundred.AddForm(GrammaticalCase.Accusative, "семьсот");
            sevenHundred.AddForm(GrammaticalCase.Instrumental, "семьюстами");
            sevenHundred.AddForm(GrammaticalCase.Prepositional, "семистах");
            NumberDictionary.Add(sevenHundred);

            // 800
            var eightHundred = new NumberWord("восемьсот", 800, "сотня");
            eightHundred.AddForm(GrammaticalCase.Nominative, "восемьсот");
            eightHundred.AddForm(GrammaticalCase.Genitive, "восьмисот");
            eightHundred.AddForm(GrammaticalCase.Dative, "восьмистам");
            eightHundred.AddForm(GrammaticalCase.Accusative, "восемьсот");
            eightHundred.AddForm(GrammaticalCase.Instrumental, "восемьюстами", "восьмьюстами");
            eightHundred.AddForm(GrammaticalCase.Prepositional, "восьмистах");
            NumberDictionary.Add(eightHundred);

            // 900
            var nineHundred = new NumberWord("девятьсот", 900, "сотня");
            nineHundred.AddForm(GrammaticalCase.Nominative, "девятьсот");
            nineHundred.AddForm(GrammaticalCase.Genitive, "девятисот");
            nineHundred.AddForm(GrammaticalCase.Dative, "девятистам");
            nineHundred.AddForm(GrammaticalCase.Accusative, "девятьсот");
            nineHundred.AddForm(GrammaticalCase.Instrumental, "девятьюстами");
            nineHundred.AddForm(GrammaticalCase.Prepositional, "девятистах");
            NumberDictionary.Add(nineHundred);
        }

        private void AddMultipliers()
        {
            // Тысяча 
            var thousand = new NumberWord("тысяча", 1000, "множитель", "женский");
            // Единственное число
            thousand.AddForm(GrammaticalCase.Nominative, "тысяча");
            thousand.AddForm(GrammaticalCase.Genitive, "тысячи");
            thousand.AddForm(GrammaticalCase.Dative, "тысяче");
            thousand.AddForm(GrammaticalCase.Accusative, "тысячу");
            thousand.AddForm(GrammaticalCase.Instrumental, "тысячей");
            thousand.AddForm(GrammaticalCase.Prepositional, "тысяче");
            // Множественное число (2-4 тысячи)
            thousand.AddForm(GrammaticalCase.Nominative, "тысячи");
            thousand.AddForm(GrammaticalCase.Genitive, "тысяч");
            thousand.AddForm(GrammaticalCase.Dative, "тысячам");
            thousand.AddForm(GrammaticalCase.Accusative, "тысячи");
            thousand.AddForm(GrammaticalCase.Instrumental, "тысячами");
            thousand.AddForm(GrammaticalCase.Prepositional, "тысячах");
            // Множественное число (5+ тысяч)
            thousand.AddForm(GrammaticalCase.Nominative, "тысяч");
            thousand.AddForm(GrammaticalCase.Genitive, "тысяч");
            thousand.AddForm(GrammaticalCase.Dative, "тысячам");
            thousand.AddForm(GrammaticalCase.Accusative, "тысяч");
            thousand.AddForm(GrammaticalCase.Instrumental, "тысячами");
            thousand.AddForm(GrammaticalCase.Prepositional, "тысячах");
            NumberDictionary.Add(thousand);

            // Миллион 
            var million = new NumberWord("миллион", 1000000, "множитель", "мужской");
            // Единственное число
            million.AddForm(GrammaticalCase.Nominative, "миллион");
            million.AddForm(GrammaticalCase.Genitive, "миллиона");
            million.AddForm(GrammaticalCase.Dative, "миллиону");
            million.AddForm(GrammaticalCase.Accusative, "миллион");
            million.AddForm(GrammaticalCase.Instrumental, "миллионом");
            million.AddForm(GrammaticalCase.Prepositional, "миллионе");
            // Множественное число
            million.AddForm(GrammaticalCase.Nominative, "миллионы", "миллионов");
            million.AddForm(GrammaticalCase.Genitive, "миллионов");
            million.AddForm(GrammaticalCase.Dative, "миллионам");
            million.AddForm(GrammaticalCase.Accusative, "миллионы", "миллионов");
            million.AddForm(GrammaticalCase.Instrumental, "миллионами");
            million.AddForm(GrammaticalCase.Prepositional, "миллионах");
            NumberDictionary.Add(million);

            // миллиард  
            var billion = new NumberWord("миллиард", 1000000000, "множитель", "мужской");
            billion.AddForm(GrammaticalCase.Nominative, "миллиард");
            billion.AddForm(GrammaticalCase.Genitive, "миллиарда");
            billion.AddForm(GrammaticalCase.Dative, "миллиарду");
            billion.AddForm(GrammaticalCase.Accusative, "миллиард");
            billion.AddForm(GrammaticalCase.Instrumental, "миллиардом");
            billion.AddForm(GrammaticalCase.Prepositional, "миллиарде");
            // Множественное число
            billion.AddForm(GrammaticalCase.Nominative, "миллиарды", "миллиардов");
            billion.AddForm(GrammaticalCase.Genitive, "миллиардов");
            billion.AddForm(GrammaticalCase.Dative, "миллиардам");
            billion.AddForm(GrammaticalCase.Accusative, "миллиарды", "миллиардов");
            billion.AddForm(GrammaticalCase.Instrumental, "миллиардами");
            billion.AddForm(GrammaticalCase.Prepositional, "миллиардах");
            NumberDictionary.Add(billion);




        }

        public NumberWord GetNumberWord(string word)
        {
            // нижний регистр
            string lowerWord = word.ToLower();

            // Ищем в словаре
            foreach (var numberWord in NumberDictionary)
            {
                if (numberWord.IsFormOf(lowerWord))
                {
                    return numberWord;
                }
            }

            // Не нашли
            return null;
        }


        // числовое значение слова

        public long? GetWordValue(string word)
        {
            // Находим NumberWord
            var numberWord = GetNumberWord(word);

            // Если нашли - возвращаем значение, иначе null
            // ?. - оператор условного null, если numberWord = null, то вернет null
            return numberWord?.Value;
        }


        // является ли слово числительным

        public bool IsNumberWord(string word)
        {
            return GetNumberWord(word) != null;
        }


        //  тип слова

        public string GetWordType(string word)
        {
            var numberWord = GetNumberWord(word);
            return numberWord?.Type;
        }


        //  род слова

        public string GetWordGender(string word)
        {
            var numberWord = GetNumberWord(word);
            return numberWord?.Gender;
        }


        // формы слова для указанного падежа

        public List<string> GetWordForms(string word, GrammaticalCase grammaticalCase)
        {
            var numberWord = GetNumberWord(word);
            if (numberWord != null && numberWord.AllForms.ContainsKey(grammaticalCase))
            {
                return numberWord.AllForms[grammaticalCase];
            }
            return new List<string>();
        }
    }
}

