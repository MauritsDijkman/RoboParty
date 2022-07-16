using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighscoreTable : MonoBehaviour
{
    [System.Serializable]
    private class HighscoreEntry
    {
        public string name;
        public int score;
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = GameObject.Find("HighscoreEntryContainer").transform;
        entryTemplate = GameObject.Find("HighscoreEntryTemplate").transform;

        entryTemplate.gameObject.SetActive(false);

        //AddHighScoreEntry(PlayerPrefs.GetInt("Player1Score"), "Player1");
        //AddHighScoreEntry(PlayerPrefs.GetInt("Player2Score"), "Player2");
        //AddHighScoreEntry(PlayerPrefs.GetInt("Player3Score"), "Player3");
        //AddHighScoreEntry(PlayerPrefs.GetInt("Player4Score"), "Player4");
        AddHighScoreEntry(5, "Test");

        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 100f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default: rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3ND"; break;
            case 4: rankString = "4ND"; break;
        }

        entryTransform.Find("PosText").GetComponent<TMP_Text>().text = rankString;

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TMP_Text>().text = name;

        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<TMP_Text>().text = score.ToString();

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int score, string name)
    {
        //Debug.Log($"Given name: {name} || Given score: {score}");

        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = "";

        if (!PlayerPrefs.HasKey("HighscoreTable"))
            PlayerPrefs.SetString("HighscoreTable", jsonString);

        jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        Debug.Log("Highscores created");

        if (highscores == null)
            Debug.Log("Highscore is null!");

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighscoreTable", json);
        PlayerPrefs.Save();
    }
}
