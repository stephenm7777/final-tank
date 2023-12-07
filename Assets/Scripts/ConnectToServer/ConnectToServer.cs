using Photon.Pun;
using Photon.Realtime; 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using PlayFab.ClientModels;

public class ConnectToServer : MonoBehaviourPunCallbacks, ILobbyCallbacks {
    public TMP_InputField input; 
    public TMP_Text buttonText; 
    public void OnClickConnect()
    { 
        if(input.text.Length >= 1){
            PhotonNetwork.NickName = input.text;
            buttonText.text = "Connecting";
            Debug.Log("Connecting to Photon...");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings(); 
        }
 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Photon Lobby!");
        PhotonNetwork.LoadLevel("Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected from Photon. Cause: {cause}");
        SceneManager.LoadScene("Disconnected");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        Debug.Log("Room list updated!");
        foreach (RoomInfo room in roomList){
            Debug.Log("Room Name: " + room.Name + " Players: " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

}
