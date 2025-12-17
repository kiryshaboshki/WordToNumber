using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordToNumber.Enums
{
    // если вдруг падежи будут разные
    public enum GrammaticalCase
    {
        Nominative, // именительный один два

        Genitive, // родительный одного двух

        Dative, // дательный одному двум

        Accusative, // винительный один два(кого что)

        Instrumental, // творительный одним двумя

        Prepositional // предложный об одном о двух
    }
}
