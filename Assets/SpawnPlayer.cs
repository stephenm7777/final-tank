using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3[] spawnLocation;

    private void Start()
    {
        spawnLocation = new Vector3[4];

        spawnLocation[0] = new Vector3(64.3f, 0.5f, -65f);
        spawnLocation[1] = new Vector3(65.3f, 0.5f, 67.3f);
        spawnLocation[2] = new Vector3(-69.9f, 0.5f, 67.3f);
        spawnLocation[3] = new Vector3(-82.1f, 0.5f, -97.7f);

        int randomNumber = Random.Range(0, 4);
        PhotonNetwork.Instantiate(player.name, spawnLocation[randomNumber], Quaternion.identity);
    }
}
