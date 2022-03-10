using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollection : MonoBehaviour
{
    [SerializeField] private UIManager UIM;
    private ObjectPooler objPooler;
    public ParticleColor colorType;

    public static GemCollection gemCollection;

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
        gemCollection = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

        //Debug.Log(highScore + " bas");
        var particle = objPooler.GetPooledObject(colorType.ToString());
        particle.transform.position = transform.position;
        particle.transform.rotation = Quaternion.Euler(235, Random.Range(-15f, 16f), 0);
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();

        UIM.HighScore();

        Invoke("Reactivate", 2f);
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}