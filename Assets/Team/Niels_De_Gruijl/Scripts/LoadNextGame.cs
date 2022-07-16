using UnityEngine;
using Photon.Pun;

public class LoadNextGame : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private string nextScene;

    private bool sceneLoaded = false;

    private void Update()
    {
        if (timer.time <= 0 && !sceneLoaded)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(nextScene);
                sceneLoaded = true;
            }
        }
    }
}
