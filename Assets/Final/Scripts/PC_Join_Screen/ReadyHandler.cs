using UnityEngine;
using Photon.Pun;

public class ReadyHandler : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private GameObject player1 = null;
    [SerializeField] private GameObject player2 = null;
    [SerializeField] private GameObject player3 = null;
    [SerializeField] private GameObject player4 = null;

    [Header("Lights")]
    [SerializeField] private GameObject player1_Light = null;
    [SerializeField] private GameObject player2_Light = null;
    [SerializeField] private GameObject player3_Light = null;
    [SerializeField] private GameObject player4_Light = null;

    [Header("Light colors")]
    [SerializeField] private Color readyColor;
    [SerializeField] private Color notReadyColor;
    [SerializeField] private float intensityValue = 60;

    [Header("Next scene")]
    [SerializeField] string nextScene = "";

    private bool everyoneReady = false;
    private bool loadingNextGame = false;

    private void Awake()
    {
        PlayerPrefs.SetInt("Player1_Ready", 0);
        PlayerPrefs.SetInt("Player2_Ready", 0);
        PlayerPrefs.SetInt("Player3_Ready", 0);
        PlayerPrefs.SetInt("Player4_Ready", 0);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Player1_Ready"))
        {
            if (PlayerPrefs.GetInt("Player1_Ready") == 0)
                ChangeColor(player1_Light, notReadyColor);
            else if (PlayerPrefs.GetInt("Player1_Ready") == 1)
                ChangeColor(player1_Light, readyColor);
        }

        if (PlayerPrefs.HasKey("Player2_Ready"))
        {
            if (PlayerPrefs.GetInt("Player2_Ready") == 0)
                ChangeColor(player2_Light, notReadyColor);
            else if (PlayerPrefs.GetInt("Player2_Ready") == 1)
                ChangeColor(player2_Light, readyColor);
        }

        if (PlayerPrefs.HasKey("Player3_Ready"))
        {
            if (PlayerPrefs.GetInt("Player3_Ready") == 0)
                ChangeColor(player3_Light, notReadyColor);
            else if (PlayerPrefs.GetInt("Player3_Ready") == 1)
                ChangeColor(player3_Light, readyColor);
        }

        if (PlayerPrefs.HasKey("Player4_Ready"))
        {
            if (PlayerPrefs.GetInt("Player4_Ready") == 0)
                ChangeColor(player4_Light, notReadyColor);
            else if (PlayerPrefs.GetInt("Player4_Ready") == 1)
                ChangeColor(player4_Light, readyColor);
        }

        if (PhotonNetwork.PlayerList.Length > 1)
        {
            if (!loadingNextGame && !everyoneReady)
                CheckEveryoneReady();

            if (everyoneReady)
                LoadNextScene();
        }
    }

    private void ChangeColor(GameObject pTarget, Color pNewColor)
    {
        Material materialColor = pTarget.GetComponentInChildren<Renderer>().material;

        materialColor.color = notReadyColor;
        materialColor.SetColor("_EmissionColor", pNewColor * intensityValue);
    }

    void CheckEveryoneReady()
    {
        switch (PhotonNetwork.PlayerList.Length - 1)
        {
            case 1:
                if (PlayerPrefs.GetInt("Player1_Ready") == 1)
                    everyoneReady = true;
                break;

            case 2:
                if (PlayerPrefs.GetInt("Player1_Ready") == 1 && PlayerPrefs.GetInt("Player2_Ready") == 1)
                    everyoneReady = true;
                break;

            case 3:
                if (PlayerPrefs.GetInt("Player1_Ready") == 1 && PlayerPrefs.GetInt("Player2_Ready") == 1 &&
                    PlayerPrefs.GetInt("Player3_Ready") == 1)
                    everyoneReady = true;
                break;

            case 4:
                if (PlayerPrefs.GetInt("Player1_Ready") == 1 && PlayerPrefs.GetInt("Player2_Ready") == 1 &&
                    PlayerPrefs.GetInt("Player3_Ready") == 1 && PlayerPrefs.GetInt("Player4_Ready") == 1)
                    everyoneReady = true;
                break;
        }
    }

    void LoadNextScene()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(nextScene);

        loadingNextGame = true;
        everyoneReady = false;
    }
}
