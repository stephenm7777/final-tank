using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveTank : MonoBehaviourPunCallbacks
{
    public float spinSpeed = 90.0f; 
    PhotonView view;


    void Start(){
            view = GetComponent<PhotonView>();
        }
    void Update(){
        if(view.IsMine){
            MoveTurret();
        }
    }


    void MoveTurret(){
        if(Input.GetKey(KeyCode.E)){
                transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.Q)){
                transform.Rotate(Vector3.up, -spinSpeed * Time.deltaTime);
            }
    }
}
