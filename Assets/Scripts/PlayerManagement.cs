using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    //ring scaling
    public float scaleMultiplier;
    private float ringScaler;

    private Transform currentPipe;
    private Vector3 ringTargetScale;

    public LayerMask cylinderLayer;
    public LayerMask obstacleLayer;

    //ring activate object & stopping movement
    [SerializeField]
    private GameObject brokenRing;
    [SerializeField]
    private Movement movement;
    public BoxCollider boxCol;

    private void Update()
    {
        RingResizer();
        BoxCollider();
        DeathCheck();
    }

    private void RingResizer()
    {
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
    }

    private void DeathCheck()
    {
        if (ringScaler > transform.localScale.x)
        {
            Death();
        }

        //die after touching obstacles
        if (Physics.CheckSphere(transform.position, 0.001f, obstacleLayer))
        {
            if (transform.localScale.x <= ringTargetScale.x + 0.05f)
            {
                Death();
            }
        }
    }

    private void BoxCollider()
    {
        if (transform.localScale.x > ringTargetScale.x + 0.05f)
        {
            boxCol.isTrigger = false;
        }
        else
        {
            boxCol.isTrigger = true;
        }
    }

    private void Death()
    {
        movement.enabled = false;
        Destroy(gameObject);
        brokenRing.SetActive(true);
    }

}
