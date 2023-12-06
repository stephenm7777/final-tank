using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using Photon.Pun;

public class BulletHandler : NetworkBehaviour
{
    public float launchSpeed = 75.0f;
    public float bulletTimeToLive = 3.0f;
    public GameObject objectPrefab;
    private bool controlsLocked = false;
    PhotonView view; 

    // Update is called once per frame
    void Update()
    {
        if (!controlsLocked && Input.GetKeyDown("space") && view.IsMine)
        {
            SpawnObject();
            
        }
    }
     private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Tank")){
            HealthSystem tankHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (tankHealth != null){
                tankHealth.Damage(10);
            }
            Destroy(gameObject);
        }
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
