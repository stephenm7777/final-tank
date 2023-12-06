using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class ForgotPassword : MonoBehaviour
{
    public TMP_InputField resendEmailInput;
    public Button resetButton;

    public void Awake()
    {
        resetButton.onClick.AddListener(OnResetButtonClick);
    }

    // This method is assigned to the onClick event of the button in the Unity Editor
    public void OnResetButtonClick()
    {
        string email = resendEmailInput.text;

        if (IsValidEmail(email))
        {
            var request = new SendAccountRecoveryEmailRequest
            {
                Email = email,
                TitleId = "A4DFC" // Add your Title ID here
            };

            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnForgotPasswordSuccess, OnForgotPasswordFailure);
        }
        else
        {
            Debug.LogWarning("Invalid email format. Please enter a valid email address.");
            // Provide feedback to the user about the invalid email format
        }
    }

    private bool IsValidEmail(string email)
    {
        // Implement your email validation logic here
        // You can use regular expressions or other methods to validate the email format
        // Return true if the email format is valid; otherwise, return false
        return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
    }

    private void OnForgotPasswordSuccess(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Password recovery email sent successfully!");
        SceneManager.LoadScene("Login");
        // You can also provide feedback to the user that the email has been sent
    }

    private void OnForgotPasswordFailure(PlayFabError error)
    {
        Debug.LogError("Failed to send password recovery email: " + error.ErrorMessage);
        // Handle the error, provide feedback to the user, etc.
    }
}
