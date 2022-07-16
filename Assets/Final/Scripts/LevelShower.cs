using UnityEngine;

public class LevelShower : MonoBehaviour
{
    [Header("Host")]
    [SerializeField] private GameObject[] gameObjects_TurnedOn_Host;
    [SerializeField] private GameObject[] gameObjects_TurnedOff_Host;

    [Header("Client")]
    [SerializeField] private GameObject[] gameObjects_TurnedOn_Client;
    [SerializeField] private GameObject[] gameObjects_TurnedOff_Client;

    private void Start()
    {
        HandleHost();
        HandleClient();
    }

    private void HandleHost()
    {
        if (DeviceInfo.isHost)
        {
            foreach (GameObject pObject in gameObjects_TurnedOn_Host)
            {
                if (pObject != null)
                {
                    if (!pObject.activeSelf)
                        pObject.SetActive(true);
                }
            }

            foreach (GameObject pObject in gameObjects_TurnedOff_Host)
            {
                if (pObject != null)
                {
                    if (pObject.activeSelf)
                        pObject.SetActive(false);
                }
            }
        }
    }

    private void HandleClient()
    {
        if (DeviceInfo.isClient)
        {

            foreach (GameObject pObject in gameObjects_TurnedOn_Client)
            {
                if (pObject != null)
                {
                    if (!pObject.activeSelf)
                        pObject.SetActive(true);
                }
            }

            foreach (GameObject pObject in gameObjects_TurnedOff_Client)
            {
                if (pObject != null)
                {
                    if (pObject.activeSelf)
                        pObject.SetActive(false);
                }
            }
        }
    }
}
