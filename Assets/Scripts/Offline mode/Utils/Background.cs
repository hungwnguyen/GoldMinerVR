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
        private Vector2 _previousScreenSize;

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
            _previousScreenSize = new Vector2(UnityEngine.Screen.width, UnityEngine.Screen.height);
            transform.position = _currentRect.center;
            FitToRect();
        }

        void Update()
        {
            if (IsScreenSizeChanged())
            {
                _previousScreenSize.x = UnityEngine.Screen.width;
                _previousScreenSize.y = UnityEngine.Screen.height;
                FitToRect();
            }
        }

        private bool IsScreenSizeChanged()
        {
            return (UnityEngine.Screen.width != _previousScreenSize.x) || (UnityEngine.Screen.height != _previousScreenSize.y);
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
