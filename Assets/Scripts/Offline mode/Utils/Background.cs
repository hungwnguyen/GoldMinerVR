using System.Collections.Generic;
using UnityEngine;

namespace yuki
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetBG();
        }

        public void SetBG(){
            int rand = UnityEngine.Random.Range(0, _sprites.Count);
            spriteRenderer.sprite = _sprites[rand];
        }
        
    }
}
