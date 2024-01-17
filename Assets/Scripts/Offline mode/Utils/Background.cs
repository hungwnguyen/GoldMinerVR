using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
        [SerializeField] private bool _isPlayerBG;
        public SpriteRenderer SpriteRenderer { get; private set; }
        

        private Rect _currentRect;

        void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            int rand = UnityEngine.Random.Range(0, _sprites.Count);
            SpriteRenderer.sprite = _sprites[rand];
        }

        void Start ()
        {
            if (_isPlayerBG)
            {
                _currentRect = Screen.Instance.PlayerRect;
            }
            else
            {
                _currentRect = Screen.Instance.GameplayRect;
            }
            transform.position = _currentRect.center;
            FitToRect();
        }

        void Update()
        {
            Screen.Instance.ScreenSizeChanged(FitToRect);
        }

        private void FitToRect()
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            float rectWidth = _currentRect.width;
            float rectHeight = _currentRect.height;

            
            float scaleX = rectWidth / SpriteRenderer.bounds.size.x;
            float scaleY = rectHeight / SpriteRenderer.bounds.size.y;

            transform.localScale = new Vector3(scaleX, scaleY, 1f);
        }
    }
}
