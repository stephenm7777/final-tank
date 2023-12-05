using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class BulletHandler : NetworkBehaviour
{
    public float launchSpeed = 75.0f;
    public float bulletTimeToLive = 3.0f;
    public GameObject objectPrefab;
    private bool controlsLocked = false;


    // Update is called once per frame
    void Update()
    {
        if (!controlsLocked && Input.GetKeyDown("space"))
        {
            SpawnObject();
            
        }
    }
    public void OnNetworkSpawn(){ 
        if(!IsOwner) Destroy(this);
    }
    public void LockControls()
    {
        Debug.Log("Locking Controls");
        controlsLocked = true;
    }

    public void UnlockControls()
    {
        Debug.Log("Unlocking Controls");
        controlsLocked = false;
    }

    void SpawnObject()
    {
        if(controlsLocked){
            return;
        }
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = Quaternion.identity;

        Vector3 localXDirection = transform.TransformDirection(Vector3.forward);
        Vector3 velocity = localXDirection * launchSpeed;

        // Instantiate Object
        GameObject newObject = Instantiate(objectPrefab, spawnPosition, spawnRotation);

        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.velocity = velocity;

        // Destroy the instantiated bullet after a certain time (bulletTimeToLive)
        Destroy(newObject, bulletTimeToLive);
    }
}
