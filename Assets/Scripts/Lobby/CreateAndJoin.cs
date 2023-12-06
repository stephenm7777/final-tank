using UnityEngine;
using Photon.Pun; 
using TMPro;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;


public class CreateAndJoin : MonoBehaviourPunCallbacks
{
   public TMP_InputField inputCreate; 
   public TMP_InputField inputJoin; 
   public GameObject lobbyPanel; 
   public GameObject roomPanel;
   public TMP_Text roomText; 

   public void CreateRoom(){
        Debug.Log("Room Created");
        PhotonNetwork.CreateRoom(inputCreate.text, new RoomOptions() {MaxPlayers = 3, IsVisible = true, IsOpen = true}, TypedLobby.Default, null);
   }
   public void JoinRoom(){
        Debug.Log("Room Joined");
        PhotonNetwork.JoinRoom(inputJoin.text); 
   }
   public override void OnJoinedRoom(){
        Debug.Log("Entering Stage");
        if (PhotonNetwork.CurrentRoom.Name != "ChatRoom")
        {
          lobbyPanel.SetActive(false);
          roomPanel.SetActive(true);
          roomText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
        }
    }
        
   public void JoinRoomInList(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

}
