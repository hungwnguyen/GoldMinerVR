using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class Board : MonoBehaviour
    {
        public Canvas canvas; 
        public RectTransform uiElement;
        [SerializeField] private bool _boardOne;
        [SerializeField] private Vector2 _offsetBoard;

        void Start()
        {
            ChangePosition();
        }

        void Update()
        {
            Screen.Instance.ScreenSizeChanged(ChangePosition);
        }

        private void ChangePosition()
        {
            Vector2 screenPos;
            if (_boardOne)
            {
                screenPos = Camera.main.WorldToScreenPoint(new Vector3(Screen.Instance.PlayerRect.xMin + _offsetBoard.x, Screen.Instance.PlayerRect.yMax - _offsetBoard.y));
            }
            else
            {
                screenPos = Camera.main.WorldToScreenPoint(new Vector3(Screen.Instance.PlayerRect.xMax - _offsetBoard.x, Screen.Instance.PlayerRect.yMin + _offsetBoard.y));
            }
            
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, Camera.main, out localPoint);

            uiElement.localPosition = localPoint;
        }

    }
}
