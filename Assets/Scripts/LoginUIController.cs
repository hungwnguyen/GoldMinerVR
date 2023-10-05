using System.Collections;
using UnityEngine;
using TMPro;
using DatabaseAPI.Account;
using UnityEngine.Serialization;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class LoginUIController : MonoBehaviour
{

    #region variable
    [Serializable]
    public class MyEvent : UnityEvent { }
    [Space(5f), Header("Run when Awake"), Space(5f)]
    [FormerlySerializedAs("CustomEvent")]
    [SerializeField] private MyEvent _customEvent = new MyEvent();
    public MyEvent CustomEvent
    {
        get => _customEvent;
        set { _customEvent = value; }
    }

    [Space(5f), Header("StartLoad"), Space(5f)]
    [FormerlySerializedAs("CustomEvent1")]
    [SerializeField] private MyEvent _customEvent1 = new MyEvent();
    public MyEvent CustomEvent1
    {
        get => _customEvent1;
        set { _customEvent1 = value; }
    }

    [Space(5f), Header("EndLoad"), Space(5f)]
    [FormerlySerializedAs("CustomEvent2")]
    [SerializeField] private MyEvent _customEvent2 = new MyEvent();
    public MyEvent CustomEvent2
    {
        get => _customEvent2;
        set { _customEvent2 = value; }
    }
    [Space(5f), Header("Error Password or Email"), Space(5f)]
    [FormerlySerializedAs("CustomEvent3")]
    [SerializeField] private MyEvent _customEvent3 = new MyEvent();
    public MyEvent CustomEvent3
    {
        get => _customEvent3;
        set { _customEvent3 = value; }
    }
    [Space(5f), Header("Delay on load"), Space(5f)]
    [FormerlySerializedAs("CustomEvent4")]
    [SerializeField] private MyEvent _customEvent4 = new MyEvent();
    public MyEvent CustomEvent4
    {
        get => _customEvent4;
        set { _customEvent4 = value; }
    }

    [SerializeField] private TextMeshProUGUI Error, ErrorHelper, notificationManager, Error2, ErrorHelper2;
    [SerializeField] private TMP_InputField username, password1, password2, email1, email2;

    private static LoginUIController _instance;
    public static LoginUIController Instance { get => _instance; }

    private int bg, fx;
    #endregion

    #region MonoBehaviourCallBack
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        EventActive();
        username.onEndEdit.AddListener(GetUserName);
        email2.onEndEdit.AddListener(GetUserEmail);
        email1.onEndEdit.AddListener(GetUserEmail);
        password1.onEndEdit.AddListener(GetUserPassword);
        password2.onEndEdit.AddListener(GetUserPassword);
        if (_instance == null)
        {
            _instance = this;
        }
    }
    #endregion

    #region UnityEvent


    IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("LoadData");
        EventActive1();
        while (!asyncOperation.isDone)
        {
            // Update progress bar or display loading progress here
            yield return new WaitForEndOfFrame();
        }
        EventActive2();
    }

    void EventActive()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent", this);
        _customEvent.Invoke();
    }

    void EventActive1()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent1", this);
        _customEvent1.Invoke();
    }

    void EventActive2()
    {

        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent2", this);
        _customEvent2.Invoke();
    }

    public void EventActive3(string error)
    {
        ErrorHelper.text = Error.text;
        Error.text = Error.IsActive() ? error : "";
        ErrorHelper2.text = Error2.text;
        Error2.text = Error2.IsActive() ? error : "";
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent3", this);
        try
        {
            _customEvent3.Invoke();
        }catch { }
        
    }

    public void EventActive4()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent4", this);
        _customEvent4.Invoke();
    }
    #endregion

    #region UIManagerCallback

    public void UISound()
    {
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_touch);
    }

    public IEnumerator UpdateAllStats()
    {
        EventActive1();
        while (!AccountController.controller.getStatsFinished || !ExampleManager.IsReady)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2.88888888f);
        while (AccountController.usernameDisplay == null || AccountController.userName == null)
        {
            yield return new WaitForEndOfFrame();
        }
        notificationManager.text = "Welcome " + AccountController.usernameDisplay + " !";
        EventActive2();
        yield return new WaitForSeconds(1.23f);
        EventActive4();
        
        // Get the statictic in the player data dictionnary
        //AccountController.playerData.TryGetValue("HightScore", out hightScore);
        //AccountController.playerData.TryGetValue("Level", out level);
        if (AccountController.playerData.TryGetValue("BGMusic", out bg))
        {
            SoundManager.Instance.bg = bg * 0.01f;
        }
        else
        {
            SoundManager.Instance.bg = 1;
        }
        if (AccountController.playerData.TryGetValue("FXSound", out fx))
        {
            SoundManager.Instance.fx = fx * 0.01f;
        }
        else
        {
            SoundManager.Instance.fx = 1;
        }
        SoundManager.CreatePlayBGMusic(SoundManager.Instance.audioClip.BGMusic);
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.welcom);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Lobby");
    }

    #region Button Section

    public void GetUserEmail(string value)
    {
        AccountController.controller.GET_USER_EMAIL(value);
    }

    public void GetUserName(string value)
    {
        AccountController.controller.GET_USER_USERNAME(value);
    }
    public void GetUserPassword(string value)
    {
        AccountController.controller.GET_USER_PASSWORD(value);
    }

    public void SignIn()
    {
        AccountController.controller.GET_USER_EMAIL(email1.text);
        AccountController.controller.GET_USER_PASSWORD(password1.text);
        AccountController.controller.LOGIN_ACTION();
    }

    public void SignUp()
    {
        AccountController.controller.ON_CLIC_CREATE_ACCOUNT();
    }
    
#if UNITY_ANDROID || UNITY_IOS
    #region For Anonymous connection /!\ ONLY FOR MOBILE DEVICES /!\
    public void OpenCloseRecoverySection()
    {
        AccountController.controller.OPEN_RECOVERY_PANEL();
    }
    public void CreateRecoveryAccount()
    {
        AccountController.controller.ON_CLIC_ADD_RECOVERY();
    }
    #endregion
#endif
    #endregion
    #endregion

}
