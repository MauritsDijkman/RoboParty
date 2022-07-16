using UnityEngine;

public class LevelSetter : MonoBehaviour
{
    [SerializeField] private int level = 1;

    private void Start()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
    }
}
