using UnityEngine;
using Photon.Pun;

public class PlaneMover : MonoBehaviourPun
{
    //private MicInput micInput = null;
    private MicTest2 micInput = null;
    private Rigidbody rig = null;

    private StartTimer startTimer = null;
    private Timer timer = null;

    private PlaneCollision collision = null;

    [Header("Values")]
    [SerializeField] private float micSensitivity = -40f;
    [SerializeField] private float upSpeed = 50f;

    [Header("Border")]
    [SerializeField] private float topY = 5.8f;
    [SerializeField] private float minY = 54f;

    [Header("Particles")]
    [SerializeField] private GameObject player1_DeathParticle = null;
    [SerializeField] private GameObject player2_DeathParticle = null;
    [SerializeField] private GameObject player3_DeathParticle = null;
    [SerializeField] private GameObject player4_DeathParticle = null;

    private string objectName = "";

    private GameObject animationScreen = null;
    private GameObject explanationScreen = null;
    private GameObject deathScreen = null;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        micInput = GetComponent<MicTest2>();
        //micInput = FindObjectOfType<MicTest2>();

        startTimer = FindObjectOfType<StartTimer>();
        timer = FindObjectOfType<Timer>();

        collision = GetComponent<PlaneCollision>();
    }

    private void Start()
    {
        if (transform.name == "Player1" || transform.name == "Plane1" || transform.name == "Plane1(Clone)")
            objectName = "Player1";
        else if (transform.name == "Player2" || transform.name == "Plane2" || transform.name == "Plane2(Clone)")
            objectName = "Player2";
        else if (transform.name == "Player3" || transform.name == "Plane3" || transform.name == "Plane3(Clone)")
            objectName = "Player3";
        else if (transform.name == "Player4" || transform.name == "Plane4" || transform.name == "Plane4(Clone)")
            objectName = "Player4";

        animationScreen = GameObject.Find("ClientCanvas/BlowingAnimation").gameObject;
        explanationScreen = GameObject.Find("ClientCanvas/GeneralInformation").gameObject;
        deathScreen = GameObject.Find("ClientCanvas/DeathImage").gameObject;

        if (animationScreen != null)
        {
            if (!animationScreen.activeSelf)
                animationScreen.SetActive(true);
        }

        if (explanationScreen != null)
        {
            if (!explanationScreen)
                explanationScreen.SetActive(true);
        }

        if (deathScreen != null)
        {
            if (deathScreen.activeSelf)
                deathScreen.SetActive(false);
        }
    }

    private void Update()
    {
        //Debug.Log($"Mover decibels: {micInput.MicLoudnessinDecibels}");

        if (photonView.IsMine)
        {
            if (!startTimer.inputAllowed || !timer.timeLeft)
            {
                rig.velocity = Vector3.zero;
                rig.useGravity = false;
                return;
            }
        }

        CheckBorder();

        CheckMicInput();
        CheckVelocity();

        //if (micInput == null)
        //    Debug.Log("Mic input is null!");
        //else
        //    Debug.Log("Mic input is not null!");

        //Debug.Log($"Velocity: {rig.velocity}");
    }

    private void CheckBorder()
    {
        if (photonView.IsMine)
        {
            if (rig.useGravity == false)
                rig.useGravity = true;

            if (transform.position.y >= topY)
                transform.position = new Vector3(transform.position.x, topY, transform.position.z);
            else if (transform.position.y <= minY)
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);


            //else if (transform.position.y <= minY)
            //{
            //    transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            //    GameOver();
            //}
        }
    }

    private void CheckMicInput()
    {
        //if (micInput.MicLoudnessinDecibels > micSensitivity)
        if (micInput.MicLoudnessinDecibels > micSensitivity)
            MoveUp();
    }

    private void MoveUp()
    {
        if (photonView.IsMine)
            rig.AddForce(Vector3.up * upSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void CheckVelocity()
    {
        if (photonView.IsMine)
        {
            if (rig.velocity.y >= 2)
                rig.velocity = new Vector3(0f, 2f, 0f);
            else if (rig.velocity.y <= -2)
                rig.velocity = new Vector3(0f, -2f, 0f);
        }
    }

    private void GameOver()
    {
        if (objectName == "Player1")
            PhotonNetwork.Instantiate(player1_DeathParticle.name, new Vector3(this.transform.position.x, 56, this.transform.position.z), Quaternion.identity);
        //Instantiate(player1_DeathParticle, new Vector3(this.transform.position.x, 56, this.transform.position.z), Quaternion.identity);
        else if (objectName == "Player2")
            PhotonNetwork.Instantiate(player2_DeathParticle.name, new Vector3(this.transform.position.x, 56, this.transform.position.z), Quaternion.identity);
        //Instantiate(player2_DeathParticle, new Vector3(this.transform.position.x, 56, this.transform.position.z), Quaternion.identity);
        else if (objectName == "Player3")
            PhotonNetwork.Instantiate(player3_DeathParticle.name, new Vector3(this.transform.position.x, 56, this.transform.position.z), Quaternion.identity);
        //Instantiate(player3_DeathParticle, new Vector3(this.transform.position.x, 56, this.transform.position.z), Quaternion.identity);
        else if (objectName == "Player4")
            PhotonNetwork.Instantiate(player4_DeathParticle.name, new Vector3(this.transform.position.x, 56, this.transform.position.z), Quaternion.identity);
        //Instantiate(player4_DeathParticle, new Vector3(this.transform.position.x, 56, this.transform.position.z), Quaternion.identity);

        if (animationScreen != null)
        {
            if (animationScreen.activeSelf)
                animationScreen.SetActive(false);
        }

        if (explanationScreen != null)
        {
            if (explanationScreen.activeSelf)
                explanationScreen.SetActive(false);
        }

        if (deathScreen != null)
        {
            if (!deathScreen.activeSelf)
                deathScreen.SetActive(true);
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
            collision.SetGameOverUI();
        }

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    Destroy(gameObject);
        //    collision.SetGameOverUI();
        //}

        return;
    }
}
