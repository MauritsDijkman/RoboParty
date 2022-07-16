using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShakeInput : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 movement;

    float timer = 1;
    bool coroutineStopped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Input.gyro.enabled = true;
    }

    private void Update()
    {
        if(Input.gyro.userAcceleration.y >= 2 && !coroutineStopped)
        {
            rb.velocity = new Vector3(0, -5, 0);
            StartCoroutine(movementTimer());
        }
        
        Debug.Log(rb.velocity);
    }

    IEnumerator movementTimer()
    {
        yield return new WaitForSeconds(timer);
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, 5, 0);
    }
}
