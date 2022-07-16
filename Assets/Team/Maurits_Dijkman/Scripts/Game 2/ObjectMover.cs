using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Vector3 newVelocity = new Vector3(-5, 0, 0);

    private Rigidbody rig = null;

    private StartTimer startTimer = null;
    private Timer timer = null;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

        startTimer = FindObjectOfType<StartTimer>();
        timer = FindObjectOfType<Timer>();
    }

    private void FixedUpdate()
    {
        //if (!startTimer.inputAllowed || !timer.timeLeft)
        //    return;

        rig.velocity = newVelocity;
    }

    public void SetVelocity(Vector3 pNewVelocity)
    {
        newVelocity = pNewVelocity;
    }

    public Vector3 GetVelocity()
    {
        return newVelocity;
    }
}
