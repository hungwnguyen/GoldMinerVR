using UnityEngine;
using System.Collections;
using TMPro;
using LucidSightTools;
using System.Collections.Generic;
using Michsky.MUIP;

public class PlayerManager : ExampleNetworkedEntityView
{
    public TextMeshProUGUI userNameDisplay;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public string userName = null;

    public bool isReady = false;

    [SerializeField] private NotificationManager messengerPopup;
    [SerializeField] private TextMeshProUGUI messengerText;

    public void UpdateReadyState(bool ready)
    {
        if (IsMine)
        {
            isReady = ready;
            if (ready)
            {
                base.SetName();
            }
            else
            {
                this.gameObject.name = userName;
            }
            SetAttributes(new Dictionary<string, string>() { { "isReady", isReady.ToString() } });
        }
    }

    public void UpdateMessenger(string s)
    {
        if (!string.IsNullOrEmpty(s))
        {
            s = s.Substring(0, s.Length > 40 ? 40 : s.Length);
            messengerText.text = s;
            messengerPopup.OpenNotification();
            SetAttributes(new Dictionary<string, string>() { { "messenger", s } });
        }
    }

    protected override void UpdateStateFromView()
    {
        base.UpdateStateFromView();

    }

    protected override void UpdateViewFromState()
    {
        base.UpdateViewFromState();
        if (string.IsNullOrEmpty(userName) && state.attributes.ContainsKey("userName"))
        {
            userName = state.attributes["userName"];
            userNameDisplay.text = userName;
            gameObject.name = userName;
            userNameDisplay.color = Color.red;
        }
        if (state.attributes.TryGetValue("isReady", out string readyState))
        {
            isReady = bool.Parse(readyState);
            if (isReady)
            {
                base.SetName();
            }
            else
            {
                this.gameObject.name = userName;
            }
        }
        
        if (state.attributes.TryGetValue("messenger", out string messenger))
        {
            messengerText.text = messenger;
            messengerPopup.OpenNotification();
        }
    }

    private IEnumerator WaitForConnect()
    {
        if (ExampleManager.Instance.CurrentUser != null && !IsMine)
        {
            yield break;
        }

        while (!ExampleManager.Instance.IsInRoom || string.IsNullOrEmpty(ExampleManager.Instance.UserName))
        {
            yield return new WaitForEndOfFrame();
        }

        while (GameManager.Instance.siblingCurrent == -1)
        {
            yield return new WaitForEndOfFrame();
        }
        LSLog.LogImportant("HAS JOINED ROOM - CREATING ENTITY");

        this.gameObject.name = ExampleManager.Instance.UserName;
        ExampleManager.CreateNetworkedEntityWithTransform(new Vector3(0, -44.3f, 0), Quaternion.identity,
            new Dictionary<string, object> { ["userName"] = ExampleManager.Instance.UserName }, this, entity =>
            {
                userName = ExampleManager.Instance.UserName;
                userNameDisplay.text = userName;
                ExampleManager.Instance.CurrentNetworkedEntity = entity;
            });
        yield return new WaitForEndOfFrame();
        this.transform.SetSiblingIndex(GameManager.Instance.siblingCurrent);
    }

    protected override void Start()
    {
        autoInitEntity = false;
        base.Start();
        userName = string.Empty;
        StartCoroutine("WaitForConnect");
    }


    protected override void Update()
    {
        base.Update();
        if (IsMine)
        {
            UpdateInput();
        }
    }
 
    private void UpdateInput()
    {

    }
}

