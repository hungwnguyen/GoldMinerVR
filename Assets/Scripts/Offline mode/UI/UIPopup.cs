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
    private Database firebase;
    int hight;
    public static UIPopup Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        firebase = GameObject.FindWithTag("firebase").GetComponent<Database>();
        popupTarget.OnAnimationFinished += Initializtion;
        popupCongat.OnAnimationFinished += ShowShop;
        animatorPopUp = popupTarget.GetComponent<Animator>();
        animatorPopUpShop = popupCongat.GetComponent<Animator>();
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_muctieu);
    }

    public void PlayPopUpCongat()
    {
        animatorPopUpShop.SetTrigger("newGame");
    }
    private void ShowShop()
    {
        SoundManager.CreatePlayBgMusic(SoundManager.Instance.audioClip.aud_shop);
        UIShop.Instance.SetStatus(true);
    }

    public void SetTargetSocre(string value)
    {
        targetScore.text = value;
    }

    public void SetCurrentSocre()
    {
        currentScore.text = ((int)Player.Instance.Score) + "";
    }

    public void SetHightSocre()
    {
        hight = PlayerPrefs.GetInt("hight score", 0);
        hightScore.text = hight + "";
    }

    public void ReSetAmin()
    {
        UIMain.Instance.onSetUI();
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_muctieu);
        animatorPopUp.SetTrigger("newGame");
    }

    private void Initializtion()
    {
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

        UpdateData();
    }

    private void UpdateData()
    {
        hight = PlayerPrefs.GetInt("hight score", 0);
        if (hight < Player.Instance.Score)
        {
            hight = (int)Player.Instance.Score;
            PlayerPrefs.SetInt("hight score", hight);
            firebase.UpdateProperties("maxScore", hight);
        }
        int maxLevel = PlayerPrefs.GetInt("maxLevel", 0);
        if (maxLevel < GameManager.Instance.Level - 1)
        {
            maxLevel = GameManager.Instance.Level - 1;
            PlayerPrefs.SetInt("maxLevel", maxLevel);
            firebase.UpdateProperties("maxLevel", maxLevel);
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SoundManager.DisableAllMusic();
        GameManager.Instance.CheckIfCountdownEnd();
        GameManager.Instance.StopCountdown();
        UpdateData();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        SoundManager.PauseAllMusic();
        UpdateData();
    }

    public void ContinueGame()
    {
        SoundManager.ContinuePlayAllMusic();
        Time.timeScale = 1;
    }

    public void LostAppear()
    {
        EventEndGame();
    }

    public void TargetAppear()
    {
    }

    public void MessengerAppear()
    {
    }

}
