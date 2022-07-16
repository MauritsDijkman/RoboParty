using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class movement : MonoBehaviour
{
    PhotonView pv;

    private void Start()
    {
        pv = gameObject.GetComponent<PhotonView>();

        if (!pv.IsMine)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.localPosition += Vector3.left * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.localPosition += Vector3.right * Time.deltaTime;
            }
        }
    }
}
