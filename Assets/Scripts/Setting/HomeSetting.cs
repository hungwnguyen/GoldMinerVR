using DatabaseAPI.Account;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        usernameSetting.text = AccountController.usernameDisplay;
    }

    public override void SaveChangeSetting()
    {
        base.SaveChangeSetting();
        string s = changeUsername.text;
        try
        {
            int.Parse(s + "0");
        }
        catch
        {
            s = s[0].ToString().ToUpper() + (s.Length > 1 ? s.Substring(1) : "");
            AccountController.controller.ChangeUserName(s);
            usernameSetting.text = s;
            ExampleManager.Instance.UserName = s;
            Debug.Log(s);
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
