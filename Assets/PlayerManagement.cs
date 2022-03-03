using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    //ring scaling
    public float scaleMultiplier;
    public LayerMask cylinderLayer;
    private float ringScaler;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //current pipe detection
            Transform pipe = Physics.OverlapSphere(transform.position, 1f, cylinderLayer)[0].transform;

            //ring new scale calculations
            ringScaler = pipe.localScale.x * scaleMultiplier;
            Vector3 ringTargetScale = new Vector3(ringScaler, ringScaler, 1);

            transform.localScale = Vector3.Slerp(transform.localScale, ringTargetScale, 0.15f);
        }
        else
        {
            Vector3 originalScale = new Vector3(1.5f, 1.5f, 1.5f);
            transform.localScale = Vector3.Slerp(transform.localScale, originalScale, 0.15f);
        }
    }
}
