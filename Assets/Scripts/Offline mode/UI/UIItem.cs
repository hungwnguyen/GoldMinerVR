using TMPro;
using UnityEngine;

namespace yuki
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private TMP_Text _itemPrice;
        
        private float _priceItem;
       
        void OnEnable()
        {
            _priceItem = CalculateItemPrice();
            _itemPrice.SetText("$" +  _priceItem);
        }

        public void BuyItem()
        {
            if(Player.Instance.Score >= _priceItem)
            {
                Player.Instance.Bag.Add(_itemData.type);
                Player.Instance.Score -= _priceItem;
                this.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Not enough $$$");
            }
        }

        public void OnPointerEnter(){
            UIShop.Instance.SetDescription(_itemData.description);
        }

        private int CalculateItemPrice()
        {
            int _price = Random.Range(_itemData.minValue, _itemData.maxValue) * Random.Range(1,  GameManager.Instance.Level);
            return _price;
        }

    }
}
