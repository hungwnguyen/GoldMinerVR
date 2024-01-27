using System;
using UnityEngine;

namespace yuki
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private float _screenRatio = 0.25f;
        [SerializeField] private float _partRatio = 1 / 3;
        [SerializeField] private Vector2 _offset = new Vector2(0.6f, 0.25f);
        [SerializeField, Tooltip("Space between player and game play screen")] private float _spaceBetweet;

        private Vector2 _previousScreenSize;
        private float _height; public float Height { get => _height; set => _height = value; }
        private float _width; public float Width { get => _width; set => _width = value; }
        public Rect PlayerRect { get; private set; }
        public Rect GameplayRect { get; private set; }
        // Ở dưới cùng
        public Rect PartOneRect { get; private set; }
        // Ở giữa
        public Rect PartTwoRect { get; private set; }
        // Ở trên cùng
        public Rect PartThreeRect { get; private set; }
        public static Screen Instance;

        void Awake()
        {
            Instance = this;
            CalculateScreenPart();
        }

        private void Start()
        {
            _previousScreenSize = new Vector2(UnityEngine.Screen.width, UnityEngine.Screen.height);
        }
 
        private void Update()
        {
            ScreenSizeChanged(CalculateScreenPart);
        }

        public void ScreenSizeChanged(Action action)
        {
            if ((UnityEngine.Screen.width != _previousScreenSize.x) || (UnityEngine.Screen.height != _previousScreenSize.y)) 
            {
                _previousScreenSize.x = UnityEngine.Screen.width;
                _previousScreenSize.y = UnityEngine.Screen.height;
                action();
            }
        }

        public void CalculateScreenPart()
        {
            _height = Camera.main.orthographicSize * 2;
            _width = _height * Camera.main.aspect;

            float playerHeight = _height * _screenRatio;
            float gameplayHeight = _height * (1 - _screenRatio);

            float gameplayY = -_height / 2f;
            float playerY = gameplayY + gameplayHeight;

            PlayerRect = new Rect(-_width / 2f, playerY, _width, playerHeight );
            GameplayRect = new Rect(-_width / 2f , gameplayY, _width, gameplayHeight);

            float partHeight = _partRatio * (GameplayRect.height - 2 * _offset.y - _spaceBetweet);

            float part1Y = GameplayRect.yMin + _offset.y;
            float part2Y = part1Y + partHeight;
            float part3Y = part2Y + partHeight;

            PartOneRect = new Rect(GameplayRect.xMin + _offset.x, part1Y, GameplayRect.width - 2 * _offset.x, partHeight);
            PartTwoRect = new Rect(GameplayRect.xMin + _offset.x, part2Y, GameplayRect.width - 2 * _offset.x, partHeight);
            PartThreeRect = new Rect(GameplayRect.xMin + _offset.x, part3Y, GameplayRect.width - 2 * _offset.x, partHeight);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            DrawGizmoRect(PlayerRect);

            Gizmos.color = Color.blue;
            DrawGizmoRect(GameplayRect);

            Gizmos.color = Color.yellow;
            DrawGizmoRect(PartOneRect);

            Gizmos.color = Color.red;
            DrawGizmoRect(PartTwoRect);

            Gizmos.color = Color.cyan;
            DrawGizmoRect(PartThreeRect);
        }

        void DrawGizmoRect(Rect rect)
        {
            Vector3 center = rect.center;
            Vector3 size = new Vector3(rect.width, rect.height, 0f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
