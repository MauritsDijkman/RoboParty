using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [System.Serializable]
    public class Tags_Particle
    {
        public string tag = "";
        public GameObject particle = null;
    }

    [SerializeField] private Tags_Particle[] tags_particles;

    private void OnTriggerEnter(Collider other)
    {
        foreach (Tags_Particle tagParticle in tags_particles)
        {
            if (tagParticle.tag == other.tag)
            {
                if (tagParticle.particle != null)
                {
                    GameObject spawnedParticle = (GameObject)Instantiate(tagParticle.particle, transform.position, Quaternion.identity);

                    if (GameObject.Find("Particles") != null)
                        spawnedParticle.transform.parent = GameObject.Find("Particles").transform;

                    float particleDuration = spawnedParticle.GetComponent<ParticleSystem>().duration;

                    Destroy(spawnedParticle, particleDuration);
                    Destroy(gameObject);
                }
                else
                    Destroy(gameObject, 0.5f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (Tags_Particle tagParticle in tags_particles)
        {
            if (tagParticle.tag == collision.gameObject.tag)
            {
                if (tagParticle.particle != null)
                {
                    GameObject spawnedParticle = (GameObject)Instantiate(tagParticle.particle, transform.position, Quaternion.identity);

                    if (GameObject.Find("Particles") != null)
                        spawnedParticle.transform.parent = GameObject.Find("Particles").transform;

                    float particleDuration = spawnedParticle.GetComponent<ParticleSystem>().duration;

                    Destroy(spawnedParticle, particleDuration);
                    Destroy(gameObject);
                }
                else
                    Destroy(gameObject, 0.5f);
            }
        }
    }
}
