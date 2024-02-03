using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private TMP_Text description, messenger;
        [SerializeField] GameObject _uiShop;
        [SerializeField] private Animator saler;
        public static UIShop Instance;

        void OnEnable()
        {
            AddItemToShop();
            _uiShop.GetComponent<CanvasGroup>().alpha = 1;
        }

        void OnDisable()
        {
            for (int i = 0; i < transform.childCount; i++){
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        void Awake()
        {
            Instance = this;
            SetStatus(false);
        }

        void Start()
        {
            _nextLevelButton.onClick.AddListener(NextLevel);
        }

        private void NextLevel() 
        {
            StartCoroutine(Transition());
        }

        IEnumerator Transition(){
            float time = 1;
            SoundManager.Instance.StopBgMusic(SoundManager.Instance.audioClip.aud_shop);
            if (!UIItem.isBuy){
                saler.SetTrigger("unsold");
                SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_UnBuy);
                time = SoundManager.Instance.audioClip.aud_UnBuy.length;
            } else {
                SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_touch);
                saler.SetTrigger("saled");
            }
            yield return new WaitForSeconds(time);
            GameManager.Instance.NextLevel();
        }

        private void AddItemToShop()
        {
            int rand = UnityEngine.Random.Range(0, this.transform.childCount);
            for (int i = 0; i < rand; i++)
            {
                this.transform.GetChild(Random.Range(0, this.transform.childCount)).gameObject.SetActive(false);
            }
        }

        public void SetStatus(bool status){
            this._uiShop.SetActive(status);
        }

        public void SetDescription(string value){
            description.text = value;
        }

        public void SetMessenger(string value){
            messenger.text = value;
            saler.SetTrigger("talk");
        }

    }
}
