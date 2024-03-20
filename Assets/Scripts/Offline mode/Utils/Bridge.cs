using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using yuki;
public class Bridge : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void InitializeSpeechRecognition(string language);

    public TMP_Text DisplayText;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        try{
            SendToJS();
        } catch{
        }
    }

    public void SendToJS()
    {
// #if UNITY_WEBGL && !UNITY_EDITOR
        InitializeSpeechRecognition("vi-VN");
// #endif
    }

    public void SendToUnity(string message)
    {
        string[] words = message.Split(' ');

        string newMessage = "";

        // Check if the message has more than 2 words
        if (words.Length > 2)
        {
            // Get the last two words of the message
            newMessage = words[words.Length - 2] + " " + words[words.Length - 1];
        }
        else
        {
            newMessage = message;
        }
        DisplayText.text = newMessage;
        animator.SetTrigger("play");
        Player.Instance.OnPointerDown();
    }

}
