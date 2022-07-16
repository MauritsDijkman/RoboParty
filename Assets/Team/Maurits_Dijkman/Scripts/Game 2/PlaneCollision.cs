using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlaneCollision : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text scoreText = null;
    [SerializeField] private GameObject gameOverUI = null;

    [Header("Particle")]
    [SerializeField] private GameObject hoopParticle = null;
    [SerializeField] private GameObject birdParticle = null;

    private int score = 0;
    private string ownerName = "";

    private bool damageAllowed = true;
    private float timer = 1f;

    private void Start()
    {
        score = 0;

        //Debug.Log("Name: " + transform.name);

        if (transform.name == "Player1" || transform.name == "Player1(Clone)" || transform.name == "Plane1" || transform.name == "Plane1(Clone)")
        {
            scoreText = GameObject.Find("Score_Player1").GetComponent<TMP_Text>();
            gameOverUI = GameObject.Find("GameOver_Player1").gameObject;
            ownerName = "Player1";
        }
        else if (transform.name == "Player2" || transform.name == "Player2(Clone)" || transform.name == "Plane2" || transform.name == "Plane2(Clone)")
        {
            scoreText = GameObject.Find("Score_Player2").GetComponent<TMP_Text>();
            gameOverUI = GameObject.Find("GameOver_Player2").gameObject;
            ownerName = "Player2";
        }
        else if (transform.name == "Player3" || transform.name == "Player3(Clone)" || transform.name == "Plane3" || transform.name == "Plane3(Clone)")
        {
            scoreText = GameObject.Find("Score_Player3").GetComponent<TMP_Text>();
            gameOverUI = GameObject.Find("GameOver_Player3").gameObject;
            ownerName = "Player3";
        }
        else if (transform.name == "Player4" || transform.name == "Player4(Clone)" || transform.name == "Plane4" || transform.name == "Plane4(Clone)")
        {
            scoreText = GameObject.Find("Score_Player4").GetComponent<TMP_Text>();
            gameOverUI = GameObject.Find("GameOver_Player4").gameObject;
            ownerName = "Player4";
        }

        if (gameOverUI.activeSelf)
            gameOverUI.SetActive(false);

        UpdateUI();
    }

    private void Update()
    {
        Debug.Log($"Damage allowed: {damageAllowed}");

        if (!damageAllowed)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else if (timer <= 0)
                damageAllowed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collider: {other.tag}");

        if (other.CompareTag("Hoop"))
        {
            Debug.Log("Score is plus 1");

            score++;
            UpdateUI();

            //GameObject spawnedParticle = (GameObject)Instantiate(hoopParticle, other.gameObject.transform.position, hoopParticle.transform.rotation);
            GameObject spawnedParticle = (GameObject)PhotonNetwork.Instantiate(hoopParticle.name, other.gameObject.transform.position, hoopParticle.transform.rotation);

            float particleDuration = spawnedParticle.GetComponent<ParticleSystem>().duration;
            //PhotonNetwork.Destroy(spawnedParticle, particleDuration);
        }

        if (other.CompareTag("Bird"))
        {
            if (damageAllowed)
            {
                if (score > 0)
                    score--;

                UpdateUI();

                damageAllowed = false;
                timer = 2f;

                //GameObject spawnedParticle = (GameObject)Instantiate(birdParticle, other.gameObject.transform.position, birdParticle.transform.rotation);
                GameObject spawnedParticle = (GameObject)PhotonNetwork.Instantiate(birdParticle.name, other.gameObject.transform.position, birdParticle.transform.rotation);

                float particleDuration = spawnedParticle.GetComponent<ParticleSystem>().duration;
                //PhotonNetwork.Destroy(spawnedParticle, particleDuration);
            }
        }
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString();

        PlayerPrefs.SetInt(ownerName + "_Score_Level2", score);
        PlayerPrefs.Save();
    }

    public void SetGameOverUI()
    {
        if (!gameOverUI.activeSelf)
            gameOverUI.SetActive(true);
    }
}
