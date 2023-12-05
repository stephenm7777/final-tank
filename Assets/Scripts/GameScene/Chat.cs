using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI;
using TMPro; 

public class Chat : MonoBehaviour
{   
    public InputField inputField; 
    public GameObject Message; 
    public GameObject Content; 

    public void SendMessage(){
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, inputField.text);
        inputField.text = "";
    }

    [PunRPC]
    public void GetMessage(string ReceiveMessage){
        GameObject M = Instantiate(Message, Vector3.zero, Quaternion.identity, Content.transform);
        M.GetComponent<Message>().myMessage.text = ReceiveMessage; 
    }
   
}
