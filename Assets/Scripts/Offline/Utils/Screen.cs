using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private float _screenRatio = 0.25f;
        [SerializeField] private float _partRatio = 1 / 3;
        [SerializeField] private Vector2 _offset = new Vector2(0.6f, 0.25f);
        [SerializeField, Tooltip("Space between player and game play screen")] private float _spaceBetweet;
        private float _height; public float Height { get => _height; set => _height = value; }
        private float _width; public float Width { get => _width; set => _width = value; }
        public Rect PlayerRect { get; private set; }
        public Rect GameplayRect { get; private set; }
        public Rect PartOneRect { get; private set; }
        public Rect PartTwoRect { get; private set; }
        public Rect PartThreeRect { get; private set; }
        public static Screen Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            CalculateScreenPart();
        }

        private void Start()
        {
            
        }
 
        private void Update()
        {
            CalculateScreenPart();
        }

        public void CalculateScreenPart()
        {
            _height = Camera.main.orthographicSize * 2;
            _width = _height * Camera.main.aspect;

            float playerHeight = _height * _screenRatio;
            float gameplayHeight = _height * (1 - _screenRatio);

            float gameplayY = -_height / 2f;
            float playerY = gameplayY + gameplayHeight;

            PlayerRect = new Rect(-_width / 2f + _offset.x, playerY + _offset.y, _width - 2 * _offset.x, playerHeight - 2 * _offset.y);
            GameplayRect = new Rect(-_width / 2f + _offset.x, gameplayY + _offset.y, _width - 2 * _offset.x, gameplayHeight - 2 * _offset.y - _spaceBetweet);

            float partHeight = _partRatio * GameplayRect.height;

            float part1Y = GameplayRect.yMin;
            float part2Y = part1Y + partHeight;
            float part3Y = part2Y + partHeight;

            PartOneRect = new Rect(GameplayRect.xMin, part1Y, GameplayRect.width, partHeight);
            PartTwoRect = new Rect(GameplayRect.xMin, part2Y, GameplayRect.width, partHeight);
            PartThreeRect = new Rect(GameplayRect.xMin, part3Y, GameplayRect.width, partHeight);
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
