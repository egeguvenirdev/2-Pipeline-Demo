using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollection : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
        gameObject.SetActive(false);
        Instantiate(particle, transform.position, Quaternion.Euler(180, 0, 0));
        Invoke("Reactivate", 2f);
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}