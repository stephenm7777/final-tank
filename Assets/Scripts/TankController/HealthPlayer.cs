using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI;
using TMPro;

public class HealthPlayer : MonoBehaviour
{
    
    private float health = 100;
    private float minHealth = 0; 
    private float maxHealth = 100; 
    public TMP_Text healthText; 
    public PhotonView view; 
    public GameObject exitGame; 
    public GameObject game; 
    private void FixedUpdate(){
        healthText.text = health.ToString(); 
    }
    void Update(){
        if(view.IsMine){
            if(health > maxHealth){
                health = maxHealth;
            }
            if(health < minHealth){
                health = minHealth; 
            }
            if(health == minHealth){
                LeaveGame();

            }
        }
    }
    public void LeaveGame(){
        game.SetActive(false);
        exitGame.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Bullet"){
            if(view.IsMine){
            view.RPC("Damage", RpcTarget.All);
            }
        }
        
    }
    [PunRPC]
    void Damage(){
        health -= 10; 
    }
    public void OnPhotonSerializedView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(health);
        }else if(stream.IsReading){
            health = (float)stream.ReceiveNext();
        }
    }
}
