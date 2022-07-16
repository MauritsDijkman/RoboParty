using UnityEngine;

public class PlaneAmountChecker : MonoBehaviour
{
    private GameObject[] players;

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public int GetPlaneAmount()
    {
        return players.Length;
    }
}
