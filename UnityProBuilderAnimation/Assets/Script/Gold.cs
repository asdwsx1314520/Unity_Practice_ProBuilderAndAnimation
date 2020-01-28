using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour, IPools
{
    public float upForce = 80.0f;

    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-5.0f, -12.5f);
        float yForce = Random.Range(upForce / 2, upForce);
        float zForce = Random.Range(-5.0f, -12.5f);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;
    }

}
