using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SetupRooms : MonoBehaviourPunCallbacks
{
    /*    [SerializeField] GameObject host;
        [SerializeField] GameObject client;*/

    [SerializeField] TMP_InputField createRoom;
    [SerializeField] TMP_InputField joinRoom;

    /*    private void Start()
        {
            if (DeviceInfo.isHost)
            {
                host.SetActive(true);
            }
            if (DeviceInfo.isClient)
            {
                client.SetActive(true);
            }
        }*/

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoom.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoom.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (DeviceInfo.isHost)
        {
            PhotonNetwork.LoadLevel("Game1Lobby");
        }
        if (DeviceInfo.isClient)
        {
            PhotonNetwork.LoadLevel("Game1Lobby");
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Room " + joinRoom.text + " doesn't exist");
    }
}
