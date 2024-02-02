using System;

namespace yuki
{
    public class RandomBag : Rod
    {
        protected override void Start()
        {
            base.Start();
        }

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
                    Player.Instance.TNTCount++;
                    TextContainer.Instance.ShowPopupText("TNT");
                    return RandomBagItem.TNT.ToString();
                case RandomBagItem.STRENGTH_UP:
                    Player.Instance.PowerBuff = 10;
                    SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_strength);
                    return RandomBagItem.STRENGTH_UP.ToString();
                case RandomBagItem.GOLD:
                    int randGoldReceive;
                    if (Player.Instance.isLucky){
                        randGoldReceive = UnityEngine.Random.Range(600, 1000);
                    } else {
                        randGoldReceive = UnityEngine.Random.Range(100, 300);
                    }
                    Player.Instance.Score += randGoldReceive * GameManager.Instance.Level;
                    TextContainer.Instance.ShowFloatingText(randGoldReceive + "$");
                    return RandomBagItem.GOLD.ToString();
            }
            return "";
        }
    }   
}
