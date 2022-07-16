using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int targetFPS = 60;

    private void Start()
    {
        Application.targetFrameRate = targetFPS;
    }
}
