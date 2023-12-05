using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayFabSettings.TitleId = "A4DFC";
        Login();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, onSuccess, onError);
    }

    void onSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }

    void onError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
}
