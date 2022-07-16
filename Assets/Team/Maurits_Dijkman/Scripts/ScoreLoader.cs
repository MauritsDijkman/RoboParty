using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadSceneAsync("ScoreScene"));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        while (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(sceneName);

        yield return null;
    }
}
