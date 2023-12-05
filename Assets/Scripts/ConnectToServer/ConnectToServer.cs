using Photon.Pun;
using Photon.Realtime; 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ConnectToServer : MonoBehaviourPunCallbacks, ILobbyCallbacks {
    public GameObject dissconnect; 

    private void Start()
    { 
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server!");
        PhotonNetwork.JoinLobby();
    }
    private void ConnectToPhoton(){
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Photon Lobby!");
        PhotonNetwork.LoadLevel("Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected from Photon. Cause: {cause}");
        
        ConnectToPhoton();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        Debug.Log("Room list updated!");
        foreach (RoomInfo room in roomList){
            Debug.Log("Room Name: " + room.Name + " Players: " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

}
