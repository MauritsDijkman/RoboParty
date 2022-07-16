using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameLobby : MonoBehaviourPun
{
    [Header("Empty player podiums")]
    [SerializeField] private GameObject empty_p1;
    [SerializeField] private GameObject empty_p2;
    [SerializeField] private GameObject empty_p3;
    [SerializeField] private GameObject empty_p4;

    [Header("Player podiums")]
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] private GameObject p3;
    [SerializeField] private GameObject p4;

    [Header("Player nicknames")]
    [SerializeField] private TextMeshProUGUI p1Name;
    [SerializeField] private TextMeshProUGUI p2Name;
    [SerializeField] private TextMeshProUGUI p3Name;
    [SerializeField] private TextMeshProUGUI p4Name;

    //[Header("Next scene")]
    //[SerializeField] string nextScene = "";

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Player1_Name"))
            PlayerPrefs.DeleteKey("Player1_Name");
        if (PlayerPrefs.HasKey("Player2_Name"))
            PlayerPrefs.DeleteKey("Player2_Name");
        if (PlayerPrefs.HasKey("Player3_Name"))
            PlayerPrefs.DeleteKey("Player3_Name");
        if (PlayerPrefs.HasKey("Player4_Name"))
            PlayerPrefs.DeleteKey("Player4_Name");

        if (PlayerPrefs.HasKey("Player1_Score_Level1"))
            PlayerPrefs.DeleteKey("Player1_Score_Level1");
        if (PlayerPrefs.HasKey("Player1_Score_Level2"))
            PlayerPrefs.DeleteKey("Player1_Score_Level2");
        if (PlayerPrefs.HasKey("Player1_Score_Level3"))
            PlayerPrefs.DeleteKey("Player1_Score_Level3");

        if (PlayerPrefs.HasKey("Player2_Score_Level1"))
            PlayerPrefs.DeleteKey("Player2_Score_Level1");
        if (PlayerPrefs.HasKey("Player2_Score_Level2"))
            PlayerPrefs.DeleteKey("Player2_Score_Level2");
        if (PlayerPrefs.HasKey("Player2_Score_Level3"))
            PlayerPrefs.DeleteKey("Player2_Score_Level3");

        if (PlayerPrefs.HasKey("Player3_Score_Level1"))
            PlayerPrefs.DeleteKey("Player3_Score_Level1");
        if (PlayerPrefs.HasKey("Player3_Score_Level2"))
            PlayerPrefs.DeleteKey("Player3_Score_Level2");
        if (PlayerPrefs.HasKey("Player3_Score_Level3"))
            PlayerPrefs.DeleteKey("Player3_Score_Level3");

        if (PlayerPrefs.HasKey("Player4_Score_Level1"))
            PlayerPrefs.DeleteKey("Player4_Score_Level1");
        if (PlayerPrefs.HasKey("Player4_Score_Level2"))
            PlayerPrefs.DeleteKey("Player4_Score_Level2");
        if (PlayerPrefs.HasKey("Player4_Score_Level3"))
            PlayerPrefs.DeleteKey("Player4_Score_Level3");

        PlayerPrefs.Save();
    }

    private void Start()
    {
        SetPlayerIndex();
        photonView.RPC("SetPodiumsActive", RpcTarget.AllBuffered);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    [PunRPC]
    void SetPodiumsActive()
    {
        switch (PhotonNetwork.PlayerList.Length - 2)
        {
            case 0:
                empty_p1.SetActive(false);

                p1.SetActive(true);
                //p1Light.gameObject.SetActive(true);

                if (PlayerPrefs.HasKey("Player1_Name") && PlayerPrefs.GetString("Player1_Name") != "")
                    p1Name.text = PlayerPrefs.GetString("Player1_Name");
                else
                {
                    PlayerPrefs.SetString("Player1_Name", "Player1");
                    PlayerPrefs.Save();
                    p1Name.text = PlayerPrefs.GetString("Player1_Name");
                }

                break;

            case 1:
                empty_p1.SetActive(false);
                empty_p2.SetActive(false);

                p1.SetActive(true);
                p2.SetActive(true);

                if (PlayerPrefs.HasKey("Player2_Name") && PlayerPrefs.GetString("Player2_Name") != "")
                    p2Name.text = PlayerPrefs.GetString("Player2_Name");
                else
                {
                    PlayerPrefs.SetString("Player2_Name", "Player2");
                    PlayerPrefs.Save();
                    p2Name.text = PlayerPrefs.GetString("Player2_Name");
                }

                break;

            case 2:
                empty_p1.SetActive(false);
                empty_p2.SetActive(false);
                empty_p3.SetActive(false);

                p1.SetActive(true);
                p2.SetActive(true);
                p3.SetActive(true);

                if (PlayerPrefs.HasKey("Player3_Name") && PlayerPrefs.GetString("Player3_Name") != "")
                    p3Name.text = PlayerPrefs.GetString("Player3_Name");
                else
                {
                    PlayerPrefs.SetString("Player3_Name", "Player3");
                    PlayerPrefs.Save();
                    p3Name.text = PlayerPrefs.GetString("Player3_Name");
                }

                break;

            case 3:
                empty_p1.SetActive(false);
                empty_p2.SetActive(false);
                empty_p3.SetActive(false);
                empty_p4.SetActive(false);

                p1.SetActive(true);
                p2.SetActive(true);
                p3.SetActive(true);
                p4.SetActive(true);

                if (PlayerPrefs.HasKey("Player4_Name") && PlayerPrefs.GetString("Player4_Name") != "")
                    p4Name.text = PlayerPrefs.GetString("Player4_Name");
                else
                {
                    PlayerPrefs.SetString("Player4_Name", "Player4");
                    PlayerPrefs.Save();
                    p4Name.text = PlayerPrefs.GetString("Player4_Name");
                }

                break;

            default:
                Debug.Log($"Lobby is just created or full!");
                break;
        }
    }

    void SetPlayerIndex()
    {
        switch (PhotonNetwork.PlayerList.Length - 2)
        {
            case 0:
                DeviceInfo.playerIndex = 1;
                break;

            case 1:
                DeviceInfo.playerIndex = 2;
                break;

            case 2:
                DeviceInfo.playerIndex = 3;
                break;

            case 3:
                DeviceInfo.playerIndex = 4;
                break;

            default:
                Debug.Log($"Lobby is just created or full!");
                break;
        }
    }

    //public void StartGame()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //        PhotonNetwork.LoadLevel(nextScene);
    //}
}
