using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollection : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        Instantiate(particle, transform.position, Quaternion.Euler(235, Random.Range(-15f, 16f), 0));
        Invoke("Reactivate", 2f);
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}