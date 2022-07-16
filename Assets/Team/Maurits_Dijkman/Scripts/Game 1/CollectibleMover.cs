using UnityEngine;

public class CollectibleMover : MonoBehaviour
{
    [Header("Velocity")]
    [SerializeField] private float maxSpeed = 3f;

    private Rigidbody rig;
    private bool hitSomething = false;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!hitSomething)
        {
            if (rig.velocity.y < -maxSpeed)
                rig.velocity = new Vector3(rig.velocity.x, -maxSpeed, rig.velocity.z);
        }

        //Debug.Log(rig.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bucket") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
            hitSomething = true;
    }
}
