using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Lobby1 : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text p1;
    [SerializeField] private Text p2;
    [SerializeField] private Text p3;
    [SerializeField] private Text p4;

    [SerializeField] private Button start;

    private void Start()
    {
        if (DeviceInfo.isHost)
            start.gameObject.SetActive(true);

        SetPlayerIndex();
        photonView.RPC("UpdatePlayerList", RpcTarget.All);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

/*    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        CheckPlayersJoined();
    }*/

    [PunRPC]
    void UpdatePlayerList()
    {
        switch (PhotonNetwork.PlayerList.Length - 2)
        {
            case 0:
                p1.gameObject.SetActive(true);
                break;
            case 1:
                p1.gameObject.SetActive(true);
                p2.gameObject.SetActive(true);
                break;
            case 2:
                p1.gameObject.SetActive(true);
                p2.gameObject.SetActive(true);
                p3.gameObject.SetActive(true);
                break;
            case 3:
                p1.gameObject.SetActive(true);
                p2.gameObject.SetActive(true);
                p3.gameObject.SetActive(true);
                p4.gameObject.SetActive(true);
                break;
            default:
                Debug.Log("Lobby is full");
                break;
        }
    }

    void SetPlayerIndex()
    {
        switch (PhotonNetwork.PlayerList.Length - 1)
        {
            case 0:
                DeviceInfo.playerIndex = 1;
                break;
            case 1:
                DeviceInfo.playerIndex = 2;
                break;
            case 2:
                DeviceInfo.playerIndex = 3;
                break;
            case 3:
                DeviceInfo.playerIndex = 4;
                break;
            default:
                Debug.Log("Lobby is full");
                break;
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("Game3"); 
    }
}
