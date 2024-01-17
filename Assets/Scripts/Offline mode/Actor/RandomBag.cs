using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuki
{
    public class RandomBag : Rod
    {
        private RandomBagItem RandomItem()
        {
            RandomBagItem[] items = (RandomBagItem[]) Enum.GetValues(typeof(RandomBagItem));
            int rand = new Random().Next(items.Length);
            return items[rand];
        }

        public string GetEffectRandomBag()
        {
            RandomBagItem item = RandomItem();
            switch (item)
            {
                case RandomBagItem.TNT:
                    Player.Instance.Bag.Add(Item.TNT);
                    TextContainer.Instance.ShowPopupText("TNT");
                    return RandomBagItem.TNT.ToString();
                case RandomBagItem.STRENGTH_UP:
                    Player.Instance.PowerBuff = 10;
                    TextContainer.Instance.ShowPopupText("STRENGTH");
                    return RandomBagItem.STRENGTH_UP.ToString();
                case RandomBagItem.GOLD:
                    int randGoldReceive = UnityEngine.Random.Range(100, 300);
                    Player.Instance.Score += randGoldReceive * GameManager.Instance.Level;
                    TextContainer.Instance.ShowPopupText(randGoldReceive + "$");
                    return RandomBagItem.GOLD.ToString();
            }
            return "";
        }


    }
}
