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

    private List<string> credentials; // Use List<T> instead of ArrayList

    private void Start()
    {
        loginButton.onClick.AddListener(LoginUser);
        goToRegisterButton.onClick.AddListener(MoveToRegister);

        string filePath = Application.dataPath + "/logins.txt";
        if (File.Exists(filePath))
        {
            credentials = new List<string>(File.ReadAllLines(filePath));
        }
        else
        {
            Debug.Log("Logins file doesn't exist");
        }
    }

    private void LoginUser()
    {
        bool isExists = false;

        credentials = new List<string>(File.ReadAllLines(Application.dataPath + "/logins.txt"));

        foreach (string line in credentials)
        {
             //everything after
            string[] parts = line.Split(':');
            if (parts.Length == 2 && parts[0].Equals(usernameInput.text) && parts[1].Equals(passwordInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (is
