using UnityEngine;
using Photon.Pun;

public class BulletHandler : MonoBehaviourPunCallbacks
{
    public float launchSpeed = 75.0f;
    public float bulletTimeToLive = 2.0f;
    public GameObject objectPrefab;

    void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnPosition = transform.position + transform.forward * 1.0f;
            Quaternion spawnRotation = transform.rotation;

            photonView.RPC("SpawnBullet", RpcTarget.MasterClient, spawnPosition, spawnRotation);
        }
    }

    [PunRPC]
    private void SpawnBullet(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject newObject = PhotonNetwork.Instantiate(objectPrefab.name, spawnPosition, spawnRotation);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();

        Vector3 localXDirection = newObject.transform.TransformDirection(Vector3.forward);
        rb.velocity = localXDirection * launchSpeed;

        Destroy(newObject, bulletTimeToLive);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnPosition = transform.position + transform.forward * 1.0f;
            Quaternion spawnRotation = transform.rotation;

            photonView.RPC("SpawnBullet", RpcTarget.MasterClient, spawnPosition, spawnRotation);
        }
    }

}
