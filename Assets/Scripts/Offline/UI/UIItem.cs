using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private Button _buyItemButton;
        [SerializeField] private TMP_Text _itemPrice;
        [SerializeField] private TMP_Text _effect;
        private float _priceItem;

        void Start()
        {
            _priceItem = CalculateItemPrice();
            _itemPrice.SetText("$" +  _priceItem);
            switch (_item)
            {
                case Item.DIAMOND_UP:
                    _effect.SetText("Diamond value x 2");
                    break;
                case Item.ROCK_UP:
                    _effect.SetText("Rock value x 5");
                    break;
                case Item.POWER_UP:
                    _effect.SetText("Strength up");
                    break;
                case Item.TNT:
                    _effect.SetText("Add a TNT to bag");
                    break;
                case Item.LUCKY_UP:
                    _effect.SetText("Next level has more diamond");
                    break;
            }
            _buyItemButton.onClick.AddListener(BuyItem);
        }

        private void BuyItem()
        {
            if(Player.Instance.Score >= _priceItem)
            {
                Player.Instance.Bag.Add(_item);
                Player.Instance.Score -= _priceItem;
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Not enough $$$");
            }
        }

        private int CalculateItemPrice()
        {
            float _price = (int)_item;
            if(GameManager.Instance.Level == 1)
            {
                return (int) _price;
            }
            _price += GameManager.Instance.Level * (float) new System.Random().NextDouble() * _price;
            return Mathf.RoundToInt(_price);
        }

    }
}
