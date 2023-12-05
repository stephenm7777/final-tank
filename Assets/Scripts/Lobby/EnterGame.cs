using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnterGame : MonoBehaviour
{
    public GameObject playerButton; 
    public void OnClickPlayButton(){
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.LoadLevel("Game");
        }
    }

}
