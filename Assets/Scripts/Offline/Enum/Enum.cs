using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuki
{
    public enum Item
    {
        DIAMOND_UP = 200,
        ROCK_UP = 210,
        POWER_UP = 250,
        TNT = 50,
        LUCKY_UP = 500
    }

    public enum RandomBagItem
    {
        TNT,
        STRENGTH_UP,
        GOLD
    }

    public enum Order
    {
        ENTIRE,
        PART_ONE,
        PART_TWO,
        PART_THREE,
        PART_ONE_TWO,
        PART_TWO_THREE
    }
}
