using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateRooms : MonoBehaviourPunCallbacks
{
    [Header("Room input")]
    [SerializeField] private TMP_InputField roomName;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomName.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedLobby();
        PhotonNetwork.LoadLevel("PC_Join_Screen");
    }
}
