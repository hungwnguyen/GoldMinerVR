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

    public override void SaveChangeSetting()
    {
        /*if (!AccountController.userName.Equals("Guest"))
        {
            base.SaveChangeSetting();
        }*/
        base.SaveChangeSetting();
        string s = changeUsername.text;
        try
        {
            int.Parse(s + "0");
        }
        catch
        {
            s = s[0].ToString().ToUpper() + (s.Length > 1 ? s.Substring(1) : "");
            usernameSetting.text = s;
            PlayerPrefs.SetString("Player name", s);
        }
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
