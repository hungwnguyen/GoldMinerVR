using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus;
using DatabaseAPI.Account;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LobbyController : MonoBehaviour
{
    #region Variable
    [Serializable]
    public class MyEvent : UnityEvent { }
    [Space(2f), Header("Run when awake"), Space(2f)]
    [FormerlySerializedAs("CustomEvent0")]
    [SerializeField] private MyEvent _customEvent0 = new MyEvent();
    public MyEvent CustomEvent0
    {
        get => _customEvent0; 
        set { _customEvent0 = value; } 
    }

    [Space(2f), Header("Start load"), Space(2f), Tooltip("Start load")]
    [FormerlySerializedAs("CustomEvent")]
    [SerializeField] private MyEvent _customEvent = new MyEvent();
    public MyEvent CustomEvent
    {
        get => _customEvent;
        set { _customEvent = value; }
    }

    [Space(2f), Header("End load!"), Space(2f), Tooltip("End load")]
    [FormerlySerializedAs("CustomEvent2")]
    [SerializeField] private MyEvent _customEvent2 = new MyEvent();
    
    public MyEvent CustomEvent2
    {
        get => _customEvent2;
        set { _customEvent2 = value; }
    }

    public int minRequiredPlayers = 1;
    public int numberOfTargetRows = 5;

    //Variables to initialize the room controller
    public string roomName = "GoldMiner";

    [SerializeField]
    private RoomSelectionMenu selectRoomMenu = null;

    [SerializeField]
    private TMP_InputField roomID;

    [SerializeField] 
    private TextMeshProUGUI usernameSetting;

    public static bool NeedUnload = false;
    #endregion

    private IEnumerator Start()
    {
        while (!ExampleManager.IsReady || !this.gameObject.activeSelf)
        {
            yield return new WaitForEndOfFrame();
        }
        Dictionary<string, object> roomOptions = new Dictionary<string, object>
        {
            ["logic"] = "GoldMiner", //The name of our custom logic file
            ["minReqPlayers"] = minRequiredPlayers.ToString(),
            ["numberOfTargetRows"] = numberOfTargetRows.ToString()
        };
        ExampleManager.Instance.Initialize(roomName, roomOptions);
        ExampleManager.onRoomsReceived += OnRoomsReceived;
        /*while (string.IsNullOrEmpty(AccountController.usernameDisplay))
            {
                yield return new WaitForEndOfFrame();
            }
            ExampleManager.Instance.UserName = AccountController.usernameDisplay;*/
        ExampleManager.Instance.UserName = PlayerPrefs.GetString("Player name");
        CreateUser();
        usernameSetting.text = PlayerPrefs.GetString("Player name");
        yield return new WaitForSeconds(0.45f);
        EventActive0();
    }

    void Update()
    {
        if (NeedUnload)
        {
            EventActive2();
            NeedUnload = false;
        }
    }

    private void OnDestroy()
    {
        ExampleManager.onRoomsReceived -= OnRoomsReceived;
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
    
    private void EventActive2()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent2", this);
        _customEvent2.Invoke();
    }

    /// <summary>
    /// Activate only one time when start game.
    /// </summary>
    

    public void FindRoom()
    {
        if (roomID.text != null && ExampleManager.Instance.CheckRoomID(roomID.text))
        {
            try
            {
                JoinRoom(roomID.text);
            }
            catch
            {
                EventActive2();
            }
        }
        else
        {
            EventActive2();
        }
        
    }

    public void CreateUser()
    {
        //Do user creation stuff
        ExampleManager.Instance.InitializeClient();
        selectRoomMenu.GetAvailableRooms();
    }

    public void CreateRoom()
    {
        EventActive();
        string desiredRoomName = DateTime.Now.ToString("ssHHmm");
        LoadGallery(() => { ExampleManager.Instance.CreateNewRoom(desiredRoomName); });
    }

    public void JoinRoom(string id)
    {
        StartCoroutine(TryJoinRoom(id));
    }

    IEnumerator TryJoinRoom(string id)
    {
        EventActive();
        LoadGallery(() => { ExampleManager.Instance.JoinExistingRoom(id); });
        yield return new WaitForSeconds(21f);
        EventActive2();
    }
    public void OnConnectedToServer()
    {
        EventActive2();
    }

    private void OnRoomsReceived(ColyseusRoomAvailable[] rooms)
    {
        selectRoomMenu.HandRooms(rooms);
    }

    private void LoadGallery(Action onComplete)
    {
        StartCoroutine(LoadSceneAsync("Gamescene", onComplete));
    }

    private IEnumerator LoadSceneAsync(string scene, Action onComplete)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (op.progress <= 0.9f)
        {
            //Wait until the scene is loaded
            yield return new WaitForEndOfFrame();
        }
        onComplete.Invoke();
        op.allowSceneActivation = true;
        while (!ExampleRoomController.OnCreateRoom || ExampleManager.Instance.UserName == null)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.UnloadSceneAsync(2);
    }
}