using UnityEngine;

public class PlaneRotator : MonoBehaviour
{
    private Rigidbody rig = null;

    [SerializeField] private Transform upTarget;
    [SerializeField] private Transform downTarget;
    [SerializeField] private float turnSpeed = 1.0f;

    private StartTimer startTimer = null;
    private Timer timer = null;


    private void Awake()
    {
        rig = GetComponentInParent<Rigidbody>();

        startTimer = FindObjectOfType<StartTimer>();
        timer = FindObjectOfType<Timer>();
    }

    private void Update()
    {
        if (!startTimer.inputAllowed || !timer.timeLeft)
            return;

        if (rig.velocity.y > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, upTarget.rotation, turnSpeed * Time.deltaTime);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, downTarget.rotation, turnSpeed * Time.deltaTime);
    }
}
