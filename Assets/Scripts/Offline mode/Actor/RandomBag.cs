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

        public void GetEffectRandomBag()
        {
            RandomBagItem item = RandomItem();
            switch (item)
            {
                case RandomBagItem.TNT:
                    Player.Instance.Bag.Add(Item.TNT);
                    PopupTextContainer.Instance.ShowPopupText("++TNT");
                    break;
                case RandomBagItem.STRENGTH_UP:
                    Player.Instance.PowerBuff = 2;
                    PopupTextContainer.Instance.ShowPopupText("++STRENGTH");
                    break;
                case RandomBagItem.GOLD:
                    int randGoldReceive = UnityEngine.Random.Range(100, 300);
                    Player.Instance.Score += randGoldReceive * GameManager.Instance.Level;
                    PopupTextContainer.Instance.ShowPopupText("++" + randGoldReceive + "$");
                    break;
            }
        }
    }
}
