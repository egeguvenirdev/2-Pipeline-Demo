﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    //ring scaling
    public float scaleMultiplier;
    public LayerMask cylinderLayer;
    public LayerMask obstacleLayer;
    private float ringScaler;
    private Transform currentPipe;
    private Vector3 ringTargetScale;

    //ring activate object & stopping movement
    [SerializeField]
    private GameObject brokenRing;
    [SerializeField]
    private Movement movement;

    /*private void Start()
    {
        movement = FindObjectOfType<Movement>();
    }*/

    private void Update()
    {
        //current pipe detection
        currentPipe = Physics.OverlapSphere(transform.position, 0.1f, cylinderLayer)[0].transform;
        if (Input.GetMouseButton(0))
        {
            //METHOD YAZ

            //ring new scale calculations
            ringScaler = currentPipe.localScale.x * scaleMultiplier;
            ringTargetScale = new Vector3(ringScaler, ringScaler, 1);

            transform.localScale = Vector3.Slerp(transform.localScale, ringTargetScale, 0.15f);
        }
        else
        {
            Vector3 originalScale = new Vector3(1.5f, 1.5f, 1.5f);
            transform.localScale = Vector3.Slerp(transform.localScale, originalScale, 0.15f);
        }

        //die after hitting a larger scale pipe
        if (ringScaler > transform.localScale.x)
        {
            Death();
        }

        //die after touching obstacles
        if (Physics.CheckSphere(transform.position, 0.001f, obstacleLayer))
        {
            Debug.Log("heyo");
            if (transform.localScale.x <= ringTargetScale.x + 0.05f)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        movement.enabled = false;
        Destroy(gameObject);
        brokenRing.SetActive(true);
    }
}
