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
        if( (cam.transform.position.z - this.transform.position.z) > 10f)
        {
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}
