using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic; // Ensure this namespace is included for Dictionary usage
using TMPro;

public class Login : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;

    void Start()
    {
        loginButton.onClick.AddListener(LoginUser);
        registerButton.onClick.AddListener(RegisterUser);
    }

    void LoginUser()
    {
        if (!string.IsNullOrEmpty(usernameInput.text) && !string.IsNullOrEmpty(passwordInput.text))
        {
            // Connect to Photon's server
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Username or password is empty.");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon!");

        // Authenticate with Photon's Cloud using username as a player's NickName
        PhotonNetwork.AuthValues = new AuthenticationValues(usernameInput.text);
        PhotonNetwork.NickName = usernameInput.text; // Setting username as NickName for Photon

        // Join the default lobby (or a specific lobby)
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby!");

        // You can perform additional actions here after joining the lobby, such as loading another scene
        GoToLobby();
    }

    void GoToLobby()
    {
        // Load the lobby scene after successful login
        // Replace "LobbySceneName" with your actual lobby scene name
        UnityEngine.SceneManagement.SceneManager.LoadScene("LobbySceneName");
    }

    void RegisterUser()
    {
        // Handle user registration on your server-side logic, not within PUN
        Debug.Log("Account registration should be handled on the server.");
    }
}
