using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using Photon.Realtime; 
using UnityEngine.UI; 

public class Lobby : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel; 
    public GameObject roomPanel; 
    public Text roomName ; 
    public void Start(){ 
        PhotonNetwork.JoinLobby(); 
    }
    public void OnClickCreate(){
        if(roomInputField.text.Length >= 1){
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){ MaxPlayers = 3}); 
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false); 
        roomPanel.SetActive(true); 
        roomName.text = "Room name: " + PhotonNetwork.CurrentRoom.Name; 
    }
}
