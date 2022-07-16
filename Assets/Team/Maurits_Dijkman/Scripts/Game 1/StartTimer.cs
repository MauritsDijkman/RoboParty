using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private GameObject animationObject = null;

    [Header("Position")]
    [SerializeField] private Vector3 spawnPosition = new Vector3(0f, 0f, 0f);

    [HideInInspector] public bool inputAllowed = false;
    private float originalAnimationTime = 0f;
    private GameObject spawnedAnimation = null;

    private void Start()
    {
        inputAllowed = false;

        spawnedAnimation = (GameObject)Instantiate(animationObject, spawnPosition, animationObject.transform.rotation);
        {
            // Checks the length of the animation
            if (spawnedAnimation.GetComponentInChildren<Animator>().runtimeAnimatorController.animationClips != null)
            {
                AnimationClip[] animation = spawnedAnimation.GetComponentInChildren<Animator>().runtimeAnimatorController.animationClips;
                float animationTime = animation[0].length;
                //Debug.Log($"Length of animation : {animationTime}");
                Destroy(spawnedAnimation, animationTime);

                originalAnimationTime = animationTime;
            }
        }
    }

    private void Update()
    {
        if (originalAnimationTime > 0)
            originalAnimationTime -= Time.deltaTime;

        if (originalAnimationTime <= 0)
            inputAllowed = true;
    }
}
