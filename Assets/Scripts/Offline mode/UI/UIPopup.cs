using UnityEngine;
using UnityEngine.EventSystems;
using yuki;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lost;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject messenger;
    public UIPopup Instance {get; private set;}

    void Awake()
    {
        if (Instance != null){
            Destroy(this);
        } else {
            this.Instance = this;
        }
    }

    private void PlayContinue(){
        pause.SetActive(false);
    }

    public void PauseGame(){
        Time.timeScale = 0;
        pause.SetActive(true);
        SoundManager.PauseAllMusic();
    }

    public void ContinueGame(){
        Time.timeScale = 1;
        SoundManager.ContinuePlayAllMusic();
        Invoke("PlayContinue", 0.2f);
    }

    public void WinAppear(){
        win.SetActive(true);
    }

    public void LostAppear(){
        lost.SetActive(true);
    }

    public void TargetAppear(){
        target.SetActive(true);
    }

    public void MessengerAppear(){
        messenger.SetActive(true);
    }

}
