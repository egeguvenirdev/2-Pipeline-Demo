using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RGBText : MonoBehaviour
{
    [SerializeField] private float rFloat;
    [SerializeField] private float gFloat;
    [SerializeField] private float bFloat;
    private float rSpeed = .3f;
    private float gSpeed = .5f;
    private float bSpeed = .4f;
    private Color color;

    [SerializeField] private TMP_Text currentScoreText;

    void FixedUpdate()
    {
        if (!UIManager.UIM.isPaused)
        {
            gFloat = ((Mathf.Sin(gSpeed * Time.time) + 1f) / 4f) + 0.25f;

            rFloat = (Mathf.Sin(rSpeed * Time.time) + 1f) / 2f;

            bFloat = (Mathf.Sin(bSpeed * Time.time) + 1f) / 2f;

            color = new Color(rFloat, gFloat, bFloat, 1);
            currentScoreText.color = color;
        }
    }
}
