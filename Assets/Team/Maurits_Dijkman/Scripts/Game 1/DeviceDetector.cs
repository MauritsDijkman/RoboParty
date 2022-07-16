using UnityEngine;

public class DeviceDetector : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetString("DeviceType", SystemInfo.deviceType.ToString());
        Debug.Log(PlayerPrefs.GetString("DeviceType"));
    }
}
