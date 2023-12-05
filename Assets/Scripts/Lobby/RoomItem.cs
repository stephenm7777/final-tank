using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class RoomItem : MonoBehaviourPunCallbacks
{
   public GameObject RoomPrefab; 
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
         for(int i = 0; i < roomList.Count; i++){
            print(roomList[i].Name);
            if (roomList[i].Name != "ChatRoom"){
               GameObject Room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
               Room.GetComponent<Room>().Name.text = roomList[i].Name; 
            }
         }  
    }
}
