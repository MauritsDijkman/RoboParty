using Photon.Pun;
using TMPro;
using UnityEngine;

public class LoadingScreenHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text startText = null;
    [SerializeField] private GameObject[] backgroundFinished = null;

    private int showLevel = 0;
    private string nextLevelName = "";
    private bool allReady = false;

    private bool player1Ready;
    private bool player2Ready;
    private bool player3Ready;
    private bool player4Ready;

    private bool loadingNextGame = false;

    private void Start()
    {
        //PlayerPrefs.SetInt("Level", 3);
        //PlayerPrefs.Save();

        PlayerPrefs.SetInt("Player1_Ready", 0);
        PlayerPrefs.SetInt("Player2_Ready", 0);
        PlayerPrefs.SetInt("Player3_Ready", 0);
        PlayerPrefs.SetInt("Player4_Ready", 0);

        showLevel = PlayerPrefs.GetInt("Level");

        for (int i = 0; i < backgroundFinished.Length; i++)
        {
            backgroundFinished[i].gameObject.SetActive(false);
        }

        if (showLevel == 0)
        {
            backgroundFinished[0].SetActive(true);
            nextLevelName = "Game_1";
        }
        else if (showLevel == 1)
        {
            backgroundFinished[1].SetActive(true);
            nextLevelName = "Game_2";
        }
        else if (showLevel == 2)
        {
            backgroundFinished[2].SetActive(true);
            nextLevelName = "Game_3";
        }
        else if (showLevel == 3)
        {
            backgroundFinished[3].SetActive(true);
            nextLevelName = "VictoryScene";
        }

        startText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Player1_Ready"))
        {
            if (PlayerPrefs.GetInt("Player1_Ready") == 0)
                player1Ready = false;
            else if (PlayerPrefs.GetInt("Player1_Ready") == 1)
                player1Ready = true;
        }

        if (PlayerPrefs.HasKey("Player2_Ready"))
        {
            if (PlayerPrefs.GetInt("Player2_Ready") == 0)
                player2Ready = false;
            else if (PlayerPrefs.GetInt("Player2_Ready") == 1)
                player2Ready = true;
        }

        if (PlayerPrefs.HasKey("Player3_Ready"))
        {
            if (PlayerPrefs.GetInt("Player3_Ready") == 0)
                player3Ready = false;
            else if (PlayerPrefs.GetInt("Player3_Ready") == 1)
                player3Ready = true;
        }

        if (PlayerPrefs.HasKey("Player4_Ready"))
        {
            if (PlayerPrefs.GetInt("Player4_Ready") == 0)
                player4Ready = false;
            else if (PlayerPrefs.GetInt("Player4_Ready") == 1)
                player4Ready = true;
        }

        if (PhotonNetwork.PlayerList.Length > 1)
        {
            if (!loadingNextGame && !allReady)
                CheckReady();

            if (allReady)
                LoadNextGame(nextLevelName);
        }
    }

    private void CheckReady()
    {
        switch (PhotonNetwork.PlayerList.Length - 1)
        {
            case 1:
                if (PlayerPrefs.GetInt("Player1_Ready") == 1)
                    allReady = true;
                break;

            case 2:
                if (PlayerPrefs.GetInt("Player1_Ready") == 1 && PlayerPrefs.GetInt("Player2_Ready") == 1)
                    allReady = true;
                break;

            case 3:
                if (PlayerPrefs.GetInt("Player1_Ready") == 1 && PlayerPrefs.GetInt("Player2_Ready") == 1 &&
                    PlayerPrefs.GetInt("Player3_Ready") == 1)
                    allReady = true;
                break;

            case 4:
                if (PlayerPrefs.GetInt("Player1_Ready") == 1 && PlayerPrefs.GetInt("Player2_Ready") == 1 &&
                    PlayerPrefs.GetInt("Player3_Ready") == 1 && PlayerPrefs.GetInt("Player4_Ready") == 1)
                    allReady = true;
                break;
        }
    }

    private void LoadNextGame(string pSceneName)
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(pSceneName);

        loadingNextGame = true;
        allReady = false;
    }
}
