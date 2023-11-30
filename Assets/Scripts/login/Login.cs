using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class Login : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text messageText;
    public TMP_InputField emailInput;
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
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

  
    void RegisterUser()
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
        SceneManager.LoadScene("Lobby");
    }

    void OnError(PlayFabError error)
    {
        messageText.text = "Error: " + error.ErrorMessage;
        Debug.Log("PlayFab Error: " + error.GenerateErrorReport());
    }
    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in successfully!";
        SceneManager.LoadScene("Lobby");
    }

}
