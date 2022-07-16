using System.Collections;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private GameObject animationPanel = null;

    [Header("Delay")]
    [SerializeField] private float delayBeforeAnimation = 1f;

    private Timer timer = null;
    private bool checkAllowed = false;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private void Update()
    {
        //Debug.Log("Delay: " + delay);

        if (!timer.timeLeft && checkAllowed)
        {
            if (delayBeforeAnimation <= 0)
            {
                if (!animationPanel.activeSelf)
                    animationPanel.SetActive(true);
            }
            else
                delayBeforeAnimation -= Time.deltaTime;
        }
    }

    private IEnumerator Delay()
    {
        checkAllowed = false;
        yield return new WaitForSeconds(5f);
        checkAllowed = true;
    }
}
