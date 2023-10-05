using Colyseus;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI clientCount = null;

    [SerializeField]
    private ButtonManager joinButton = null;

    private RoomSelectionMenu menuRef;

    [SerializeField]
    private TextMeshProUGUI roomName = null;

    private ColyseusRoomAvailable roomRef;

    public void Initialize(ColyseusRoomAvailable roomReference, RoomSelectionMenu menu)
    {
        menuRef = menu;
        roomRef = roomReference;
        roomName.text = roomReference.roomId;
        string maxClients = roomReference.maxClients > 0 ? roomReference.maxClients.ToString() : "--";
        clientCount.text = $"{roomReference.clients} / {maxClients}";
        //TODO: if we want to lock rooms, will need to do so here
        if (roomReference.maxClients > 0 && roomReference.clients >= roomReference.maxClients)
        {
            joinButton.isInteractable = false;
        }
        else
        {
            joinButton.isInteractable = true;
        }
    }

    public void TryJoin()
    {
        menuRef.JoinRoom(roomRef.roomId);
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_touch);
    }
}