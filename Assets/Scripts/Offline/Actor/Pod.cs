using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace yuki
{
    public class Pod : Actor
    {
        [SerializeField] private PodData podData;
        public PodRotationState RotationState { get; private set; }
        public PodRewindState RewindState { get; private set; }
        public PodShootState ShootState { get; private set; }

        private float _height; public float Height { get => _height; set => _height = value; }
        private float _width; public float Width { get => _width; set => _width = value; }
        private Vector3 _originPos; public Vector3 OriginPos { get => _originPos; set => _originPos = value; }

        #region POWER BUFF
        private float _powerBuff; public float PowerBuff { get => _powerBuff; set => _powerBuff = value; }
        private float _powerBuffTime; public float PowerBuffTime { get => _powerBuffTime; set => _powerBuffTime = value; }
        private float _currentTime;
        #endregion

        #region ITEM BUFF
        private float _diamondBuff; public float DiamondBuff { get => _diamondBuff; set => _diamondBuff = value; }
        private float _rockBuff; public float RockBuff { get => _rockBuff; set => _rockBuff = value; }
        #endregion

        public Drag Drag { get; private set; }
        [SerializeField] private GameObject _popupText; public GameObject PopupText { get => _popupText; set => _popupText = value; }

        public static Pod Instance;

        protected override void Awake()
        {
            base.Awake();

            Instance = this;

            _height = Camera.main.orthographicSize * 2;
            _width = _height * Camera.main.aspect;

            RotationState = new PodRotationState(this, podData, "rotate");
            RewindState = new PodRewindState(this, podData, "rewind");
            ShootState = new PodShootState(this, podData, "shoot");
        }

        protected override void Start()
        {
            base.Start();

            Drag = GetComponentInChildren<Drag>();
            _originPos = new Vector3(0, Height / 2, 0);
            _powerBuff = 1;
            _currentTime = 0;
            _diamondBuff = 1;
            _rockBuff = 1;
            FSM.Initialization(RotationState);
        }

        public bool CheckIfOutOfScreen()
        {
            return Vector3.Distance(transform.position, _originPos) > (Mathf.Sqrt(Mathf.Pow(_width/2 - 0.5f, 2) + Mathf.Pow(_height - 0.5f, 2)) - 2);
        }

        public void StartPowerBuff()
        {
            _currentTime += _powerBuffTime;
            UIMain.Instance.SetPowerBuffText(_currentTime);
            UIMain.Instance.SetPowerBuffEnable(true);
            StartCoroutine(Countdown());
        }

        IEnumerator Countdown()
        {
            while (_currentTime > 0)
            {
                _currentTime -= 1;

                UIMain.Instance.SetPowerBuffText(_currentTime);

                yield return new WaitForSeconds(1.0f);
            }
            CheckIfCountdownEnd();
        }

        private void CheckIfCountdownEnd() {
            if(_currentTime == 0)
            {
                _powerBuff = 1;
                _powerBuffTime = 0;
                UIMain.Instance.SetPowerBuffEnable(false);
            }
        }

        public void ShowPopupText(string content)
        {
            if(_popupText != null)
            {
                var cointainer = GameObject.FindGameObjectWithTag("PopupTextCointainer");
                if(cointainer != null )
                {
                    var go = Instantiate(_popupText, cointainer.transform.position, Quaternion.identity, cointainer.transform);
                    go.GetComponent<TMP_Text>().SetText(content);
                }
                else
                {
                    Debug.Log("Missing popuptext cointainer");
                }
            }
            else
            {
                Debug.Log("Missing popuptext prefab");
            }
        }

        public void SetStatus(bool status)
        {
            this.gameObject.SetActive(status);
        }
    }
}

