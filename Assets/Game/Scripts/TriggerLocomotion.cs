using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerLocomotion : MonoBehaviour {

    public GameManager gameManager;
    private SteamVR_TrackedObject trackedObj;
    private Hand hand;

    

    //[Header("Controller Objects")]
    //public SteamVR_TrackedObject trackedRight;
    //public SteamVR_TrackedObject trackedLeft;

    [Header("Locomotion Vector Properties")]
    public Vector3 controllerForward;
    public Vector2 touchPad;
    public float triggerAxis;

    [Header("Player Objects")]
    public GameObject cameraRig;
    public GameObject playerEye;

    [Header("Player Sounds")]
    public AudioSource sprintingAudio;
    public AudioSource afterSprintingAudio;

    public GameObject bodyCollider;
    public GameObject footCollider;
    private Rigidbody bodyRb; //rigid body of the body Collider;
    private Rigidbody cameraRigRb; //rigid body of the body Collider;

    [Header("Movement Variables")]
    #region MovementVariables
    public bool enableSprint;
    public float moveSpeed; // Overall movespeed

    public float walkSpeed;
    public float sprintSpeed;

    public float staminaAmnt; //current stamina amount
    public float staminaAmntMax; //maximum stamina amount

    public float sprintInertia; //the extra variable to prevent the user from immediatly speeding up while sprinting
    public float sprintInertiaSpeed; // the speed at which the player overcomes innertia
    
    public float staminaRecovery; // the amount at which the stamina is replenished
    public float staminaDrain; // the amount at which the stamina drains while sprinting

    private bool isSprinting;
    private bool sprintSoundPlayed;

    #endregion

    [Header("Image Properties")]
    public Image _triangleOutlineObj;
    public Image _triangleFillObj;

    
    private void Awake()
    {
        //trackedObj = hand.handTrackedRight;
        //device = hand.handDeviceRight;
        hand = GetComponent<Hand>();
    }

    
    void Start()
    {
        //trackedObj = hand.handTrackedRight;
        //device = hand.handDeviceRight;
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        staminaAmnt = staminaAmntMax;
        
        bodyRb = bodyCollider.GetComponent<Rigidbody>();
        cameraRigRb = cameraRig.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        
        FillTriangle();
        // Initializes device variable every frame
        //trackedObj = hand.handTrackedRight;
        //device = hand.handDeviceRight;

        ///SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        //triggerAxis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x; //Gets depth of trigger press

        if (enableSprint)
        {
            // Enable sprinting when the touchpad is pressed and stamina != 0
            if (PlayerScript._deviceRight.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && staminaAmnt >= 0f)
            {
                staminaAmnt -= staminaDrain * Time.deltaTime;
                sprintInertia = Mathf.Lerp(sprintInertia, 1, Time.deltaTime * sprintInertiaSpeed);
                isSprinting = true;
                sprintSpeed = (staminaAmnt / staminaAmntMax);

                if (staminaAmnt >= .1f)
                {
                    SprintSound();
                    sprintingAudio.volume = Mathf.Lerp(sprintingAudio.volume, .07f, Time.deltaTime / 2);
                }
            }
            else
            {
                sprintSpeed = 0;
                sprintInertia = Mathf.Lerp(sprintInertia, 0, Time.deltaTime / 2f);
                sprintingAudio.volume = Mathf.Lerp(sprintingAudio.volume, 0f, Time.deltaTime);
                staminaAmnt += staminaRecovery * Time.deltaTime;
                if (staminaAmnt >= staminaAmntMax)
                {
                    staminaAmnt = staminaAmntMax;
                }
                sprintSoundPlayed = false;

            }
        }
        
    }

    private void FixedUpdate()
    {

        //Make the bodyCollider follow the player around and make it 
        //bodyCollider.transform.position = new Vector3(4, 5, 1);
        

        if (PlayerScript._trackedLeft.gameObject.activeInHierarchy)
        {
            Vector3 playerPos = new Vector3(PlayerScript._playerEye.transform.position.x, 0, PlayerScript._playerEye.transform.position.z);
            if (Vector3.Distance(playerPos, new Vector3(PlayerScript._bodyCollider.transform.position.x, 0, PlayerScript._bodyCollider.transform.position.z)) > PlayerScript._bodyCollider.transform.localScale.x / 2)
            {
                //bodyCollider.transform.position = new Vector3(playerEye.transform.position.x, bodyCollider.transform.position.y, playerEye.transform.position.z);
                //bodyCollider.transform.position = Vector3.MoveTowards(bodyCollider.transform.position, new Vector3(playerEye.transform.position.x, bodyCollider.transform.position.y, playerEye.transform.position.z), 2f * Time.deltaTime);
                PlayerScript._bodyCollider.transform.position = Vector3.MoveTowards(PlayerScript._bodyCollider.transform.position, 
                    new Vector3(PlayerScript._playerEye.transform.position.x, PlayerScript._bodyCollider.transform.position.y, PlayerScript._playerEye.transform.position.z), 2f * Time.deltaTime);
            }

            if (PlayerScript._triggerAxisLeft > .05f) //If the trigger is pressed passed a certain threshold
            {
                PlayerScript._cameraRigRb.drag = 1;
                //cameraRig.transform.position = new Vector3(cameraRig.transform.position.x, bodyCollider.transform.position.y - bodyCollider.transform.localScale.y, cameraRig.transform.position.z);
                //Assemble beginning variables
                controllerForward = PlayerScript._trackedLeft.transform.forward;
                moveSpeed = Mathf.Lerp(moveSpeed, (PlayerScript._triggerAxisLeft * walkSpeed)  + sprintSpeed, Time.deltaTime * 5f);
                Vector3 direction = new Vector3(controllerForward.x, 0, controllerForward.z);




                //bodyRb.MovePosition(bodyCollider.transform.position + direction * Time.deltaTime);

                //cameraRig.transform.position = Vector3.Lerp(new Vector3(cameraRig.transform.position.x, footCollider.transform.position.y, cameraRig.transform.position.z), new Vector3(bodyCollider.transform.position.x, footCollider.transform.position.y, bodyCollider.transform.position.z), Time.deltaTime * moveSpeed * 5);

                //cameraRigRb.AddForce(direction / 10, ForceMode.Impulse);
                PlayerScript._cameraRigRb.MovePosition(PlayerScript._cameraRigRb.transform.position + (direction * moveSpeed) * Time.deltaTime);
                PlayerScript._cameraRigRb.velocity = Vector3.ClampMagnitude(cameraRigRb.velocity, moveSpeed);
                //print(cameraRigRb.velocity.magnitude);
            } else
            {
                PlayerScript._cameraRigRb.drag = 10;
            }
        }
        
    }

    private void SprintSound()
    {
        if (!sprintSoundPlayed)
        {
            sprintingAudio.Play();
            sprintSoundPlayed = true;
        }
    }

    private void FillTriangle()
    {
        _triangleFillObj.fillAmount = Mathf.Lerp(_triangleFillObj.fillAmount, PlayerScript._triggerAxisLeft, Time.deltaTime * .75f);
        ObjectFade.ImageFadeWithDistance(_triangleFillObj, .75f, false);
        ObjectFade.ImageFadeWithDistance(_triangleOutlineObj, .75f, false);
    }
    
}
