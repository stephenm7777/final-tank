using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class JoinChat : MonoBehaviourPunCallbacks, ILobbyCallbacks
{

    public void JoinChatRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("ChatRoom", new RoomOptions() {MaxPlayers = 10, IsVisible = true, IsOpen = true}, TypedLobby.Default, null);
    }

    public override void OnJoinedRoom(){
        Debug.Log("Entering Chat");
        if (PhotonNetwork.CurrentRoom.Name == "ChatRoom")
        {
            PhotonNetwork.LoadLevel("ChatRoom");
        }
   }

    public void ExitChatRoom()
    {
        Debug.Log("Exit Chat Room");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        
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

}