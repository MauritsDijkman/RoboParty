using UnityEngine;

public class ForcePusher : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float force = 3f;

    private Rigidbody rig = null;

    private void Awake()
    {
        rig = GetComponentInParent<Rigidbody>();

        //if (rig == null)
        //    Debug.Log("Rigibody not found (is null)");
        //else if (rig != null)
        //    Debug.Log("Rigibody found (not null)");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision entered!" + " || Tag: " + collision.gameObject.tag);

        // If the object we hit is the enemy
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = collision.contacts[0].point - transform.position;

            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;

            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            rig.AddForce(dir * force);

            //Debug.Log("Force is applied");
        }
    }
}
