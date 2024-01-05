using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuki
{
    public enum RandomBagItem
    {
        TNT_1,
        TNT_2,
        TNT_3,
        STRENGTH_UP,
        NONE
    }

    public class RandomBag : Rod
    {

        public RandomBagItem RandomItem()
        {
            RandomBagItem[] items = (RandomBagItem[]) Enum.GetValues(typeof(RandomBagItem));
            int rand = new Random().Next(items.Length);
            return items[rand];
        }

        public Dictionary<RandomBagItem, String> CreateDictStr()
        {
            Dictionary<RandomBagItem, String> dict = new Dictionary<RandomBagItem, String>();
            dict.Add(RandomBagItem.TNT_1 , "+1 TNT");
            dict.Add(RandomBagItem.TNT_2, "+2 TNT");
            dict.Add(RandomBagItem.TNT_3, "+3 TNT");
            dict.Add(RandomBagItem.STRENGTH_UP, "Strength Up!!!");
            dict.Add(RandomBagItem.NONE, "Unlucky -.-");
            return dict;
        }
    }
}
