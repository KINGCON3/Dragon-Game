using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private Vector3 shootDir;

    public void Setup(Vector3 shoorDir)
    {
        this.shootDir = shoorDir;

    }

    private void Update()
    {
        float moveSpeed = 2f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    public override void OnNetworkSpawn()
    {
        Debug.Log("network spawning bullet");
    }
}
