using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public enum Item
    {
        DIAMOND_UP,
        ROCK_UP,
        POWER_UP,
        TNT,
        LUCKY_UP,
        NONE
    }
    public class PlayerManager : MonoBehaviour
    {
        private float _score; public float Score { get => _score; set => _score = value; }
        private List<Item> _items; public List<Item> Items { get => _items; set => _items = value; }

        public static PlayerManager Instance;

        void Awake()
        {
            Instance = this;
            _score = 0;
            _items = new List<Item>();
        }

        void Start()
        {
            _items.Add(Item.TNT);
            _items.Add(Item.TNT);
            _items.Add(Item.TNT);
            DontDestroyOnLoad(this);
        }

        public float GetTNTNumber()
        {
            int count = 0;
            foreach (Item item in _items)
            {
                if(item == Item.TNT) count++;
            }
            return count;
        }

        public bool CheckIfHaveItem(Item item)
        {
            return _items.Contains(item);
        }

        public void UseItem(Item item)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == item)
                {
                    _items.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
