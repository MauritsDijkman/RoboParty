using UnityEngine;

public class ObjectEnabler : MonoBehaviour
{
    private GameObject[] hoops;
    private GameObject[] birds;

    private void Update()
    {
        hoops = GameObject.FindGameObjectsWithTag("Hoop");
        birds = GameObject.FindGameObjectsWithTag("Bird");

        foreach (GameObject hoop in hoops)
        {
            if (!hoop.activeSelf)
                hoop.SetActive(true);
        }

        foreach (GameObject bird in birds)
        {
            if (!bird.activeSelf)
                bird.SetActive(true);
        }
    }
}
