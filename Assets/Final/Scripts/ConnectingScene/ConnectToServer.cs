using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            DeviceInfo.isHost = true;
            DeviceInfo.isClient = false;
            //Debug.Log($"Device is host");
        }
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            DeviceInfo.isHost = false;
            DeviceInfo.isClient = true;
            //Debug.Log($"Device is client");
        }

        PhotonNetwork.PhotonServerSettings.DevRegion = PhotonNetwork.CloudRegion;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        if (DeviceInfo.isHost)
            SceneManager.LoadScene("PC_Main_Menu");
        else if (DeviceInfo.isClient)
            SceneManager.LoadScene("Mobile_Main_Menu");
    }
}
