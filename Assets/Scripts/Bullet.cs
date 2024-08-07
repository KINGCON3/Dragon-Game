using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
}
