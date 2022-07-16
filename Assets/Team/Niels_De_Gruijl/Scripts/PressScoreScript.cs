using UnityEngine;
using TMPro;
using Photon.Pun;

public class PressScoreScript : MonoBehaviourPun
{
    [Header("UI")]
    [SerializeField] public TMP_Text scoreText = null;
    private int score = 0;

    [Header("Ingot")]
    [SerializeField] private GameObject ingot;

    [Header("Particles")]
    [SerializeField] private GameObject sparks;
    [SerializeField] private GameObject explosion;

    private string ownerName = "";
    private Vector3 ingotPos;

    private bool objectSpawned = false;

    private float timer = 1f;

    private void Start()
    {
        Debug.Log($"Parent name: {transform.parent.name}");

        if (transform.parent.name == "Press1" || transform.parent.name == "Press1(Clone)")
        {
            scoreText = GameObject.Find("HostCanvas/UI/Score_Player1").GetComponent<TMP_Text>();
            ownerName = "Player1";
        }
        else if (transform.parent.name == "Press2" || transform.parent.name == "Press2(Clone)")
        {
            scoreText = GameObject.Find("HostCanvas/UI/Score_Player2").GetComponent<TMP_Text>();
            ownerName = "Player2";
        }
        else if (transform.parent.name == "Press3" || transform.parent.name == "Press3(Clone)")
        {
            scoreText = GameObject.Find("HostCanvas/UI/Score_Player3").GetComponent<TMP_Text>();
            ownerName = "Player3";
        }
        else if (transform.parent.name == "Press4" || transform.parent.name == "Press4(Clone)")
        {
            scoreText = GameObject.Find("HostCanvas/UI/Score_Player4").GetComponent<TMP_Text>();
            ownerName = "Player4";
        }

        score = 0;
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        if (objectSpawned)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else if (timer <= 0)
            {
                timer = 1f;
                objectSpawned = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomb"))
        {
            if (score > 0)
            {
                score--;
                scoreText.text = score.ToString();

                Debug.Log("PlayerScore: " + score);

                PlayerPrefs.SetInt(ownerName + "_Score_Level3", score);
                PlayerPrefs.Save();

                PhotonNetwork.Instantiate(explosion.name, other.transform.position, Quaternion.identity);
            }
        }
        else if (other.CompareTag("Scrap") && !objectSpawned)
        {
            //Destroy(other);

            Debug.Log($"Other.gameObject destroyed: {other}");

            Debug.Log("Scrap has collided");

            score++;
            scoreText.text = score.ToString();

            Debug.Log("PlayerScore: " + score);

            PlayerPrefs.SetInt(ownerName + "_Score_Level3", score);
            PlayerPrefs.Save();

            PhotonNetwork.Instantiate(sparks.name, other.transform.position, Quaternion.identity);

            ingotPos = other.gameObject.transform.position;

            ObjectMover previousObjectMover = other.gameObject.GetComponent<ObjectMover>();

            //GameObject ingotObject = (GameObject)PhotonNetwork.Instantiate(ingot.name, ingotPos, Quaternion.identity).AddComponent<ObjectMover>().SetVelocity(previousObjectMover.GetVelocity());
            //PhotonNetwork.Instantiate(ingot.name, ingotPos, Quaternion.identity).AddComponent<ObjectMover>().SetVelocity(previousObjectMover.GetVelocity());
            PhotonNetwork.Instantiate(ingot.name, ingotPos, Quaternion.identity).AddComponent<ObjectMover>().SetVelocity(previousObjectMover.GetVelocity());

            //PhotonNetwork.Destroy(other.gameObject);
            objectSpawned = true;
        }

        if(PhotonNetwork.IsMasterClient)
            Destroy(other.gameObject);

        //Destroy(other.gameObject);
    }
}
