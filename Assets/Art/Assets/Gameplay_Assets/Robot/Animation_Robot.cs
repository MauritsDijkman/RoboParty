using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Robot : MonoBehaviour
{
    public Animator AnimCtrl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("g"))
        {
            AnimCtrl.SetBool("Carrying Idle", true);
            AnimCtrl.SetBool("Carrying Walking", false);
            Debug.Log("G");
        };

        if (Input.GetKey("h"))
        {
            AnimCtrl.SetBool("Carrying Idle", false);
            AnimCtrl.SetBool("Carrying Running", false);
            AnimCtrl.SetBool("Carrying Walking", true);
            Debug.Log("h");
        };

        if (Input.GetKey("j"))
        {
            AnimCtrl.SetBool("Carrying Idle", false);
            AnimCtrl.SetBool("Carrying Walking", false);
            AnimCtrl.SetBool("Carrying Running", true);
            Debug.Log("j");
        };
    }
}
