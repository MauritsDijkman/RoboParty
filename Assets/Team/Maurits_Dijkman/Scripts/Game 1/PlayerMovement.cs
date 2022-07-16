using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    [Header("Keyboard movement")]
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;
    [SerializeField] private KeyCode jump = KeyCode.W;
    [SerializeField] private float keyboardMovementspeed = 5;
    [SerializeField] private float jumpVelocity = 10f;

    [Header("Mobile movement")]
    [SerializeField] private float sensitivity = 0.05f;
    [SerializeField] private float maxSpeed = 2.5f;
    [SerializeField] private float maxJumpSpeed;
    private float movementSpeed = 20;

    [Header("Input Type")]
    [SerializeField] private bool mobile = true;
    [SerializeField] private bool keyboard = false;

    private Transform player = null;
    private Rigidbody playerRigidbody;
    private Animator AnimCtrl = null;

    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;

    private bool isGrounded;

    private StartTimer startTimer = null;
    private Timer timer = null;

    bool isJumping;

    private void Awake()
    {
        player = gameObject.transform;
        playerRigidbody = player.GetComponent<Rigidbody>();
        AnimCtrl = GetComponentInChildren<Animator>();

        startTimer = FindObjectOfType<StartTimer>();
        timer = FindObjectOfType<Timer>();

/*        if (!photonView.IsMine || DeviceInfo.isHost)
            UI.SetActive(false);*/

        Input.gyro.enabled = true;
    }

    private void Start()
    {
        SetIdle();
    }

    private void Update()
    {
        if (!startTimer.inputAllowed || !timer.timeLeft)
        {
            SetIdle();
            return;
        }
        
        if (keyboard)
            HandleKeyboardInput();
        else if (mobile)
            HandleMobileInput();
        
        //CheckJump();
    }

    private void CheckJump()
    {
        if (photonView.IsMine)
        {
            if (playerRigidbody.velocity.y < 0)
            {
                //Debug.Log("First argument");
                playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (playerRigidbody.velocity.y > 0 && !Input.GetKeyDown(jump) && !isJumping)
            {
                //Debug.Log("Second argument");
                playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    private void HandleKeyboardInput()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(right))
            {
                //Debug.Log("Moving right");
                //playerRigibody.velocity = new Vector3(keyboardMovementspeed, 0f, 0f);
                playerRigidbody.velocity = new Vector3(keyboardMovementspeed, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
                SetRunning();
            }
            else if (Input.GetKey(left))
            {
                //Debug.Log("Moving left");
                //playerRigibody.velocity = new Vector3(-keyboardMovementspeed, 0f, 0f);
                playerRigidbody.velocity = new Vector3(-keyboardMovementspeed, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
                SetRunning();
            }
            else
            {
                playerRigidbody.velocity = new Vector3(0f, playerRigidbody.velocity.y, 0f);
                SetIdle();
            }

            //Debug.Log("IsGrounded: " + isGrounded);

            if (Input.GetKeyDown(jump) && isGrounded)
            {
                playerRigidbody.velocity = Vector3.up * jumpVelocity;
                SetIdle();
            }
        }
    }

    private void HandleMobileInput()
    {
        //Debug.Log($"{transform.name} input: {Input.acceleration.x} || Photon view is mine? {photonView.IsMine}");

        if (photonView.IsMine)
        {
            Rigidbody playerRigibody = player.GetComponent<Rigidbody>();

            //Debug.Log($"{transform.name} input: {Input.acceleration.x} || Photon view is mine!");

            float positiveInputValue = Input.acceleration.x;
            if (positiveInputValue < 0)
                positiveInputValue *= -1;

            float adjustedSpeed = movementSpeed * positiveInputValue;
            if (adjustedSpeed > maxSpeed)
                adjustedSpeed = maxSpeed;

            //Debug.Log($"Adjusted speed: {adjustedSpeed}");
            if (Input.acceleration.x > sensitivity)
            {
                //Debug.Log("Moving right");
                playerRigibody.velocity = new Vector3(adjustedSpeed, playerRigibody.velocity.y, playerRigibody.velocity.z);
                SetRunning();
            }
            else if (Input.acceleration.x < sensitivity * -1)
            {
                //Debug.Log("Moving left");
                playerRigibody.velocity = new Vector3(-adjustedSpeed, playerRigibody.velocity.y, playerRigibody.velocity.z);
                SetRunning();
            }
            else
            {
                playerRigibody.velocity = new Vector3(0, playerRigibody.velocity.y, 0);
                SetIdle();
            }

            if (isGrounded)
            {
                if (Input.gyro.userAcceleration.y >= 1)
                {
                    Debug.Log("JUMP!");
                    playerRigibody.velocity += Vector3.up * jumpVelocity;
                    SetIdle();
                }
            }

            if (playerRigibody.velocity.y >= maxJumpSpeed)
                playerRigidbody.velocity = new Vector3(playerRigibody.velocity.x, maxJumpSpeed, playerRigibody.velocity.z);
        }
    }

    private void SetIdle()
    {
        AnimCtrl.SetBool("Carrying Idle", true);
        AnimCtrl.SetBool("Carrying Running", false);
    }

    private void SetRunning()
    {
        AnimCtrl.SetBool("Carrying Idle", false);
        AnimCtrl.SetBool("Carrying Running", true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        else if (collision.gameObject.CompareTag("Player"))
            isGrounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        else if (collision.gameObject.CompareTag("Player"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
        else if (collision.gameObject.CompareTag("Player"))
            isGrounded = false;
    }
}
