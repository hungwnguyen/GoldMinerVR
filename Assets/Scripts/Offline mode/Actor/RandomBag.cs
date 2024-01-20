using System;
using UnityEngine;

namespace yuki
{
    public class RandomBag : Rod
    {
        private RandomBagItem RandomItem()
        {
            RandomBagItem[] items = (RandomBagItem[]) Enum.GetValues(typeof(RandomBagItem));
            int rand = new System.Random().Next(items.Length);
            return items[rand];
        }

        public string GetEffectRandomBag()
        {
            RandomBagItem item = RandomItem();
            switch (item)
            {
                case RandomBagItem.TNT:
                    Player.Instance.playerData.Bag.Add(Item.TNT);
                    TextContainer.Instance.ShowPopupText("TNT");
                    return RandomBagItem.TNT.ToString();
                case RandomBagItem.STRENGTH_UP:
                    Player.Instance.playerData.PowerBuff = 10;
                    
                    SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_strength);
                    return RandomBagItem.STRENGTH_UP.ToString();
                case RandomBagItem.GOLD:
                    int randGoldReceive = UnityEngine.Random.Range(100, 300);
                    Player.Instance.playerData.Score += randGoldReceive * LevelManager.Instance.Level;
                    TextContainer.Instance.ShowFloatingText(randGoldReceive + "$");
                    return RandomBagItem.GOLD.ToString();
            }
            return "";
        }
    }   
}
