using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSorter : MonoBehaviour
{
    List<PlayerInfo> list = new List<PlayerInfo>();

    [SerializeField] private TMP_Text[] playerNames;
    [SerializeField] private TMP_Text[] playerScores;

    private string levelName;

    private void Start()
    {
        //AddStats();

        if (PlayerPrefs.GetInt("Level") == 1)
            levelName = "_Level1";
        else if (PlayerPrefs.GetInt("Level") == 2)
            levelName = "_Level2";
        else if (PlayerPrefs.GetInt("Level") == 3)
            levelName = "_Level3";

        PlayerInfo Player1 = new PlayerInfo();
        Player1.name = PlayerPrefs.GetString("Player1_Name");
        Player1.score = PlayerPrefs.GetInt("Player1_Score" + levelName);

        PlayerInfo Player2 = new PlayerInfo();
        Player2.name = PlayerPrefs.GetString("Player2_Name");
        Player2.score = PlayerPrefs.GetInt("Player2_Score" + levelName);

        PlayerInfo Player3 = new PlayerInfo();
        Player3.name = PlayerPrefs.GetString("Player3_Name");
        Player3.score = PlayerPrefs.GetInt("Player3_Score" + levelName);

        PlayerInfo Player4 = new PlayerInfo();
        Player4.name = PlayerPrefs.GetString("Player4_Name");
        Player4.score = PlayerPrefs.GetInt("Player4_Score" + levelName);

        list.Add(Player1);
        list.Add(Player2);
        list.Add(Player3);
        list.Add(Player4);

        SortList();
        UpdateUI();
    }

    private void AddStats()
    {
        PlayerPrefs.SetString("Player1_Name", "Jeroen Vedder");
        PlayerPrefs.SetString("Player2_Name", "Anne-Maij Franke");
        PlayerPrefs.SetString("Player3_Name", "Niels de Gruijl");
        PlayerPrefs.SetString("Player4_Name", "Maurits Dijkman");

        PlayerPrefs.Save();
    }

    private void SortList()
    {
        //list.Sort((x, y) => (x.score < y.score) ? -1 : 1);    // Smallest first
        list.Sort((x, y) => (x.score > y.score) ? -1 : 1);      // Biggest first
    }

    private void UpdateUI()
    {
        for (int i = 0; i < playerNames.Length; i++)
        {
            playerNames[i].text = list[i].name;
        }

        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i].text = list[i].score.ToString();
        }
    }

    class PlayerInfo
    {
        public string name;
        public int score;
    }
}
