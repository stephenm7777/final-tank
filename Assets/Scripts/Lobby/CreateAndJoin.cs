using UnityEngine;
using Photon.Pun; 
using TMPro;
using Photon.Realtime;


public class CreateAndJoin : MonoBehaviourPunCallbacks
{
   public TMP_InputField inputCreate; 
   public TMP_InputField inputJoin; 
   public GameObject lobbyPanel; 

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

        if (PhotonNetwork.CurrentRoom.Name == "ChatRoom")
        {
          PhotonNetwork.LoadLevel("ChatRoom");
        }
        else
        {
          lobbyPanel.SetActive(false);
          PhotonNetwork.LoadLevel("Player Stage");
        }
        
   }
   public void JoinRoomInList(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

}
