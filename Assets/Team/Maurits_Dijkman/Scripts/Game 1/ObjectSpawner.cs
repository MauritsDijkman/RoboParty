using UnityEngine;
using Photon.Pun;

public class ObjectSpawner : MonoBehaviourPun
{
    [System.Serializable]
    public class ObjectInformationHolder
    {
        public GameObject spawnObject = null;
        [Range(0f, 100f)] public int changeOfSpawning = 100;
        [HideInInspector] public int _weight;
    }

    private int accumulatedWeights;
    private System.Random rand = new System.Random();

    [Header("Collectible")]
    [SerializeField] private ObjectInformationHolder[] collectibles = null;

    [Header("Spawn coordinates")]
    [SerializeField] private Vector3 left;
    [SerializeField] private Vector3 right;

    [Header("Spawn coordinates")]
    [SerializeField] private float startAfterSeconds = 1.0f;
    [SerializeField] private float repeatRate = 0.5f;

    [Header("Timer")]
    [SerializeField] private float timeLeftBeforeSpawningStops = 1.5f;

    private Timer timer = null;
    private StartTimer startTimer = null;

    GameObject collectible;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        startTimer = FindObjectOfType<StartTimer>();

        CalculateWeights();
    }

    private void Start()
    {
        InvokeRepeating("SpawnCollectible", startAfterSeconds, repeatRate);
    }

    private void SpawnCollectible()
    {
        if (timer != null && startTimer != null)
            if (timer.time <= timeLeftBeforeSpawningStops || !startTimer.inputAllowed)
                return;

        GameObject randomObject = ReturnObject();
        // GameObject collectible = (GameObject)Instantiate(randomObject, GetRandomVector3(left, right, 0.8f, 0.8f), randomObject.transform.rotation);

        if (gameObject.name == "ScrapSpawner")
        {
            if (photonView.IsMine)
                collectible = (GameObject)PhotonNetwork.Instantiate(randomObject.name, GetRandomVector3(left, right, 1, 1), randomObject.transform.rotation);
        }
        else
        {
            //if (photonView.IsMine)
            //    collectible = (GameObject)PhotonNetwork.Instantiate(randomObject.name, GetRandomVector3(left, right, 1, 1), randomObject.transform.rotation);
            //else
            GameObject collectible = (GameObject)PhotonNetwork.Instantiate(randomObject.name, GetRandomVector3(left, right, 1, 1), randomObject.transform.rotation);

            collectible.SetActive(true);
            //collectible = (GameObject)Instantiate(randomObject, GetRandomVector3(left, right, 1, 1), randomObject.transform.rotation);
        }
    }

    private GameObject ReturnObject()
    {
        GameObject randomObject = collectibles[GetRandomObjectIndex()].spawnObject;
        return randomObject;
    }

    private Vector3 GetRandomVector3(Vector3 min, Vector3 max, float minPadding, float maxPadding)
    {
        Vector3 point1 = min + minPadding * (max - min);
        Vector3 point2 = max + maxPadding * (min - max);

        return point1 + Random.Range(0f, 1f) * (point2 - point1);
    }

    private void CalculateWeights()
    {
        accumulatedWeights = 0;

        foreach (ObjectInformationHolder _object in collectibles)
        {
            accumulatedWeights += _object.changeOfSpawning;
            _object._weight = accumulatedWeights;
        }
    }

    private int GetRandomObjectIndex()
    {
        int r = (int)(rand.NextDouble() * accumulatedWeights);

        for (int i = 0; i < collectibles.Length; i++)
            if (collectibles[i]._weight >= r)
                return i;

        return 0;
    }
}
