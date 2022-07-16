using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp_Sound : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gameplay_Sounds/Game_3/Stomp_Hit");
            Debug.Log("KEY");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gameplay_Sounds/Game_3/Stomp_No_Hit");

        }
    }
}
