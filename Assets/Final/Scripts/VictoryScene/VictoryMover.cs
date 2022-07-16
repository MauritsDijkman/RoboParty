using UnityEngine;
using TMPro;

public class VictoryMover : MonoBehaviour
{
    private float score = 0;
    private float speed = 0;

    private bool particleSpawned = false;
    private bool isMoving = true;

    [Header("Values")]
    [SerializeField] private VictoryScoreSetter scoreSetter = null;
    [SerializeField] private GameObject particleSpawnpoint = null;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText = null;

    public void SetStats(float pScore, float pSpeed)
    {
        score = pScore / 4;     // Devided so that it doesn't take ages to go up to the real score (20 score is y height of 20)
        speed = pSpeed;
    }

    private void Update()
    {
        SetScore(score, speed);
        VisualizeScore();
    }

    private void SetScore(float pTargetHeight, float pSpeed)
    {
        if (transform.localScale.y < pTargetHeight)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + pSpeed * Time.deltaTime, transform.localScale.z);
            isMoving = true;
        }
        else if (transform.localScale.y >= pTargetHeight && !particleSpawned)
        {
            GameObject particle = scoreSetter.GetParticle();
            Instantiate(particle, particleSpawnpoint.transform.position, particle.transform.rotation);
            particleSpawned = true;
            isMoving = false;
        }
    }
    private void VisualizeScore()
    {
        scoreText.text = (transform.localScale.y * 4).ToString("F0");
    }

    public bool GetMovingStatus()
    {
        return isMoving;
    }
}
