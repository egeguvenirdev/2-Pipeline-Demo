using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float camSpeed = 0.1f;
    private bool movement = false;

    private void FixedUpdate()
    {
        if (movement)
        {
            transform.Translate(new Vector3(0, camSpeed, camSpeed));
            if (transform.position.y / 50 == 0)
                camSpeed += 0.05f;
        }
    }

    public void movementCheck()
    {
        movement = true;
    }
}
