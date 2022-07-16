using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PhotonTest : MonoBehaviour
{
    private TMP_Text loadAmountText = null;
    private Image progressBar = null;

    private void Start()
    {
        StartCoroutine(LoadLevelAsync("Game_1"));
    }

    private IEnumerator LoadLevelAsync(string pLevel)
    {
        PhotonNetwork.LoadLevel(pLevel);

        while (PhotonNetwork.LevelLoadingProgress < 1)
        {
            Debug.Log("Loading: " + (int)(PhotonNetwork.LevelLoadingProgress * 100) + "%");

            loadAmountText.text = "Loading: " + (int)(PhotonNetwork.LevelLoadingProgress * 100) + "%";
            progressBar.fillAmount = PhotonNetwork.LevelLoadingProgress;

            yield return new WaitForEndOfFrame();
        }
    }
}
