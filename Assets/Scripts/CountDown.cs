using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CountDown : MonoBehaviour
{

    [Serializable]
    public class MyEvent : UnityEvent { }
    [Space(2f), Header("Open Window"), Tooltip("Call when count donw, all players is ready"), Space(2f)]
    [FormerlySerializedAs("CustomEvent0")]
    [SerializeField] private MyEvent _customEvent0 = new MyEvent();
    public MyEvent CustomEvent0
    {
        get => _customEvent0;
        set { _customEvent0 = value; }
    }

    [Space(2f), Header("Close Window"), Space(2f), Tooltip("Start load")]
    [FormerlySerializedAs("CustomEvent")]
    [SerializeField] private MyEvent _customEvent = new MyEvent();
    public MyEvent CustomEvent
    {
        get => _customEvent;
        set { _customEvent = value; }
    }

    [SerializeField] private TextMeshProUGUI countDownText;

    void Start()
    {
        countDownText.text = null;
    }

    IEnumerator CountDownWindow()
    {
        EventActive0();
        while (string.IsNullOrEmpty(GameManager.Instance.CountDownString))
        {
            yield return new WaitForEndOfFrame();
        }
        while (!string.IsNullOrEmpty(GameManager.Instance.CountDownString))
        {
            countDownText.text = GameManager.Instance.CountDownString;
            yield return new WaitForEndOfFrame();
        }
        GameManager.Instance.PlayMode = GameManager.eGameState.ENDROUND;
        EventActive();
    }

    public void OpenWindow()
    {
        StartCoroutine(CountDownWindow());
    }
    private void EventActive0()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent0", this);
        _customEvent0.Invoke();
    }

    private void EventActive()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent", this);
        _customEvent.Invoke();
    }

}
