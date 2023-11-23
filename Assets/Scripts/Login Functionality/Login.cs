using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button goToRegisterButton;

    ArrayList log;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(login);
        goToRegisterButton.onClick.AddListener(registerScene);

        if (File.Exists(Application.dataPath + "/logins.txt"))
        {
            log = new ArrayList(File.ReadAllLines(Application.dataPath + "/logins.txt"));
        }
        else
        {
            Debug.Log("logins file doesn't exist");
        }

    }



    // Update is called once per frame
    void login()
    {
        bool isExists = false;

        log = new ArrayList(File.ReadAllLines(Application.dataPath + "/logins.txt"));

        foreach (var i in log)
        {
            string line = i.ToString();
            
            //substring 0-indexof(:) - indexof(:)+1 - i.length-1
            if (i.ToString().Substring(0, i.ToString().IndexOf(":")).Equals(usernameInput.text) &&
                i.ToString().Substring(i.ToString().IndexOf(":") + 1).Equals(passwordInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            Debug.Log($"Logging in '{usernameInput.text}'");
            welcomeScene();
        }
        else
        {
            Debug.Log("Incorrect login");
        }
    }

    void registerScene()
    {
        SceneManager.LoadScene("Register");
    }

    void welcomeScene()
    {
        SceneManager.LoadScene("WelcomeScreen");
    }
}