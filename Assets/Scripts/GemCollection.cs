using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollection : MonoBehaviour
{
    [SerializeField] private UIManager UIM;
    private ObjectPooler objPooler;
    public ParticleColor colorType;
    private Haptic haptic;

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
        haptic = FindObjectOfType<Haptic>();
        UIM = FindObjectOfType<UIManager>();
        gemCollection = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

        var particle = objPooler.GetPooledObject(colorType.ToString());
        particle.transform.position = transform.position;
        particle.transform.rotation = Quaternion.Euler(180, Random.Range(-25f, 26f), 0);
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();

        StressReceiver.shake.InduceStress(0.01f);
        haptic.HapticFeedback(MoreMountains.NiceVibrations.HapticTypes.LightImpact);

        UIM.CurrentScore();

        Invoke("Reactivate", 2f);
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}