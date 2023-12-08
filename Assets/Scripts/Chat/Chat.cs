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
    Debug.Log("Send Chat Message");
    string message = messageInput.text;
    if (!string.IsNullOrEmpty(message))
    {
        PhotonView photonView = PhotonView.Get(this);
        string username = PhotonNetwork.LocalPlayer.NickName;
        
        // Censor specific words
        string censoredMessage = CensorMessage(message);

        string messageWithUsername = $"{username}: {censoredMessage}";
        photonView.RPC("RPC_SendMessage", RpcTarget.All, messageWithUsername);
        messageInput.text = string.Empty;
    }
}

private string CensorMessage(string message)
{
    // Define a list of words to censor
    string[] wordsToCensor = { "fuck", "shit" };

    // Replace the specified words with asterisks
    foreach (var word in wordsToCensor)
    {
        // Case-insensitive replacement
        message = message.Replace(word, new string('*', word.Length));
    }

    return message;
}

    [PunRPC]
    void RPC_SendMessage(string message)
    {
        chatText.text += $"{message}\n";

        string[] messages = chatText.text.Split('\n');
        if (messages.Length > maxMessages)
        {
            string[] newMessages = new string[messages.Length - 1];
            System.Array.Copy(messages, 1, newMessages, 0, newMessages.Length);
            chatText.text = string.Join("\n", newMessages);
        }
    }
}
