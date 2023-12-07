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
        int randomNumber = Random.Range(0, spawnPoints.Length - 1);
        Transform spawnPoint = spawnPoints[randomNumber];

        if (!IsSpawnPointOccupied(spawnPoint))
        {
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
        else
        {
            Debug.LogWarning("Spawn point is occupied. Player not spawned.");
        }
    }

    private bool IsSpawnPointOccupied(Transform spawnPoint)
    {
        // Check if there is already a player at the spawn point
        Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, 1f); // Adjust the radius as needed

        foreach (var collider in colliders)
        {
            // You may want to check for a specific tag or layer to identify players
            if (collider.CompareTag("Player"))
            {
                return true; // Spawn point is occupied
            }
        }

        return false; // Spawn point is not occupied
    }
}
