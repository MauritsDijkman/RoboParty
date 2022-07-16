using UnityEngine;
using TMPro;
using Photon.Pun;

public class LoadingScreenLoader : MonoBehaviourPun
{
    [Header("Time")]
    [SerializeField] public float timer = 20f;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText = null;

    private bool nextSceneIsLoaded = false;

    private void Update()
    {
        UpdateTimer();
        UpdateUI();
    }

    private void UpdateTimer()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else if (timer <= 0)
        {
            if (PhotonNetwork.IsMasterClient && !nextSceneIsLoaded)
            {
                PhotonNetwork.LoadLevel("LoadingScene");
                nextSceneIsLoaded = true;
            }
        }
    }

    private void UpdateUI()
    {
        timerText.text = "Continuing\n in " + timer.ToString("F0");
    }
}
