using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ReadyButton : MonoBehaviourPun
{
    [Header("Buttons")]
    [SerializeField] private Button readyButtonPressed = null;
    [SerializeField] private Button readyButtonNotPressed = null;

    private void Start()
    {
        UnPressButton();
    }

    public void PressButton()
    {
        readyButtonPressed.gameObject.SetActive(true);
        readyButtonNotPressed.gameObject.SetActive(false);

        DeviceInfo.playerIsReady = true;

        if (DeviceInfo.isClient)
            photonView.RPC("GiveStatusToHost", RpcTarget.AllBuffered, DeviceInfo.playerIndex, DeviceInfo.playerIsReady);
    }

    public void UnPressButton()
    {
        readyButtonPressed.gameObject.SetActive(false);
        readyButtonNotPressed.gameObject.SetActive(true);

        DeviceInfo.playerIsReady = false;

        if (DeviceInfo.isClient)
            photonView.RPC("GiveStatusToHost", RpcTarget.AllBuffered, DeviceInfo.playerIndex, DeviceInfo.playerIsReady);
    }

    [PunRPC]
    void GiveStatusToHost(int pPlayerIndex, bool pPlayerReady)
    {
        //PlayerPrefs.DeleteKey($"Player{pPlayerIndex}_Ready");

        if (!pPlayerReady)
            PlayerPrefs.SetInt($"Player{pPlayerIndex}_Ready", 0);
        else if (pPlayerReady)
            PlayerPrefs.SetInt($"Player{pPlayerIndex}_Ready", 1);

        PlayerPrefs.Save();

        //Debug.Log($"PlayerIndex: {pPlayerIndex} || PlayerReady: {pPlayerReady}");
    }
}
