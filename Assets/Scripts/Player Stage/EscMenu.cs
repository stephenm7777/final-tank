using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    public GameObject menu;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button disconnectBtn;


    public static bool isDisplay = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Debug.Log("working");
            if (!isDisplay)
            {
                menu.gameObject.SetActive(true);
            }
            else {
                menu.gameObject.SetActive(false);
            }
        } 
    }

    private void Awake()
    {
        resumeBtn.onClick.AddListener(() =>
        {
            menu.gameObject.SetActive(!gameObject.activeSelf);
        });

        disconnectBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        });
    }

}
