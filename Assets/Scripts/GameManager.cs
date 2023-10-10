using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Colyseus;
using Colyseus.Schema;
using LucidSightTools;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using static ExampleRoomController;

public class GameManager : MonoBehaviour
{
    #region Variable
    public PlayerManager prefab;

    private PlayerManager newView;

    [SerializeField]
    private TextMeshProUGUI players;

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameUI uiController;

    private bool _showCountdown;
    public bool ShowCountdown { get => _showCountdown; }

    private string _countDownString = "";
    /// <summary>
    ///     Return Count down string value when all players is ready
    /// </summary>
    public string CountDownString { get => _countDownString; }

    private int readyNumbers;

    public int siblingCurrent { get; private set; }

    public enum eGameState
    {
        NONE,
        WAITING,
        WAITINGFOROTHERS,
        SENDTARGETS,
        BEGINROUND,
        SIMULATEROUND,
        ENDROUND,
        START
    }

    private eGameState currentGameState;
    private eGameState lastGameState;
    public eGameState PlayMode;

    [SerializeField] MapController mapController;
    #endregion

    #region Monobehavior Call Back
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Application.targetFrameRate = 60;
        siblingCurrent = -1;
        PlayMode = eGameState.NONE;
        Instance = this;
    }

    private IEnumerator Start()
    {
        while (ExampleManager.Instance.IsInRoom == false)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        if (players.gameObject.activeSelf && PlayMode != eGameState.ENDROUND)
        {
            if (PlayMode.Equals(eGameState.START))
            {
                PlayerReadyToPlay();
            }
            else
            {
                UpdateUserName();
            }
        }
    }

    private void OnEnable()
    {
        ExampleRoomController.onAddNetworkEntity += OnNetworkAdd;
        ExampleRoomController.onRemoveNetworkEntity += OnNetworkRemove;

        ExampleRoomController.onGotTargetLineUp += GotNewTargetLineUp;
        ExampleRoomController.onRoomStateChanged += OnRoomStateChanged;
        ExampleRoomController.onBeginRoundCountDown += OnBeginRoundCountDown;
        ExampleRoomController.onBeginRound += OnBeginRound;
        ExampleRoomController.onRoundEnd += OnRoundEnd;
    }


    private void OnDisable()
    {
        ExampleRoomController.onAddNetworkEntity -= OnNetworkAdd;
        ExampleRoomController.onRemoveNetworkEntity -= OnNetworkRemove;
        ExampleRoomController.onGotTargetLineUp -= GotNewTargetLineUp;

        ExampleRoomController.onRoomStateChanged -= OnRoomStateChanged;
        ExampleRoomController.onBeginRoundCountDown -= OnBeginRoundCountDown;
        ExampleRoomController.onBeginRound -= OnBeginRound;
        ExampleRoomController.onRoundEnd -= OnRoundEnd;

        if (newView != null)
        {
            Destroy(newView.gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        ExampleManager.Instance.OnEditorQuit();
    }
#endif

#endregion

    #region UI

    public void PlayerReadyToPlay()
    {
        ExampleManager.NetSend("setAttribute",
            new ExampleAttributeUpdateMessage
            {
                userId = ExampleManager.Instance.CurrentUser.id,
                attributesToSet = new Dictionary<string, string> { { "readyState", "ready" } }
            });
        if (eGameState.START != PlayMode)
        {
            PlayMode = eGameState.WAITINGFOROTHERS;
            PlayerManager player = GetPlayerView(ExampleManager.Instance.CurrentNetworkedEntity.id);
            if (player != null)
            {
                player.UpdateReadyState(true);
            }
        }
    }

    public PlayerManager GetPlayerView(string entityID)
    {
        if (ExampleManager.Instance.HasEntityView(entityID))
        {
            return ExampleManager.Instance.GetEntityView(entityID) as PlayerManager;
        }

        return null;
    }

    private void UpdateUserName()
    {
        StringBuilder s = new StringBuilder("Room members: ");
        int childCount = this.transform.childCount; 
        this.readyNumbers = 0;

        for (int i = 0; i < childCount; i++)
        {
            string name = this.transform.GetChild(i).name;
            if (name != null && name.Length > 0)
            {
                s.Append(name);
                if (name[0] == '<') readyNumbers++;
                if (i < childCount - 1)
                {
                    s.Append(", ");
                }
            }
        }
        if (readyNumbers == childCount && childCount > 1)
        {
            uiController.UpdatePlayerStatus();
            PlayMode = eGameState.START;
            LSLog.LogImportant("All player ready to play game");
            UpdateTag(childCount);
        }
        players.text = s.ToString();
    }

    private void UpdateTag(int childCount)
    {
        for (int i = 0; i < childCount; i++)
        {
            this.transform.GetChild(i).tag = "Player";
        }
    }

    #endregion


    #region ColySeusCall Back

    private void GotNewTargetLineUp(GoldMinerNewTargetLineUpMessage targetLineUp)
    {
        if (targetLineUp == null || targetLineUp.targets == null)
        {
            LSLog.LogError("No targets came in");
            return;
        }
        int targetCount = targetLineUp.targets.Length;
        for (int i = 0; i < targetCount; ++i)
        {
           /* Debug.Log("row " + targetLineUp.targets[i].row +
                " comlumn : " + int.Parse(targetLineUp.targets[i].uid)
                 + "id :" + targetLineUp.targets[i].id);*/
            mapController.CreateMap(targetLineUp.targets[i].row,  int.Parse(targetLineUp.targets[i].uid), targetLineUp.targets[i].id);
            
        }
    }

    private void OnBeginRound()
    {
        StartCoroutine(DelayedRoundBegin());
    }

    private IEnumerator DelayedRoundBegin()
    {
        yield return new WaitForSeconds(1);
        _countDownString = "";
        _showCountdown = false;
        //fix
    }

    private void OnBeginRoundCountDown()
    {
        _showCountdown = true;
    }

    private void OnRoundEnd(Winner winner)
    {
        PlayerManager player = GetPlayerView(ExampleManager.Instance.CurrentNetworkedEntity.id);
        if (player != null)
        {
            player.UpdateReadyState(false);
        }
        string winnerMessage = GetWinningMessage(winner);
        StartCoroutine(DelayedRoundEnd());
    }

    private IEnumerator DelayedRoundEnd()
    {
        yield return new WaitForSeconds(5);
        if ((currentGameState == eGameState.WAITING || currentGameState == eGameState.WAITINGFOROTHERS) && lastGameState == eGameState.ENDROUND)
        {
            //uiController.UpdatePlayerStatus(AwaitingPlayerReady());
        }
    }

    public bool AwaitingPlayerReady()
    {
        //Returns true if we're waiting for THIS player to be ready
        if (currentGameState == eGameState.WAITING)
        {
            return true;
        }

        return false;
    }

    private string GetWinningMessage(Winner winner)
    {
        string winnerMessage = "";

        if (winner.tie)
        {
            winnerMessage = $"TIE!\nThese players tied with a top score of {winner.score}:\n";
            for (int i = 0; i < winner.tied.Length; i++)
            {
                PlayerManager p = GetPlayerView(winner.tied[i]);
                if (p != null)
                {
                    winnerMessage += $"{(p ? p.userName : winner.tied[i])}\n";
                }
            }
        }
        else
        {
            PlayerManager p = GetPlayerView(winner.id);
            if (p != null)
            {
                winnerMessage = $"Round Over!\n{(p ? p.userName : winner.id)} wins!";
            }
        }

        return winnerMessage;
    }

    private eGameState TranslateGameState(string gameState)
    {
        switch (gameState)
        {
            case "Waiting":
                {
                    PlayerManager player = GetPlayerView(ExampleManager.Instance.CurrentNetworkedEntity.id);
                    if (player != null)
                    {
                        return player.isReady ? eGameState.WAITINGFOROTHERS : eGameState.WAITING;
                    }

                    return eGameState.WAITING;
                }
            case "SendTargets":
                {
                    return eGameState.SENDTARGETS;
                }
            case "BeginRound":
                {
                    return eGameState.BEGINROUND;
                }
            case "SimulateRound":
                {
                    return eGameState.SIMULATEROUND;
                }
            case "EndRound":
                {
                    return eGameState.ENDROUND;
                }
            default:
                return eGameState.NONE;
        }
    }
   
    private void OnRoomStateChanged(MapSchema<string> attributes)
    {
        if (PlayMode == eGameState.NONE && attributes.ContainsKey("generalMessage"))
        {
            PlayMode = eGameState.BEGINROUND;
            this.siblingCurrent = int.Parse(attributes["generalMessage"]);
        }
        if (_showCountdown && attributes.ContainsKey("countDown"))
        {
            _countDownString = attributes["countDown"];
        }
        else
        {
            _countDownString = null;
        }

        if (attributes.ContainsKey("currentGameState"))
        {
            currentGameState = TranslateGameState(attributes["currentGameState"]);
        }

        if (attributes.ContainsKey("lastGameState"))
        {
            lastGameState = TranslateGameState(attributes["lastGameState"]);
        }
    }

    private void OnNetworkAdd(ExampleNetworkedEntity entity)
    {
        if (!ExampleManager.Instance.HasEntityView(entity.id))
        {
            CreateView(entity);
        }
    }

    private void OnNetworkRemove(ExampleNetworkedEntity entity, ColyseusNetworkedEntityView view)
    {
        RemoveView(view);
    }

    private void CreateView(ExampleNetworkedEntity entity)
    {
        StartCoroutine(WaitingEntityAdd(entity));
    }

    private void RemoveView(ColyseusNetworkedEntityView view)
    {
        view.SendMessage("OnEntityRemoved", SendMessageOptions.DontRequireReceiver);
        Destroy(view.gameObject);
    }

    IEnumerator WaitingEntityAdd(ExampleNetworkedEntity entity)
    {
        newView = Instantiate(prefab, this.gameObject.transform);
        ExampleManager.Instance.RegisterNetworkedEntityView(entity, newView);
        float seconds = 0;
        float delayAmt = 1.0f;
        //Wait until we have the view's username to add it's scoreboard entry
        while (string.IsNullOrEmpty(newView.userName))
        {
            yield return new WaitForSeconds(delayAmt);
            seconds += delayAmt;
            if (seconds >= 30) //If 30 seconds go by and we don't have a userName, should still continue
            {
                newView.userName = "GUEST";
            }
        }
    }
    #endregion
}
