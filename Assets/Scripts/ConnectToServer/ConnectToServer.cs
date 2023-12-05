using Photon.Pun;
using Photon.Realtime; 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ConnectToServer : MonoBehaviourPunCallbacks, ILobbyCallbacks {
    private void Start()
    { 
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Photon Lobby!");
        SceneManager.LoadScene("Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected from Photon. Cause: {cause}");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        Debug.Log("Room list updated!");
        foreach (RoomInfo room in roomList){
            Debug.Log("Room Name: " + room.Name + " Players: " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

}
