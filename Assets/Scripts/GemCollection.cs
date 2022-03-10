using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollection : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private UIManager UIM;
    private ObjectPooler objPooler;
    public ParticleColor colorType;

    public enum ParticleColor
    {
        Blue, 
        Green,
        Yellow,
        Pink,
        Purple,
        Orange
    } 

    private void Start()
    {
        objPooler = FindObjectOfType<ObjectPooler>();
        UIM = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

        var particle = objPooler.GetPooledObject(colorType.ToString());
        particle.transform.position = transform.position;
        particle.transform.rotation = Quaternion.Euler(235, Random.Range(-15f, 16f), 0);
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();

        if (UIM.isPaused == false)
        {
            Invoke("Reactivate", 2f);
        }
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}