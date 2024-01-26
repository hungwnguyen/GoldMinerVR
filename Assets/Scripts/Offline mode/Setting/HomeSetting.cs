#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;
using UnityEngine;

public class HomeSetting : Setting
{
    [SerializeField] private TextMeshProUGUI usernameSetting;

    [SerializeField] private TMP_InputField changeUsername;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //usernameSetting.text = AccountController.usernameDisplay;
        usernameSetting.text = PlayerPrefs.GetString("Player name", "");
    }

    public void SignOutGame()
    {

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
