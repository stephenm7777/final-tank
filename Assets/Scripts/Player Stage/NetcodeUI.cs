using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetcodeUI : MonoBehaviour
{
    [SerializeField] private Button startHostBtn;
    [SerializeField] private Button startClientBtn;

    private void Awake()
    {
        startHostBtn.onClick.AddListener(() =>
        {
            Debug.Log("Host running...");
            Unity.Netcode.NetworkManager.Singleton.StartHost();
            gameObject.SetActive(false);
        });

        startClientBtn.onClick.AddListener(() =>
        {
            Debug.Log("Client running...");
            Unity.Netcode.NetworkManager.Singleton.StartClient();
            gameObject.SetActive(false);
        });
    }
}
