using System;
using System.Collections;
using Colyseus;
using DatabaseAPI.Account;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RoomSelectionMenu : MonoBehaviour
{
    [Serializable]
    public class MyEvent : UnityEvent { }
   
    [SerializeField]
    private GameObject entryPrefab = null;

    [SerializeField]
    private Transform entryRoot = null;

    [SerializeField]
    private LobbyController lobbyController = null;

    private Coroutine refreshRoutine;

    private float refreshTimer = 5f;

    private IEnumerator RefreshRoutine()
    {
        while (gameObject.activeSelf && ExampleManager.Instance.UserName != null)
        {
            GetAvailableRooms();
            yield return new WaitForSeconds(refreshTimer);
        }
    }

    public void GetAvailableRooms()
    {
        if (gameObject.activeSelf && ExampleManager.Instance.UserName != null)
        {
            ExampleManager.Instance.GetAvailableRooms();
        }
    }

    public void HandRooms(ColyseusRoomAvailable[] rooms)
    {
        if (refreshRoutine == null)
        {
            refreshRoutine = StartCoroutine(RefreshRoutine());
        }

        for (int i = 0; i < entryRoot.childCount; ++i)
        {
            Destroy(entryRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < rooms.Length; ++i)
        {
            GameObject newEntry = Instantiate(entryPrefab, entryRoot, false);
            RoomListItem listItem = newEntry.GetComponent<RoomListItem>();
            listItem.Initialize(rooms[i], this);
        }
    }

    public void JoinRoom(string roomID)
    {
        lobbyController.JoinRoom(roomID);
    }
}