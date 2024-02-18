using TMPro;
using UnityEngine;

namespace yuki
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private TMP_Text _itemPrice;
        
        private float _priceItem;

        public static bool isBuy;
       
        void OnEnable()
        {
            isBuy = false;
            _priceItem = CalculateItemPrice();
            _itemPrice.SetText("$" +  _priceItem);
        }

        public void BuyItem()
        {
            if(Player.Instance.Score >= _priceItem)
            {
                isBuy = true;
                switch (_itemData.type)
                {
                    case Item.DIAMOND_UP:
                        Player.Instance.DiamondBuff = 2;
                        break;
                    case Item.ROCK_UP:
                        Player.Instance.RockBuff = 5;
                        break;
                    case Item.POWER_UP:
                        Player.Instance.PowerBuff = 10;
                        break;
                    case Item.TNT:
                        Player.Instance.TNTCount++;
                        break;
                    case Item.LUCKY_UP:
                        Player.Instance.isLucky = true;
                        break;

                }
                
                Player.Instance.Score -= _priceItem;
                this.gameObject.SetActive(false);
            }
            else
            {
                UIShop.Instance.SetMessenger("Not enough $$$");
            }
        }

        public void OnPointerEnter(){
            UIShop.Instance.SetDescription(_itemData.description);
        }

        private int CalculateItemPrice()
        {
            int _price = Random.Range(_itemData.minValue, _itemData.maxValue) + Random.Range(0,  20) * GameManager.Instance.Level % 200;
            return _price;
        }

    }
}
