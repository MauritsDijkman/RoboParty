using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    [Header("Cursor modes")]
    [SerializeField] private bool cursorVisible = false;
    [SerializeField] private CursorLockMode cursorLockMode = CursorLockMode.Locked;

    private void Awake()
    {
        Cursor.visible = cursorVisible;
        Cursor.lockState = cursorLockMode;
    }
}
