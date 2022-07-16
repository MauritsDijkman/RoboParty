using System.Collections.Generic;
using UnityEngine;

public class VictoryScoreSetter : MonoBehaviour
{
    //[Header("Score")]
    //[SerializeField] private int scorePlayer1 = 0;
    //[SerializeField] private int scorePlayer2 = 0;
    //[SerializeField] private int scorePlayer3 = 0;
    //[SerializeField] private int scorePlayer4 = 0;

    [Header("Speed")]
    [SerializeField] private float movementSpeed = 5;

    [Header("Particle")]
    [SerializeField] private GameObject confettiParticle = null;

    private VictoryMover player1 = null;
    private VictoryMover player2 = null;
    private VictoryMover player3 = null;
    private VictoryMover player4 = null;

    private List<PlayerInfo> scoreList = new List<PlayerInfo>();

    private void Awake()
    {
        player1 = GameObject.Find("Player1").GetComponent<VictoryMover>();
        player2 = GameObject.Find("Player2").GetComponent<VictoryMover>();
        player3 = GameObject.Find("Player3").GetComponent<VictoryMover>();
        player4 = GameObject.Find("Player4").GetComponent<VictoryMover>();
    }

    private void Start()
    {
        //PlayerPrefs.SetInt("Player1_Score_Level1", 45);
        //PlayerPrefs.SetInt("Player2_Score_Level1", 23);
        //PlayerPrefs.SetInt("Player3_Score_Level1", 21);
        //PlayerPrefs.SetInt("Player4_Score_Level1", 12);

        //PlayerPrefs.Save();


        // Removing old total scores
        if (PlayerPrefs.HasKey("Player_1_TotalScore"))
            PlayerPrefs.DeleteKey("Player_1_TotalScore");
        if (PlayerPrefs.HasKey("Player_2_TotalScore"))
            PlayerPrefs.DeleteKey("Player_2_TotalScore");
        if (PlayerPrefs.HasKey("Player_3_TotalScore"))
            PlayerPrefs.DeleteKey("Player_3_TotalScore");
        if (PlayerPrefs.HasKey("Player_4_TotalScore"))
            PlayerPrefs.DeleteKey("Player_4_TotalScore");

        PlayerPrefs.Save();


        // Setting new total scores
        PlayerPrefs.SetInt("Player_1_TotalScore", PlayerPrefs.GetInt("Player1_Score_Level1") + PlayerPrefs.GetInt("Player1_Score_Level2") + PlayerPrefs.GetInt("Player1_Score_Level3"));
        PlayerPrefs.SetInt("Player_2_TotalScore", PlayerPrefs.GetInt("Player2_Score_Level1") + PlayerPrefs.GetInt("Player2_Score_Level2") + PlayerPrefs.GetInt("Player2_Score_Level3"));
        PlayerPrefs.SetInt("Player_3_TotalScore", PlayerPrefs.GetInt("Player3_Score_Level1") + PlayerPrefs.GetInt("Player3_Score_Level2") + PlayerPrefs.GetInt("Player3_Score_Level3"));
        PlayerPrefs.SetInt("Player_4_TotalScore", PlayerPrefs.GetInt("Player4_Score_Level1") + PlayerPrefs.GetInt("Player4_Score_Level2") + PlayerPrefs.GetInt("Player4_Score_Level3"));

        PlayerPrefs.Save();

        // Setting names
        PlayerInfo Player1 = new PlayerInfo();
        Player1.name = PlayerPrefs.GetString("Player1_Name");
        Player1.score = PlayerPrefs.GetInt("Player_1_TotalScore");

        PlayerInfo Player2 = new PlayerInfo();
        Player2.name = PlayerPrefs.GetString("Player2_Name");
        Player2.score = PlayerPrefs.GetInt("Player_2_TotalScore");


        PlayerInfo Player3 = new PlayerInfo();
        Player3.name = PlayerPrefs.GetString("Player3_Name");
        Player3.score = PlayerPrefs.GetInt("Player_3_TotalScore");

        PlayerInfo Player4 = new PlayerInfo();
        Player4.name = PlayerPrefs.GetString("Player4_Name");
        Player4.score = PlayerPrefs.GetInt("Player_4_TotalScore");

        scoreList.Add(Player1);
        scoreList.Add(Player2);
        scoreList.Add(Player3);
        scoreList.Add(Player4);


        // Sorting list on size (highest is [0]
        SortList();

        //Debug.Log($"Biggest score: {scoreList[0].score} & Name: {scoreList[0].name} || Smallest score: {scoreList[3].score} & Name: {scoreList[3].name}");

        // Setting the stats for the player score visualizers
        player1.SetStats(PlayerPrefs.GetInt("Player_1_TotalScore"), movementSpeed);
        player2.SetStats(PlayerPrefs.GetInt("Player_2_TotalScore"), movementSpeed);
        player3.SetStats(PlayerPrefs.GetInt("Player_3_TotalScore"), movementSpeed);
        player4.SetStats(PlayerPrefs.GetInt("Player_4_TotalScore"), movementSpeed);

        //Debug.Log($"Player 1 score: {PlayerPrefs.GetInt("Player_1_TotalScore")} || Player 2 score: {PlayerPrefs.GetInt("Player_2_TotalScore")} || Player 3 score: {PlayerPrefs.GetInt("Player_3_TotalScore")} || Player 4 score: {PlayerPrefs.GetInt("Player_4_TotalScore")}");
    }

    private void SortList()
    {
        //list.Sort((x, y) => (x.score < y.score) ? -1 : 1);    // Smallest first
        scoreList.Sort((x, y) => (x.score > y.score) ? -1 : 1);      // Biggest first
    }

    public string GetScoreList(int pIndex)
    {
        return scoreList[pIndex].name;
    }

    public GameObject GetParticle()
    {
        return confettiParticle;
    }
}

public class PlayerInfo
{
    public string name;
    public int score;
}
