using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public Vector3 inputAcceleration;
    [HideInInspector] public static InputManager instance;

    private Vector3 adjustment;

    private void Awake()
    {
        instance = this;
        adjustment = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        inputAcceleration = Input.acceleration;
        inputAcceleration += adjustment;

        if (inputAcceleration.sqrMagnitude > 1)
            inputAcceleration.Normalize();

        //Debug.Log($" Accelerometer normal: {Input.acceleration} || Accelerometer adjusted: {inputAcceleration} || Adjustment: {adjustment}");
    }

    public void SetAcceleration()
    {
        Vector3 localAcceleration = Input.acceleration;

        adjustment = new Vector3(0, 0, 0);

        if (localAcceleration.x > 0)
        {
            //Debug.Log("x is bigger than 0");
            adjustment.x -= localAcceleration.x;
        }
        else if (localAcceleration.x < 0)
        {
            //Debug.Log("x is les than 0");
            adjustment.x += localAcceleration.x * -1;
        }

        if (localAcceleration.y > 0)
        {
            //Debug.Log("y is bigger than 0");
            adjustment.y -= localAcceleration.y;
        }
        else if (localAcceleration.y < 0)
        {
            //Debug.Log("y is less than 0");
            adjustment.y += localAcceleration.y * -1;
        }

        if (localAcceleration.z > 0)
        {
            //Debug.Log("z is bigger than 0");
            adjustment.z -= localAcceleration.z;
        }
        else if (localAcceleration.z < 0)
        {
            //Debug.Log("z is less than 0");
            adjustment.z += localAcceleration.z * -1;
        }
    }
}
