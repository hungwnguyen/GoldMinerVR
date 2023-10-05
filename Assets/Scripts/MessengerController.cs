using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessengerController : MonoBehaviour
{
    [Tooltip("Current button of messenger controller gameObject")]
    private Button current;
    private Image mes;
    [SerializeField]
    private TMP_InputField messenger;

    // Start is called before the first frame update
    void Start()
    {
        this.current = this.GetComponent<Button>();
        mes = this.GetComponent<Image>();
        this.current.onClick.AddListener(SendMessenger);
        messenger.onEndEdit.AddListener(SendMestoNetwork);
    }

    private void SendMestoNetwork(string value)
    {
        PlayerManager player = GameManager.Instance.GetPlayerView(ExampleManager.Instance.CurrentNetworkedEntity.id);
        if (player != null)
        {
            player.UpdateMessenger(value);
        }
        messenger.text = "";
    }
    private void SendMessenger()
    {
        StartCoroutine(DelaySend());
    }

    IEnumerator DelaySend()
    {
        this.current.enabled = false;
        mes.color = new Color32(172, 172, 172, 255);
        yield return new WaitForSeconds(2);
        this.current.enabled = true;
        mes.color = new Color32(255, 255, 255, 255);
    }
}
