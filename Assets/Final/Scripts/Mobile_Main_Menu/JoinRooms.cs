using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class JoinRooms : MonoBehaviourPunCallbacks
{
    [Header("Text Input")]
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField roomInput;

    [Header("Button")]
    [SerializeField] private Button joinRoomButton;

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInput.text);
    }

    public override void OnJoinedRoom()
    {
        SetPlayerNames();
        PhotonNetwork.LoadLevel("PC_Join_Screen");
    }

    void SetPlayerNames()
    {
        switch (PhotonNetwork.CurrentRoom.PlayerCount)
        {
            case 2:
                SetPlayerName(1, nameInput.text);
                //Debug.Log($"Player 1 name set function is called!");
                break;

            case 3:
                SetPlayerName(2, nameInput.text);
                //Debug.Log($"Player 2 name set function is called!");
                break;

            case 4:
                SetPlayerName(3, nameInput.text);
                //Debug.Log($"Player 3 name set function is called!");
                break;

            case 5:
                SetPlayerName(4, nameInput.text);
                //Debug.Log($"Player 4 name set function is called!");
                break;

            default:
                Debug.Log($"Room '{roomInput.text}' is full!");
                break;
        }
    }

    void SetPlayerName(int pPlayerIndex, string pPlayerName)
    {
        DeviceInfo.playerIndex = pPlayerIndex;
        DeviceInfo.playerName = pPlayerName;

        //Debug.Log($"DeviceInfo_PlayerIndex: {DeviceInfo.playerIndex} || DeviceInfo_PlayerName: {DeviceInfo.playerName}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        Debug.Log($"Room '{roomInput.text}' doesn not exist!");
    }
}
