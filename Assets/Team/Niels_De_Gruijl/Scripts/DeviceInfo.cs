using UnityEngine;

public class DeviceInfo : MonoBehaviour
{
    public static DeviceInfo instance = null;

    public static bool isHost;
    public static bool isClient;

    public static int playerIndex;
    public static string playerName;

    public static bool playerIsReady = false;

    private void Awake()
    {
        if (instance = null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
