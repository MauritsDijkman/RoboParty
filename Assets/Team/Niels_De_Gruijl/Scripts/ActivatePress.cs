using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ActivatePress : MonoBehaviourPun
{
    [Header("Input type")]
    [SerializeField] bool mobile;
    [SerializeField] bool keyboard;

    [SerializeField] GameObject ingot;

    Rigidbody rb;

    private float downForce = -5;

    bool canPress = false;

    Vector3 originalPos;
    Vector3 ingotPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = transform.localPosition;

        if (originalPos.y < 0)
            canPress = true;

        Input.gyro.enabled = true;
    }

    private void Update()
    {
/*        if(originalPos.y <= 0)
        {
            if (transform.localPosition.y <= originalPos.y && !canPress)
            {
                transform.localPosition = originalPos;
                rb.velocity = Vector3.zero;
                canPress = true;
            }
        }*/
/*        else
        {

        }*/

        if (transform.localPosition.y >= originalPos.y && !canPress)
        {
            transform.localPosition = originalPos;
            rb.velocity = Vector3.zero;
            canPress = true;
        }

        Debug.Log("Original pos: " + originalPos);
        Debug.Log("Local pos: " + transform.localPosition);
        //Debug.Log(canPress);

        if (canPress)
        {
            if (keyboard)
                KeyboardInput();
            else if (mobile)
                MobileInput();
            else
                Debug.Log("Error, input type not set");
        }

        //Debug.Log(Input.gyro.userAcceleration.y);
    }

    void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MovePress();
            canPress = false;
        }
    }

    void MobileInput()
    {
        if(Input.gyro.userAcceleration.y >= 1)
        {
            Debug.Log("PRESS!");
            //photonView.RPC("MovePress", RpcTarget.All);
            MovePress();
            canPress = false;
        }
    }

    void MovePress()
    {
        if (photonView.IsMine)
        {
            rb.velocity = new Vector3(0, downForce, 0);
        }
    }

    void OnCollisionEnter()
    {
        if (photonView.IsMine)
        {
            rb.velocity = new Vector3(0, -downForce, 0);
        }
    }

/*    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectible_Bomb")
        {

            Destroy(other.gameObject);
        }

        ingotPos = other.gameObject.transform.position;
        GameObject ingotObject = PhotonNetwork.Instantiate(ingot.name, ingotPos, Quaternion.identity);
        ingotObject.GetComponent<ConveyorObjectScript>().dir = other.GetComponent<ConveyorObjectScript>().dir;
        Destroy(other.gameObject);
    }*/
}
