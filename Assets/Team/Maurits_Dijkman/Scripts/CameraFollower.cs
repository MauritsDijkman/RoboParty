using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Player targets")]
    [SerializeField] private Transform player_1_Target = null;
    [SerializeField] private Transform player_2_Target = null;
    [SerializeField] private Transform player_3_Target = null;
    [SerializeField] private Transform player_4_Target = null;

    [Header("Offset")]
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);

    private Transform followTarget = null;

    private List<PlayerInfo> list = new List<PlayerInfo>();

    private void Start()
    {
        PlayerInfo player1 = new PlayerInfo();
        player1.followTarget = player_1_Target;
        player1.score = PlayerPrefs.GetInt("Player_1_TotalScore");

        PlayerInfo player2 = new PlayerInfo();
        player2.followTarget = player_2_Target;
        player2.score = PlayerPrefs.GetInt("Player_2_TotalScore");

        PlayerInfo player3 = new PlayerInfo();
        player3.followTarget = player_3_Target;
        player3.score = PlayerPrefs.GetInt("Player_3_TotalScore");

        PlayerInfo player4 = new PlayerInfo();
        player4.followTarget = player_4_Target;
        player4.score = PlayerPrefs.GetInt("Player_4_TotalScore");

        list.Add(player1);
        list.Add(player2);
        list.Add(player3);
        list.Add(player4);

        SortList();

        followTarget = list[0].followTarget;
    }

    private void SortList()
    {
        //list.Sort((x, y) => (x.score < y.score) ? -1 : 1);    // Smallest first
        list.Sort((x, y) => (x.score > y.score) ? -1 : 1);      // Biggest first
    }

    private void Update()
    {
        transform.position = new Vector3(offset.x, followTarget.position.y + offset.y, offset.z);
    }

    class PlayerInfo
    {
        public Transform followTarget;
        public int score;
    }
}
