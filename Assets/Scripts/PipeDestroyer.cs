using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDestroyer : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if( (cam.transform.position.z - this.transform.position.z) > 10f) //set deactive the pipes after the ring moved a certain distance
        {
            gameObject.SetActive(false);
        }
    }
}
