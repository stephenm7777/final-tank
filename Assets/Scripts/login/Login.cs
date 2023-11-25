using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Login : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;

    ArrayList log;

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(login);
        registerButton.onClick.AddListener(register);

        if (File.Exists(Application.dataPath + "/logins.txt"))
        {
            log = new ArrayList(File.ReadAllLines(Application.dataPath + "/logins.txt"));
        }
        else
        {
            Debug.Log("Logins file doesn't exist");
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
            Debug.Log("Incorrect login information");
        }

    void welcomeScene()
    {
        SceneManager.LoadScene("WelcomeScreen");
    }
}
    void register()
    {
        bool isExists = false;

        log = new ArrayList(File.ReadAllLines(Application.dataPath + "/logins.txt"));
        foreach (var i in log)
        {
            if (i.ToString().Contains(usernameInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            Debug.Log($"Username '{usernameInput.text}' already exists");
        }
        else
        {
            log.Add(usernameInput.text + ":" + passwordInput.text);
            File.WriteAllLines(Application.dataPath + "/logins.txt", (String[])log.ToArray(typeof(string)));
            Debug.Log("Account Registered");
        }
    }


}