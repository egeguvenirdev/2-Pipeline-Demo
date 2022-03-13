using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float camSpeed = 3;
    public bool movementBool = false;

    private void Update()
    {
        if (movementBool)
        {
            camSpeed = 4 * Time.deltaTime;
            transform.Translate(new Vector3(0, camSpeed, camSpeed));
            if (transform.position.y / 50 == 0)
                camSpeed += 0.05f;
        }
    }

    public void MovementStart()
    {
        movementBool = true;
    }
    public void MovementStop()
    {
        movementBool = false;
    }
}
