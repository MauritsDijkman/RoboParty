using UnityEngine;
using TMPro;

public class PlayerNameViewer : MonoBehaviour
{
    [Header("Name UI")]
    [SerializeField] private TMP_Text player1_Name;
    [SerializeField] private TMP_Text player2_Name;
    [SerializeField] private TMP_Text player3_Name;
    [SerializeField] private TMP_Text player4_Name;

    private void Start()
    {
        SetName(1, player1_Name);
        SetName(2, player2_Name);
        SetName(3, player3_Name);
        SetName(4, player4_Name);
    }

    private void SetName(int playerIndex, TMP_Text player)
    {
        if (PlayerPrefs.HasKey($"Player{playerIndex}_Name"))
            player.text = (PlayerPrefs.GetString($"Player{playerIndex}_Name"));
        else
            player.text = $"Player{playerIndex}";
    }
}
