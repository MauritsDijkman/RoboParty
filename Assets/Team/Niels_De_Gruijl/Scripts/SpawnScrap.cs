using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnScrap : MonoBehaviourPun
{
    [Header("Conveyorbelt direction")]
    [SerializeField] private bool dirRight = false;
    [SerializeField] private bool dirLeft = false;

    [Header("Prefabs")]
    [SerializeField] private GameObject scraps;
    [SerializeField] private GameObject bomb;

    [SerializeField] private float bombSpawnChance;
    [SerializeField] private float spawnDelay;

    void Start()
    {
        StartCoroutine(WaitForSpawnDelay());
    }

    IEnumerator WaitForSpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnScrapPrefab();
        StartCoroutine(WaitForSpawnDelay());
    }

    void SpawnScrapPrefab()
    {
        if (photonView.IsMine)
        {
            if (Random.Range(0f, 1f) > 0.2f)
            {
                if (dirRight)
                {
                    //GameObject scrap = (GameObject)PhotonNetwork.Instantiate(scraps.name, transform.position, Quaternion.identity);
                    GameObject scrap = (GameObject)Instantiate(scraps, transform.position, Quaternion.identity);
                }
                else if (dirLeft)
                {
                    //GameObject scrap = (GameObject)PhotonNetwork.Instantiate(scraps.name, transform.position, Quaternion.identity);
                    GameObject scrap = (GameObject)Instantiate(scraps, transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (dirRight)
                {
                    //GameObject bombObject = (GameObject)PhotonNetwork.Instantiate(bomb.name, transform.position, Quaternion.identity);
                    GameObject bombObject = (GameObject)Instantiate(bomb, transform.position, Quaternion.identity);
                }
                else if (dirLeft)
                {
                    //GameObject bombObject = (GameObject)PhotonNetwork.Instantiate(bomb.name, transform.position, Quaternion.identity);
                    GameObject bombObject = (GameObject)Instantiate(bomb, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
