using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public TMP_Text scoreText = null;
    private int score = 0;

    private PlayerCollectibleInformation tagInformation;
    private string ownerName = "";

    private void Awake()
    {
        tagInformation = FindObjectOfType<PlayerCollectibleInformation>();
    }

    private void Start()
    {
        if (transform.parent.parent.name == "Player1" || transform.parent.parent.name == "Player1(Clone)")
        {
            scoreText = GameObject.Find("HostCanvas/UI/Score_Player1").GetComponent<TMP_Text>();
            ownerName = "Player1";
        }
        else if (transform.parent.parent.name == "Player2" || transform.parent.parent.name == "Player2(Clone)")
        {
            scoreText = GameObject.Find("HostCanvas/UI/Score_Player2").GetComponent<TMP_Text>();
            ownerName = "Player2";
        }
        else if (transform.parent.parent.name == "Player3" || transform.parent.parent.name == "Player3(Clone)")
        {
            scoreText = GameObject.Find("HostCanvas/UI/Score_Player3").GetComponent<TMP_Text>();
            ownerName = "Player3";
        }
        else if (transform.parent.parent.name == "Player4" || transform.parent.parent.name == "Player4(Clone)")
        {
            scoreText = GameObject.Find("HostCanvas/UI/Score_Player4").GetComponent<TMP_Text>();
            ownerName = "Player4";
        }

        score = 0;
        scoreText.text = "" + score;
    }


    private void Update()
    {
        //Debug.Log($"Owner name: {ownerName + "_Score_Level1"} || Score: {score}");

        PlayerPrefs.SetInt(ownerName + "_Score_Level1", score);
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (PlayerCollectibleInformation.CollisionInformation _object in tagInformation.objects)
        {
            if (_object.tag == other.gameObject.tag)
            {
                //Debug.Log($"Object tag: {_object.tag} || Collision tag: {other.gameObject.tag} || Same: {_object.tag == other.gameObject.tag}");

                ChangeScore(_object.points);

                Destroy(other.gameObject);
                return;
            }
        }
    }

    private void ChangeScore(int pScore)
    {
        if (score >= 0 && pScore > 0)
        {
            score += pScore;
            scoreText.text = "" + score;
        }
        else if (score > 0 && pScore < 0)
        {
            score += pScore;
            scoreText.text = "" + score;
        }
    }
}
