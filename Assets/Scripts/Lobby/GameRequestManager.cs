using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class GameRequestManager : MonoBehaviourPunCallbacks
{
    public static GameRequestManager Instance; 

    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public void SendJoinRequest(string targetRoomName){
        PhotonNetwork.GetCustomRoomList(TypedLobby.Default, "IsOpen = true AND IsVisible = true AND Name = '" + targetRoomName + "'");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        RoomInfo targetRoom = roomList.Find(room => room.IsOpen && room.IsVisible);

        if (targetRoom != null){
            PhotonNetwork.JoinRoom(targetRoom.Name);
        }
        else{
            Debug.Log("Join request failed: Room not found or not joinable.");
        }
    }
}
