using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ChatSystem : MonoBehaviourPunCallbacks
{
    public TMP_InputField messageInput;
    public TMP_Text chatText;

    public int maxMessages = 10; // Set the maximum number of displayed messages

    public void SendChatMessage()
    {
        string message = messageInput.text;
        if (!string.IsNullOrEmpty(message))
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("RPC_SendMessage", RpcTarget.All, message);
            messageInput.text = string.Empty;
        }
    }

    [PunRPC]
    void RPC_SendMessage(string message)
    {
        chatText.text += $"{message}\n";

        // Check if the number of messages exceeds the limit
        string[] messages = chatText.text.Split('\n');
        if (messages.Length > maxMessages)
        {
            // Remove the oldest message
            string[] newMessages = new string[messages.Length - 1];
            System.Array.Copy(messages, 1, newMessages, 0, newMessages.Length);
            chatText.text = string.Join("\n", newMessages);
        }
    }

}