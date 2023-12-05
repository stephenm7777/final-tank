using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PlayerInd : MonoBehaviour
{
    public PhotonView view; 
    private void Awake(){
        if(!view.IsMine){
            this.gameObject.SetActive(false);
        }
    }

}
