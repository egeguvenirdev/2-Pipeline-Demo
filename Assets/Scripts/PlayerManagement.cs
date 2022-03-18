using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    //ring scaling
    public float scaleMultiplier;
    private float ringScaleMultiplier; //it prevents scale diffrences between the objects

    private Transform currentPipe; //the pipes which the ring above it
    private Vector3 ringTargetScale;

    public LayerMask cylinderLayer;
    public LayerMask obstacleLayer;

    //ring activate object & stopping movement
    [SerializeField]
    private GameObject brokenRing;
    [SerializeField]
    private Movement movement;
    public BoxCollider boxCol;
    [SerializeField] private UIManager UIM;

    //Audio
    [SerializeField] private AudioClip deathAudio;
    private AudioSource camSound;

    private void Start()
    {
        camSound = Camera.main.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (UIM.isPaused == false)
        {
            RingResizer();
        }

        if (UIM.isPaused == true)
        {
            boxCol.enabled = false;
        }

        DeathCheck();
    }

    private void RingResizer()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            currentPipe = Physics.OverlapSphere(transform.position, 0.1f, cylinderLayer)[0].transform;

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved && !UIM.isPaused)
            {
                //ring new scale calculations
                ringScaleMultiplier = currentPipe.localScale.x * scaleMultiplier;
                ringTargetScale = new Vector3(ringScaleMultiplier, ringScaleMultiplier, 1.5f);

                transform.localScale = Vector3.Slerp(transform.localScale, ringTargetScale, 0.3f);

                if (transform.localScale.x <= ringTargetScale.x + 0.05f || transform.localScale.x >= ringTargetScale.x - 0.05f) // if the ring has true sizes, activating collider for collect gems
                {
                    boxCol.enabled = true;
                }
            }
        }

        else // if player doesnt touch the screen, resize the ring to its orginal size
        {
            Vector3 originalScale = new Vector3(1.5f, 1.5f, 1.5f);
            transform.localScale = Vector3.Slerp(transform.localScale, originalScale, 0.3f);
            BoxTriggerClose();
            boxCol.enabled = false;
        }
    }

    private void DeathCheck()
    {
        if (ringScaleMultiplier > transform.localScale.x) // if player hits another big pipe while the ring's size is smaller than that pipe, player dies
        {
            camSound.PlayOneShot(deathAudio);
            Death();
        }

        //die after touching obstacles
        if (Physics.CheckSphere(transform.position, 0.05f, obstacleLayer))
        {
            if (transform.localScale.x <= ringTargetScale.x + 0.05f)
            {
                camSound.PlayOneShot(deathAudio);
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
        UIM.OnDeath();
    }
}
