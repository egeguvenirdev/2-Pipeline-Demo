using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float camSpeed = 0.1f;

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, camSpeed, camSpeed));
        if (transform.position.y / 50 == 0)
            camSpeed += 0.05f;
    }
}
