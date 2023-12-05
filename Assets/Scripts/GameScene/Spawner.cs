using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
public class Spawner : MonoBehaviour
{
     public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;

    private void Awake()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["TankFree_Yel"] = "TankFree_Yel"; 
    }

    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];

        string tankName = (string)PhotonNetwork.LocalPlayer.CustomProperties["TankFree_Yel"]; 
        GameObject playerToSpawn = null;

        foreach (GameObject prefab in playerPrefabs) 
        {
            if (prefab.name == tankName)
            {
                playerToSpawn = prefab;
                break;
            }
        }

        if (playerToSpawn != null)
        {
            PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
        }
        else 
        {
            Debug.LogError("Tank prefab not found for tank name: " + tankName);
        }
    }
}
