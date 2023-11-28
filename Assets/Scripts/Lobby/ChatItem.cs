using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class ChatItem : MonoBehaviour
{
    public GameObject chatPrefab; 

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(chatPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
}
