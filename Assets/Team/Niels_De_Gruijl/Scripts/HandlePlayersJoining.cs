using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class HandlePlayersJoining : MonoBehaviourPunCallbacks
{
    [Header("Player prefabs")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player3;
    [SerializeField] private GameObject player4;

    [Header("Player positions")]
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;
    [SerializeField] private Vector3 pos3;
    [SerializeField] private Vector3 pos4;

    [Header("Player rotation")]
    [SerializeField] private Vector3 rot1;
    [SerializeField] private Vector3 rot2;
    [SerializeField] private Vector3 rot3;
    [SerializeField] private Vector3 rot4;

    [Header("Host/Client")]
    [SerializeField] private GameObject Host;
    [SerializeField] private GameObject Client;

    private GameObject playerPrefab;

    private int playersJoined = 0;

    private float playerPos;
    private float playerDistance = 2;
    private int prefabIndex = 0;

    private void Start()
    {
        /*        if (DeviceInfo.isHost)
                {
                    Client.SetActive(false);
                }
                if (DeviceInfo.isClient)
                {
                    Host.SetActive(false);
                }*/

        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        //Debug.Log("Player entered the room");

        switch (DeviceInfo.playerIndex)
        {
            case 1:
                GameObject p1 = PhotonNetwork.Instantiate(player1.name, pos1, Quaternion.Euler(rot1));
                break;
            case 2:
                GameObject p2 = PhotonNetwork.Instantiate(player2.name, pos2, Quaternion.Euler(rot2));
                break;
            case 3:
                GameObject p3 = PhotonNetwork.Instantiate(player3.name, pos3, Quaternion.Euler(rot3));
                break;
            case 4:
                GameObject p4 = PhotonNetwork.Instantiate(player4.name, pos4, Quaternion.Euler(rot4));
                break;
            default:
                Debug.Log("Error, room is full");
                break;
        }
    }
}
