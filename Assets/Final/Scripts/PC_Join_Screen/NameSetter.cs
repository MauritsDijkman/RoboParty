using UnityEngine;
using Photon.Pun;

public class NameSetter : MonoBehaviourPun
{
    private void Awake()
    {
        if (DeviceInfo.isClient)
            photonView.RPC("GiveNameToHost", RpcTarget.AllBuffered, DeviceInfo.playerIndex, DeviceInfo.playerName);
    }

    [PunRPC]
    void GiveNameToHost(int pPlayerIndex, string pPlayerName)
    {
        if (PlayerPrefs.HasKey($"Player{pPlayerIndex}_Name"))
            PlayerPrefs.DeleteKey($"Player{pPlayerIndex}_Name");

        PlayerPrefs.SetString($"Player{pPlayerIndex}_Name", pPlayerName);
        PlayerPrefs.Save();

        //Debug.Log($"NameSetter: {pPlayerIndex} + {pPlayerName}");
    }
}
