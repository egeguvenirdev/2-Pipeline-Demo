using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingExplosion : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Rigidbody rb = transform.GetChild(i).GetComponent<Rigidbody>();
            rb.AddExplosionForce(50f, transform.position, 10f); // force, radius
            rb.angularVelocity = new Vector3(Random.Range(-3, 4), Random.Range(-3, 4), 0); //(float)Random.Range(3, 7);
        }
    }
}
