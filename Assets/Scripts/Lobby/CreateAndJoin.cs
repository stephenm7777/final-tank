using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField inputCreate;
    public TMP_InputField inputJoin;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public TMP_Text roomText;

    public void CreateRoom()
    {
        Debug.Log("Room Created");
        PhotonNetwork.CreateRoom(inputCreate.text, new RoomOptions { MaxPlayers = 3, IsVisible = true, IsOpen = true }, TypedLobby.Default);
    }

    public void JoinRoom()
    {
        Debug.Log("Room Joined");
        PhotonNetwork.JoinRoom(inputJoin.text);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entering Stage");
        if (PhotonNetwork.CurrentRoom.Name != "ChatRoom")
        {
            lobbyPanel.SetActive(false);
            roomPanel.SetActive(true);
            roomText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master client left the room. Closing the room.");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    }

    public void JoinRoomInList(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void JoinOrCreateRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 3, IsVisible = true, IsOpen = true };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }
}
