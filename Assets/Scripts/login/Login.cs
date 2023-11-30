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
    public TMP_Text messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField usernameInput;
    public Button loginButton;
    public Button registerButton;
    const string LAST_EMAIL_KEY = "LAST_EMAIL", LAST_PASSWORD_KEY = "LAST_PASSWORD";
    private TMP_InputField[] inputFields;
    private int currentInputField = 0;

    void Start()
    {
        inputFields = new TMP_InputField[] { emailInput, passwordInput, usernameInput };
        PlayFabSettings.staticSettings.TitleId = "A4DFC";
        loginButton.onClick.AddListener(LoginPressed);
        registerButton.onClick.AddListener(RegisterPressed);
    }

    void Update()
    {
        // Detect Tab key press to move to the next input field
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentInputField = (currentInputField + 1) % inputFields.Length;
            inputFields[currentInputField].Select();
        }
    }

    void LoginPressed()
    {
        LoginUser(emailInput.text, passwordInput.text);
    }

    void RegisterPressed()
    {
        RegisterUser(emailInput.text, usernameInput.text, passwordInput.text);
    }

    void LoginUser(string email, string password)
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest()
        {
            Email = email,
            Password = password,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        },
        result =>
        {
            if (result != null && result.InfoResultPayload != null && result.InfoResultPayload.PlayerProfile != null)
            {
                PlayerPrefs.SetString(LAST_EMAIL_KEY, email);
                PlayerPrefs.SetString(LAST_PASSWORD_KEY, password);

                string displayName = result.InfoResultPayload.PlayerProfile.DisplayName;
                if (!string.IsNullOrEmpty(displayName))
                {
                    PlayerPrefs.SetString("Username", displayName);
                }
                else
                {
                    // Handle the case where the display name is null or empty
                    PlayerPrefs.SetString("Username", "DefaultUsername");
                }

                OnLoginSuccess();
            }
            else
            {
                // Handle the case where the result or its properties are null
                messageText.text = "Error: Unable to retrieve player profile information.";
                Debug.LogError("LoginUser - Null reference encountered in result or its properties.");
            }
        },
        error => PlayFabFailure(error));
    }
    void RegisterUser(string email, string username, string password)
    {
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
        {
            Email = email,
            Password = password,
            DisplayName = username,
            RequireBothUsernameAndEmail = false
        },
        result => LoginUser(email, password),
        error => PlayFabFailure(error));
    }

    void PlayFabFailure(PlayFabError error)
    {
        messageText.text = "Error: " + error.ErrorMessage;
        Debug.Log("PlayFab Error: " + error.GenerateErrorReport());
    }

    void OnLoginSuccess()
    {
        messageText.text = "Logged in successfully!";
        SceneManager.LoadScene("ConnectToServer");
    }
}
