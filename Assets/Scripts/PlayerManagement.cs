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
        DeathCheck();
    }

    private void RingResizer()
    {
        currentPipe = Physics.OverlapSphere(transform.position, 0.1f, cylinderLayer)[0].transform;

        if (Input.GetMouseButton(0))
        {
            //ring new scale calculations
            ringScaler = currentPipe.localScale.x * scaleMultiplier;
            ringTargetScale = new Vector3(ringScaler, ringScaler, 1.5f);

            transform.localScale = Vector3.Slerp(transform.localScale, ringTargetScale, 0.3f);

            if (transform.localScale.x <= ringTargetScale.x + 0.05f || transform.localScale.x >= ringTargetScale.x - 0.05f)
            {
                BoxTriggerOpen();
            }
        }
        else
        {
            Vector3 originalScale = new Vector3(1.5f, 1.5f, 1.5f);
            transform.localScale = Vector3.Slerp(transform.localScale, originalScale, 0.5f);
            BoxTriggerClose();
            boxCol.enabled = false;
        }
    }

    private void DeathCheck()
    {
        if (ringScaler > transform.localScale.x)
        {
            Death();
        }

        //die after touching obstacles
        if (Physics.CheckSphere(transform.position, 0.05f, obstacleLayer))
        {
            if (transform.localScale.x <= ringTargetScale.x + 0.05f)
            {
                Death();
            }
        }
    }

    //ring's collider opening and closing
    private void BoxTriggerOpen()
    {
        boxCol.enabled = true;
    }
    private void BoxTriggerClose()
    {
        boxCol.enabled = false;
    }

    //player's death
    private void Death()
    {
        movement.enabled = false;
        Destroy(gameObject);
        brokenRing.SetActive(true);
    }
}
