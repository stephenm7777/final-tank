using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class MoveTank : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public float rotationSpeed = 120.0f;

    public GameObject[] leftWheels;

    public GameObject[] rightWheels;
    
    public float wheelRotationSpeed = 200.0f;

    private Rigidbody rb;

    private float moveInput;

    private float rotationInput; 

    private bool controlsLocked = false;

    public Vector3[] spawnLocation;

    PhotonView view;

    
    // Start is called before the first frame update
    void Start()
    {
       // spawnLocation = new Vector3[4];

        //spawnLocation[0] = new Vector3(64.3f, 0.5f, -65f);
        //spawnLocation[1] = new Vector3(65.3f, 0.5f, 67.3f);
        //spawnLocation[2] = new Vector3(-69.9f, 0.5f, 67.3f);
        //spawnLocation[3] = new Vector3(-82.1f, 0.5f, -97.7f);
        
        rb = GetComponent<Rigidbody>();
        //int randomNumber = Random.Range(0, 4);
        //transform.position = spawnLocation[randomNumber];
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    //public void OnNetworkSpawn(){
     //   Debug.Log("Tank Spawned");
     //   if(!IsOwner) Destroy(this);
    //}

    void Update()
    {
        if (controlsLocked)
        {
            Debug.Log("Control locked");
            return;
        }

        
        if (view.IsMine)
        {
            moveInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");
            RotateWheels(rotationInput, moveInput);
        }
        
    }

    public void LockControls()
    {
        Debug.Log("Locking Controls");
        controlsLocked = true;
    }

    public void UnlockControls()
    {
        Debug.Log("Locking Controls");
        controlsLocked = false;
    }

    void FixedUpdate()
    {
        if(controlsLocked){
            return;
        }

        MoveTankObject(moveInput);
        RotateTank(rotationInput);
    }

    void MoveTankObject(float input)
    {
        Vector3 moveDirection = transform.forward * input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }

    void RotateTank(float input)
    {
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void RotateWheels(float rotation, float moveInput)
    {
        float wheelRotation = moveInput * wheelRotationSpeed * Time.deltaTime; 
        // move the left wheels 
        foreach (GameObject wheel in leftWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation - rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
        
        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation + rotationInput * wheelRotationSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
    }
}
