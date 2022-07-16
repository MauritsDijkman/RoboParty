using UnityEngine;
using TMPro;

public class MoverChecker : MonoBehaviour
{
    [Header("VictoryMovers")]
    [SerializeField] private VictoryMover player1 = null;
    [SerializeField] private VictoryMover player2 = null;
    [SerializeField] private VictoryMover player3 = null;
    [SerializeField] private VictoryMover player4 = null;

    [Header("UI")]
    [SerializeField] private TMP_Text victoryName = null;
    [SerializeField] private GameObject quitButton = null;

    private VictoryScoreSetter score = null;

    private void Awake()
    {
        score = FindObjectOfType<VictoryScoreSetter>();
    }

    private void Start()
    {
        victoryName.text = $"The winner is ...";

        if (quitButton.gameObject.activeSelf)
            quitButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!player1.GetMovingStatus() && !player2.GetMovingStatus() && !player3.GetMovingStatus() && !player4.GetMovingStatus())
            ShowVictoryName();
    }

    private void ShowVictoryName()
    {
        victoryName.text = $"The winner is {score.GetScoreList(0)}!";

        if (!quitButton.gameObject.activeSelf)
            quitButton.gameObject.SetActive(true);
    }
}
