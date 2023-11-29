using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

using TMPro;

public class ChatSystem : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;

    private string syncedMessage = "";

    private const string ChatPropertyName = "LobbyChat";

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable {{ChatPropertyName, ""}});
    }

    public void SendChatMessage()
    {
        if (PhotonNetwork.InRoom) {
            string message = $"{inputField.text}";
            photonView.RPC("ReceiveMessage", RpcTarget.All, message); // Send the message to all players
        }
        else if (PhotonNetwork.InLobby) {
            string playerName = PhotonNetwork.LocalPlayer.NickName;
            string message = $"{playerName}: {inputField.text}";

            // Get the existing chat and append the new message
            string existingChat = (string)PhotonNetwork.LocalPlayer.CustomProperties[ChatPropertyName];
            string updatedChat = $"{existingChat}\n{message}";

            // Update the custom property for lobby chat
            Hashtable customProps = new Hashtable {{ChatPropertyName, updatedChat}};
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProps);

            Debug.Log(customProps.ToString());
        }


        inputField.text = ""; // Clear the input field
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey(ChatPropertyName))
        {
            UpdateChatDisplay();
        }
    }

    private void UpdateChatDisplay()
    {
        string lobbyChat = (string)PhotonNetwork.LocalPlayer.CustomProperties[ChatPropertyName];
        chatText.text = lobbyChat;
    }

    [PunRPC]
    private void ReceiveMessage(string message)
    {
        chatText.text += $"{message}\n"; // Display the received message
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending data to others (PhotonNetwork.SendMonoMessage has assigned viewID)
            stream.SendNext(syncedMessage);
        }
        else
        {
            // Receiving data from others (via network)
            syncedMessage = (string)stream.ReceiveNext();
            chatText.text = syncedMessage;
        }
    }
}
