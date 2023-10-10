using DatabaseAPI.Account;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class HomeSetting : Setting
{
    [DllImport("__Internal")]
    private static extern void ReloadPage();

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
            ExampleManager.Instance.UserName = s;
            PlayerPrefs.SetString("Player name", s);
            /*if (!AccountController.userName.Equals("Guest"))
            {
                s = s[0].ToString().ToUpper() + (s.Length > 1 ? s.Substring(1) : "");
                AccountController.controller.ChangeUserName(s);
            }
            usernameSetting.text = s;
            ExampleManager.Instance.UserName = s;*/
        }
    }


    public void SignOutGame()
    {
        //AccountController.controller.LOG_OUT_GAME();
        Application.Quit();
#if UNITY_WEBGL
        ReloadPage();
#endif
    }

}
