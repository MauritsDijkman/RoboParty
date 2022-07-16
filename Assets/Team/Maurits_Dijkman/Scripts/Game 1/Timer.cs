using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] public float time = 60f;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText = null;

    [HideInInspector] public bool timeLeft = true;
    private StartTimer startTimer = null;

    private PlaneAmountChecker planeAmountChecker = null;

    private void Awake()
    {
        startTimer = FindObjectOfType<StartTimer>();
        planeAmountChecker = FindObjectOfType<PlaneAmountChecker>();
    }

    private void Update()
    {
        if (!startTimer.inputAllowed)
            return;

        if (planeAmountChecker != null && time < 20f)
            if (planeAmountChecker.GetPlaneAmount() <= 0)
                time = 0;

        UpdateTimer();
        UpdateUI();

        //Debug.Log($"Time: {time} || Time left: {timeLeft}");
    }

    private void UpdateTimer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timeLeft = true;
        }
        else if (time <= 0)
            timeLeft = false;
    }

    private void UpdateUI()
    {
        if (timerText != null)
            timerText.text = time.ToString("F0");
    }
}
