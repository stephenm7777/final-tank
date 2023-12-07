using Photon.Pun;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnClickLeaveGame(){
        if (PhotonNetwork.IsConnected){
            PhotonNetwork.LeaveRoom();
            LoadLobbyScene();
        }
    }

    private void LoadLobbyScene()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}