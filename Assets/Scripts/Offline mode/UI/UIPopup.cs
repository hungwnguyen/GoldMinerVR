using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using yuki;

public class UIPopup : MonoBehaviour
{
    [Serializable]
    public class MyEvent : UnityEvent { }
    
    [Space(5f), Header("Run when end game"), Space(5f)]
    [FormerlySerializedAs("CustomEvent")]
    [SerializeField] private MyEvent _customEvent = new MyEvent();
    /// <summary>
    /// Run when end game
    /// </summary>
    /// <value></value>
    public MyEvent CustomEvent
    {
        get => _customEvent;
        set { _customEvent = value; }
    }

    [SerializeField] private yuki.EventHandler popupTarget, popupCongat;
    [SerializeField] private TMP_Text targetScore, hightScore, currentScore;
    private Animator animatorPopUp, animatorPopUpShop;
    public static UIPopup Instance {get; private set;}

    void Awake()
    {
        if (Instance != null){
            Destroy(this);
        } else {
            Instance = this;
        }
        
        popupTarget.OnAnimationFinished += Initializtion;
        popupCongat.OnAnimationFinished += ShowShop;
        animatorPopUp = popupTarget.GetComponent<Animator>();
        animatorPopUpShop = popupCongat.GetComponent<Animator>();
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_muctieu);
    }

    public void PlayPopUpCongat(){
        animatorPopUpShop.SetTrigger("newGame");
    }
    private void ShowShop(){
        SoundManager.CreatePlayBgMusic(SoundManager.Instance.audioClip.aud_shop);
        UIShop.Instance.SetStatus(true);
    }

    public void SetTargetSocre(string value){
        targetScore.text = value;
    }

    public void SetCurrentSocre(){
        currentScore.text = ((int) Player.Instance.Score) + "";
    }

    public void SetHightSocre(){
        int hight = PlayerPrefs.GetInt("hight score", 0);
        if (hight < Player.Instance.Score){
            hight = (int) Player.Instance.Score;
            PlayerPrefs.SetInt("hight score", hight);
        }
        hightScore.text = hight + "";
    }

    public void ReSetAmin(){
        UIMain.Instance.onSetUI();
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_muctieu);
        animatorPopUp.SetTrigger("newGame");
    }

    private void Initializtion(){
        UIMain.Instance.onSetUI();
        UIMain.Instance.EnventStartGame();
    }

    private void EventEndGame()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent", this);
        _customEvent.Invoke();
        Time.timeScale = 0;
        SoundManager.DisableAllMusic();
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_fail);
    }

    public void NextLevel(){
        Time.timeScale = 1;
        SoundManager.DisableAllMusic();
        GameManager.Instance.CheckIfCountdownEnd();
        GameManager.Instance.StopCountdown();
    }

    public void PauseGame(){
        Time.timeScale = 0;
        SoundManager.PauseAllMusic();
    }

    public void ContinueGame(){
        SoundManager.ContinuePlayAllMusic();
        Time.timeScale = 1;
    }

    public void WinAppear(){
        EventEndGame();
    }

    public void LostAppear(){
        EventEndGame();
    }

    public void TargetAppear(){
    }

    public void MessengerAppear(){
    }

}
